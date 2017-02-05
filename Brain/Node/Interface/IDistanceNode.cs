using Brain.Learning.Interface;

namespace Brain.Node.Interface
{
    public interface IDistanceNode
    {
        IUnsupervisedLearning ILearning { get; set; }
    }
}
