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
    /// StandartFunction activation function.
    /// </summary>
    [Serializable]
    public class StandartFunction : IActivationFunction, ICloneable
    {
        public StandartFunction() { }
                
        public double Function(double x)
        {
            return x;
        }
                
        public double Derivative(double x)
        {
            return x;
        }
               
        public double Derivative2(BaseNode node, double y)
        {
            return y;
        }

        public object Clone()
        {
            return new StandartFunction();
        }
    }
}
