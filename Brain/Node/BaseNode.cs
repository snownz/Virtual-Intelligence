using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Brain.Activation;
using Brain.Signal;
using Maths;
using Brain.Learning;
using Brain.Train;
using Brain.Learning.Enum;
using Brain.Learning.Interface;
using Brain.Activation.Interface;
using Brain.Train.Models;

namespace Brain.Node
{
    public class BaseNode
    {
        private ThreadSafeRandom rand = new ThreadSafeRandom();
        private Range randRange = new Range(0.0f, 1.0f);    
        private string name;
        private int layer;
        private double learningRate;
        private double momentum;
        private double degradationValue;
        private double? value;
        private double threshold = 0.0;
        private double? currentError;
        private List<Synapse> connectionsTo;
        private List<SynapseBack> connectionsFrom;
        private ELearning learning;
        private ISupervisedLearning Supervised;
        private IUnsupervisedLearning Unsupervised;

        public IActivationFunction activation;
        public ISignalFunction signal;       
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Layer
        {
            get { return layer; }
            set { layer = value; }
        }
        public double LearningRate
        {
            get { return learningRate; }
            set
            {
                learningRate = Math.Max(0.0, Math.Min(1.0, value));
            }
        }
        public double Momentum
        {
            get { return momentum; }
            set
            {
                momentum = Math.Max(0.0, Math.Min(1.0, value));
            }
        }        
        public double DegradationValue
        {
            get { return degradationValue; }
            set { degradationValue = value; }
        }
        public double? Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public double Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }
        public double? CurrentError
        {
            get { return currentError; }
            set { currentError = value; }
        }
        public List<Synapse> ConnectionsTo
        {
            get { return connectionsTo; }
            set { connectionsTo = value; }
        }
        public List<SynapseBack> ConnectionsFrom
        {
            get { return connectionsFrom; }
            set { connectionsFrom = value; }
        }
        public Range RandRange
        {
            get { return randRange; }
            set { randRange = value; }
        }
        public double Randomize()
        {
            double d = randRange.Length;
            return rand.NextDouble() * d + randRange.Min;
        }
        public ELearning Learning
        {
            get
            {
                return learning;
            }
            set
            {
                switch (value)
                {
                    case ELearning.Supervised:
                        UpdateConnections = (desired, rate) => { Supervised.UpdateWeights(this, desired); LearningRate = LearningRate - DegradationValue; };
                        break;
                    case ELearning.UnSupervised:
                        UpdateConnections = (desired, rate) => { Unsupervised.UpdateWeights(this, rate.Value); LearningRate = LearningRate - DegradationValue; };
                        break;
                    default:
                        UpdateConnections = (desired, rate) => { };
                        break;
                }
                learning = value;
            }
        }
        
        private void _contructor()
        {
            Threshold = 1.0;
            Value = 0;
            Layer = 0;
            Name = "";
            ConnectionsTo = new List<Synapse>();
            ConnectionsFrom = new List<SynapseBack>();
        }       
        public BaseNode(IActivationFunction activation, ISignalFunction signal, IUnsupervisedLearning Unsupervised)
        {
            _contructor();
            this.activation = activation;
            this.signal = signal;
            this.Unsupervised = Unsupervised;
            Learning = ELearning.UnSupervised;
        }
        public BaseNode(IActivationFunction activation, ISignalFunction signal, ISupervisedLearning supervised)
        {
            _contructor();
            this.activation = activation;
            this.signal = signal;
            this.Supervised = supervised;
            Learning = ELearning.Supervised;
        }
        public BaseNode(IActivationFunction activation, IUnsupervisedLearning Unsupervised)
        {
            _contructor();
            this.activation = activation;
            this.Unsupervised = Unsupervised;
            Learning = ELearning.UnSupervised;
        }
        public BaseNode(IActivationFunction activation, ISupervisedLearning supervised)
        {
            _contructor();
            this.activation = activation;
            this.Supervised = supervised;
            Learning = ELearning.Supervised;
        }
        public BaseNode(ISignalFunction signal, IUnsupervisedLearning Unsupervised)
        {
            _contructor();
            this.signal = signal;
            this.Unsupervised = Unsupervised;
            Learning = ELearning.UnSupervised;
        }
        public BaseNode(ISignalFunction signal, ISupervisedLearning supervised)
        {
            _contructor();
            this.signal = signal;
            this.Supervised = supervised;
            Learning = ELearning.Supervised;
        }             
        public BaseNode()
        {
            _contructor();
        }

        protected virtual double? Input()
        {
            var values = ConnectionsTo.Select(x => x.ConnectedNode.Output() ?? 0.0).ToArray();
            var weights = ConnectionsTo.Select(x => x.Weight).ToArray();
            return signal.Compute(this, values, weights);
        }        
        public virtual double? Output()
        {
            value = activation.Function(Input() ?? 00);
            return value;
        }
        public delegate void UpdateDelegate(Desired[] desired = null, double? rate = null);
        public UpdateDelegate UpdateConnections = (desired, rate) => { };

        public void InitWeigth()
        {
            ConnectionsTo.ForEach(con => con.Weight = Randomize());
        }                       

        public void Synapse(BaseNode n)
        {
            var weigth = Randomize();
            var s = new Synapse { ConnectedNode = n, Weight = weigth };
            ConnectionsTo.Add(s);
            n.Link(this, s);
        }
        public void Link(BaseNode n, Synapse link)
        {
            ConnectionsFrom.Add(new SynapseBack { Node = n, Link = link });
        }

        public virtual BaseNode NewNode()
        {
            return new BaseNode();
        }
    }

    public class SynapseBack
    {
        public BaseNode Node { get; set; }
        public Synapse Link { get; set; }
    }
}
