using Brain.Activation;
using Brain.Learning;
using Brain.Node;
using Brain.Signal;

namespace DesktopPresentation.Nodes
{
    public class OutpuNode : BaseNode
    {
        public OutpuNode(): base(new SigmoidFunction(), new ActivationSignal(), new BackPropagationLearning())
        {

        }
    }
}
