using Brain.Node;
using Brain.Train;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Learning.Interface
{
    public interface IUnsupervisedLearning
    {
        void UpdateWeights(BaseNode node, double rate);
    }
}
