using VI.NumSharp.Arrays;
using VI.ParallelComputing.Drivers;

namespace VI.Neural.Provider
{
    public class LossFunctionProvider : ILossFunctionProvider
    {
        private IAnnParallelInterface _function;
        public LossFunctionProvider(IAnnParallelInterface function)
        {
            _function = function;
        }


        public float Loss(Array<float> desired)
        {
            throw new System.NotImplementedException();
        }
    }
}
