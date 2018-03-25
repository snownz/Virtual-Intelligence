using System.Threading.Tasks;
using VI.Neural.extension;
using VI.Neural.Factory;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class LSTMCellModel
    {
        private readonly Array<INeuron> w;
        private INeuron F => w[0];
        private INeuron I => w[1];
        private INeuron C => w[2];
        private INeuron O => w[3];

        public LSTMCellModel(int inputSize, int hiddenSize, float learningRate, float std, EnumOptimizerFunction opt)
        {
            w[0] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            w[1] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            w[2] = BuildedModels.DenseTanh(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            w[3] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
        }

        public (FloatArray z, FloatArray fGate, FloatArray iGate, FloatArray cGate, FloatArray cellState, FloatArray oGate,
           FloatArray hState)
           FeedForward(FloatArray x, FloatArray hiddenState, FloatArray cellState)
        {
            var z = hiddenState.Union(x);

            Parallel.For(0, w.Length, i => w[i].FeedForward(z));

            var c = F.Output * cellState + I.Output * C.Output;
            var h = O.Output * c.Tanh();

            return (z, F.Output, I.Output, C.Output, c, O.Output, h);
        }

        public (FloatArray dhprev, FloatArray dcprev)
            Backward(FloatArray dh, FloatArray dcnext, FloatArray cprev,
                FloatArray fGate, FloatArray iGate, FloatArray cGate, FloatArray cellState, FloatArray oGate)
        {
            // Set outputs
            F.Output = fGate;
            I.Output = iGate;
            C.Output = cGate;
            O.Output = oGate;

            // cell State Derivate 
            var cellStateAct = cellState.Tanh();
            var cellStateDer = (1 - cellStateAct * cellStateAct);

            // cell state gradient
            var dcellSate = dcnext.Clone();
            dcellSate += dh * oGate * cellStateDer;

            // output gate gradient
            var doGate = O.BackWard(dh * cellStateAct);
            
            // cell gate gradient
            var dcGate = C.BackWard(dcellSate * iGate);

            // input gate gradient
            var diGate = I.BackWard(dcellSate * cGate);

            // forget gate gradient
            var dfGate = F.BackWard(dcellSate * cprev);

            // lstm next
            var dz = (F.Weights * dfGate).SumLine() + (I.Weights * diGate).SumLine() + (C.Weights * dcGate).SumLine() + (O.Weights * doGate).SumLine();

            //  dhPrev, cprev
            return (dz, fGate * dcellSate);
        }

        public (Array<FloatArray2D> dw, Array<FloatArray> db) ComputeGradient(FloatArray input)
        {
            Parallel.For(1, w.Length, i => w[i].ComputeGradient(input));
            return (w.GetWeightsGradient(), w.GetBiasGradient());
        }

        public void UpdateParams(Array<FloatArray2D> dw, Array<FloatArray> db)
        {
            Parallel.For(0, w.Length, i => w[i].UpdateParams(dw[i], db[i]));
        }
    }
}
