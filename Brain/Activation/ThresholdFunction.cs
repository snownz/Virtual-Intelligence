using Brain.Activation.Interface;
using Brain.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Activation
{
    /// <summary>
    /// Threshold activation function.
    /// </summary>
    ///
    /// <remarks><para>The class represents threshold activation function with
    /// the next expression:
    /// <code lang="none">
    /// f(x) = 1, if x >= 0, otherwise 0
    /// </code>
    /// </para>
    /// 
    /// <para>Output range of the function: <b>[0, 1]</b>.</para>
    /// 
    /// <para>Functions graph:</para>
    /// <img src="img/neuro/threshold.bmp" width="242" height="172" />
    /// </remarks>
    ///
    [Serializable]
    public class ThresholdFunction : IActivationFunction, ICloneable
    {
        public ThresholdFunction() { }

        public double Function(double x)
        {
            return (x >= 0) ? 1 : 0;
        }

        public double Derivative(double x)
        {
            return 0;
        }

        public double Derivative2(BaseNode node, double y)
        {
            return node.Input() ?? 0.0;
        }

        public object Clone()
        {
            return new ThresholdFunction();
        }
    }
}
