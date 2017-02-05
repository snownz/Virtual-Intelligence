using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Node;
using Brain.Learning.Interface;

namespace Brain.Learning
{
    class SOMLearning : IUnsupervisedLearning
    {
        public void UpdateWeights(BaseNode node, double rate)
        {
            node.ConnectionsTo.ForEach(w => w.Weight = w.Weight + rate * ((w.ConnectedNode.Output() ?? 0.0) - w.Weight));
        }
    }
}
