namespace VI.Neural.Provider
{
    public interface IOptimizerParamsProvider
    {
        void CalculateParams();
        void UpdateWeight(ILayer target);
        void UpdateBias(ILayer target);
    }
}