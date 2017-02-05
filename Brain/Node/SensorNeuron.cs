using Brain.Signal;
using Maths;

namespace Brain.Node
{
    public class SensorNeuron : BaseNode
    {
        private Range intervalNormalization;
        private Range intervalNormalizationOutput;
        
        public Range IntervalNormalization
        {
            get { return intervalNormalization; }
            set { signal = new LinearSignal(value, IntervalNormalizationOutput); intervalNormalization = value; }
        }
        public Range IntervalNormalizationOutput
        {
            get { return intervalNormalizationOutput; }
            set { signal = new LinearSignal(IntervalNormalization, value); intervalNormalizationOutput = value; }
        }

        public SensorNeuron()
        {
            signal = new LinearSignal(IntervalNormalization, IntervalNormalizationOutput);
        }

        protected override double? Input()
        {
            return signal.Compute(this, new[] { Value ?? 0 }, null);   
        }
        public override double? Output()
        {
            return Input();
        }        
    }
}
