using Brain.Node;

namespace Brain.Signal
{
    public class ActivationSignal : ISignalFunction
    {
        public double Compute(BaseNode node, double[] values, double[] weights)
        {
            double sum = 0.0;
            
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * values[i];
            }

            return sum + node.Threshold;
        }
    }
}
