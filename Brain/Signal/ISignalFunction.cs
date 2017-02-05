using Brain.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Signal
{
    public interface ISignalFunction
    {
        double Compute(BaseNode node, double[] values, double[] weights);
    }
}
