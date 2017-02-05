using Brain.Node;
using Brain.Train;
using Brain.Train.Models;

namespace Brain.Learning.Interface
{
    public interface ISupervisedLearning
    {
        void UpdateWeights(BaseNode neuron, Desired[] desired);
    }
}
