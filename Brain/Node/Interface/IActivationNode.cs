using Brain.Learning.Interface;

namespace Brain.Node.Interface
{
    interface IActivationNode
    {
        ISupervisedLearning ILearning { get; set; }
    }
}
