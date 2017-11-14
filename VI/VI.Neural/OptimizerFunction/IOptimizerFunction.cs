using VI.Neural.Layer;

namespace VI.Neural.OptimizerFunction
{
    public interface IOptimizerFunction
    {
        void CalculateParams();
        void UpdateWeight(ILayer target);
        void UpdateBias(ILayer target);
    }
}