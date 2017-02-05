using Brain.Activation.Interface;
using Brain.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Activation
{
    [Serializable]
    public class BipolarSigmoidFunction : IActivationFunction, ICloneable
    {
        // sigmoid's alpha value
        private double alpha = 2;

        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public BipolarSigmoidFunction() { }

        public BipolarSigmoidFunction(double alpha)
        {
            this.alpha = alpha;
        }

        public double Function(double x)
        {
            return ((2 / (1 + Math.Exp(-alpha * x))) - 1);
        }

        public double Derivative(double x)
        {
            double y = Function(x);

            return (alpha * (1 - y * y) / 2);
        }

        public double Derivative2(BaseNode node, double y)
        {
            return (alpha * (1 - y * y) / 2);
        }

        public object Clone()
        {
            return new BipolarSigmoidFunction(alpha);
        }
    }
}
