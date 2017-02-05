using Brain.Node.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Learning.Interface;
using Brain.Learning;
using Brain.Activation;
using Brain.Signal;
using System.Drawing;

namespace Brain.Node
{
    public class CoopNode : BaseNode, IDistanceNode
    {
        private static IUnsupervisedLearning ilearning;
        public static IUnsupervisedLearning ILearning
        {
            get
            {
                if (ilearning == null)
                {
                    ilearning = new SOMLearning();
                }
                return ilearning;
            }
        }

        private double learningRadius;
        private double squaredRadius2;  
        private bool updated;
        private Point position;
        private IDistanceNode[,] neighbors;
        private IDistanceNode[,] netLayer;

        public double LearningRadius
        {
            get { return learningRadius; }
            set
            {
                learningRadius = Math.Max(0, value);
                squaredRadius2 = 2 * learningRadius * learningRadius;
            }
        }
        public double SquaredRadius2
        {
            get { return squaredRadius2; }
            set { squaredRadius2 = value; }
        }        
        public bool Updated
        {
            get { return updated; }
            set { updated = value; }
        }
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }
        public IDistanceNode[,] NetLayer
        {
            get { return netLayer; }
            set { netLayer = value; }
        }
        public IDistanceNode[,] Neighbors
        {
            get
            {
                if (neighbors == null)
                {
                    neighbors = new IDistanceNode[3, 3];

                    if (position.X - 1 >= 0)
                    {
                        if (position.Y - 1 >= 0) neighbors[0, 0] = NetLayer[position.X - 1, position.Y - 1];
                        neighbors[0, 1] = NetLayer[position.X - 1, position.Y];
                        if (position.Y + 1 < NetLayer.GetLength(1)) neighbors[0, 2] = NetLayer[position.X - 1, position.Y + 1];
                    }
                    if (position.X + 1 < NetLayer.GetLength(0))
                    {
                        if (position.Y - 1 >= 0) neighbors[2, 0] = NetLayer[position.X + 1, position.Y - 1];
                        neighbors[2, 1] = NetLayer[position.X + 1, position.Y];
                        if (position.Y + 1 < NetLayer.GetLength(1)) neighbors[2, 2] = NetLayer[position.X + 1, position.Y + 1];
                    }
                    if (position.Y - 1 >= 0) neighbors[1, 0] = NetLayer[position.X - 1, position.Y - 1];
                    if (position.Y + 1 < NetLayer.GetLength(1)) neighbors[1, 0] = NetLayer[position.X - 1, position.Y + 1];
                }
                return neighbors;
            }
        }

        public CoopNode(IDistanceNode[,] layer, int x, int y) : base(new StandartFunction(), new DisntanceSignal(), ILearning)
        {
            netLayer = layer;
            netLayer[x, y] = this;
            position = new Point(x, y); 
        }
        public override double? Output()
        {
            Updated = false;
            return base.Output();
        }

        public override BaseNode NewNode()
        {
            return null;
        }
    }
}
