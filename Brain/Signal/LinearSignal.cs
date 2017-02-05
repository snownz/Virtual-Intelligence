using Brain.Node;
using Maths;
using Maths.Regression;

namespace Brain.Signal
{
    class LinearSignal : ISignalFunction
    {
        private readonly Range rangeInput;
        private readonly Range rangeOtput;
        private readonly Linear linear;
        
        public LinearSignal(Range rangeInput, Range rangeOtput)
        {
            this.rangeInput = rangeInput; 
            this.rangeOtput = rangeOtput;
            
            linear = new Linear();
            if(rangeOtput.Min == rangeOtput.Max)
            {
                linear.InserirDados(rangeInput.Min, 0);
                linear.InserirDados(rangeInput.Max, 1);
            }
            else
            {
                linear.InserirDados(rangeInput.Min, rangeOtput.Min);
                linear.InserirDados(rangeInput.Max, rangeOtput.Max);
            }
            linear.CriarRegressao();
        }

        public double Compute(BaseNode node, double[] values, double[] weigths)
        {
            if (values.Length != 1)
                return 0.0;
            if (rangeInput.Min == rangeInput.Max)
                return values[0];
            return linear.Calcular(values[0]);
        }
    }
}
