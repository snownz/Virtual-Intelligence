using Brain.Activation;
using Brain.Learning;
using Brain.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Node
{
    public static class NodeCreator
    {
        public static BaseNode[] SigmoidSupervisedBPArray(int size, string layerName, double learning, double momentum)
        {
            var array = new BaseNode[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = SigmoidSupervisedBP($"{layerName}{i}", learning, momentum);
            }
            return array;
        }

        public static BaseNode SigmoidSupervisedBP(string name, double learning, double momentum)
        {
            return new BaseNode(new SigmoidFunction(), new ActivationSignal(), new BackPropagationLearning())
            {
                LearningRate = learning,
                Momentum = momentum,
                Name = name
            };
        }
        
        public static void ConnectNodes(BaseNode[] from, BaseNode[] to)
        {
            for (int i = 0; i < from.Length; i++)
            {
                for (int j = 0; j < to.Length; j++)
                {
                    from[i].Synapse(to[j]);
                }
            }
        }

        public static void ConnectNodes(BaseNode from, BaseNode[] to)
        {
            for (int j = 0; j < to.Length; j++)
            {
                from.Synapse(to[j]);
            }
        }

        public static void ConnectNodes(BaseNode[] from, BaseNode to)
        {
            for (int i = 0; i < from.Length; i++)
            {
                from[i].Synapse(to);
            }
        }

        public static void ConnectNodes(BaseNode from, BaseNode to)
        {
            from.Synapse(to);
        }
    }
}
