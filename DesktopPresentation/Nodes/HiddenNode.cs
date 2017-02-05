using Brain.Activation;
using Brain.Learning;
using Brain.Node;
using Brain.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPresentation.Nodes
{
    public class HiddenNode : BaseNode
    {
        public HiddenNode(): base(new SigmoidFunction(), new ActivationSignal(), new BackPropagationLearning())
        {

        }
    }
}
