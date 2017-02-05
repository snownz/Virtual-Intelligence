using Brain.Node;
using System;

namespace Brain.Signal
{
    class DisntanceSignal : ISignalFunction
    {
        public double Compute(BaseNode node, double[] values, double[] weights)
        {
            double dif = 0.0;
            
            for (int i = 0; i < values.Length; i++)
            {
                dif += Math.Abs(weights[i] - values[i]);
            }
            return dif;
        }
    }
}
