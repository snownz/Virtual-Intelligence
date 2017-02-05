using Brain.Node;

namespace Brain.Learning.Interface
{
    public interface IUnsupervisedLearning
    {
        void UpdateWeights(BaseNode node);
    }
}
