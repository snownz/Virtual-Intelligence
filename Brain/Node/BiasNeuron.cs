using Brain.Activation;
using Brain.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Node
{
    public class BiasNeuron : BaseNode
    {
        protected override double? Input()
        {
            return Value;
        }
        public override double? Output()
        {
            return Input();
        }
    }
}
