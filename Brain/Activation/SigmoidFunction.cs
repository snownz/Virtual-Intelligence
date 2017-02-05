using Brain.Activation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Activation
{
    /// <summary>
    /// Sigmoid activation function.
    /// </summary>
    ///
    /// <remarks><para>The class represents sigmoid activation function with
    /// the next expression:
    /// <code lang="none">
    ///                1
    /// f(x) = ------------------
    ///        1 + exp(-alpha * x)
    ///
    ///           alpha * exp(-alpha * x )
    /// f'(x) = ---------------------------- = alpha * f(x) * (1 - f(x))
    ///           (1 + exp(-alpha * x))^2
    /// </code>
    /// </para>
    ///
    /// <para>Output range of the function: <b>[0, 1]</b>.</para>
    /// 
    /// <para>Functions graph:</para>
    /// <img src="img/neuro/sigmoid.bmp" width="242" height="172" />
    /// </remarks>
    /// 
    [Serializable]
    public class SigmoidFunction : IActivationFunction, ICloneable
    {
        // sigmoid's alpha value
        private double alpha = 2;

        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public SigmoidFunction() { }

        public SigmoidFunction(double alpha)
        {
            this.alpha = alpha;
        }

        public double Function(double x)
        {
            return (1 / (1 + Math.Exp(-alpha * x)));
        }

        public double Derivative(double x)
        {
            double y = Function(x);

            return (alpha * y * (1 - y));
        }

        public double Derivative2(double y)
        {
            return (alpha * y * (1 - y));
        }

        public object Clone()
        {
            return new SigmoidFunction(alpha);
        }
    }
}
