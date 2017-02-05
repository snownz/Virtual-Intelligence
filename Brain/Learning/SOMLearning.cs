using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Node;
using Brain.Learning.Interface;
using Brain.Node.Interface;
using System.Drawing;

namespace Brain.Learning
{
    class SOMLearning : IUnsupervisedLearning
    {
        private void propagate(IDistanceNode node, IDistanceNode winner)
        {
            if (node.LearningRadius > 0)
            {
                for (int i = 0; i < node.Neighbors.GetLength(0); i++)
                {
                    for (int j = 0; j < node.Neighbors.GetLength(1); j++)
                    {
                        if (node.Neighbors[i, j] != null && !node.Updated)
                        {
                            updateNeighbors(node.Neighbors[i, j], winner);
                        }
                    }
                }
            }
        }

        private void updateNeighbors(IDistanceNode node, IDistanceNode winner)
        {
            node.Updated = true;

            var dx = node.Position.X - winner.Position.X;
            var dy = node.Position.Y - winner.Position.Y;

            double factor = Math.Exp(-(dx * dx + dy * dy) / winner.SquaredRadius2);

            var bNode = (node as BaseNode);

            bNode.ConnectionsTo.ForEach(w => w.Weight += (((w.ConnectedNode.Value ?? 0.0) - w.Weight) * factor) * bNode.LearningRate);

            propagate(node, winner);
        }

        public void UpdateWeights(BaseNode node)
        {
            if(node is IDistanceNode)
            {
                var iNode = node as IDistanceNode;
                node.ConnectionsTo.ForEach(w => w.Weight +=  ((w.ConnectedNode.Value ?? 0.0) - w.Weight) * node.LearningRate);          
                propagate(iNode, iNode);
            }
        }      
    }
}
