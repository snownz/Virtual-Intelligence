using System.Threading.Tasks;
using VI.Neural.ActivationFunction;
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
        //F,I,C,O
        //0,1,2,3
        private Array<INeuron> W;

        public LSTMCellModel(int inputSize, int hiddenSize, float learningRate, float std, OptimizerFunctionEnum opt)
        {
            W[0] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            W[1] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            W[2] = BuildedModels.DenseTanh(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
            W[3] = BuildedModels.DenseSigmoid(inputSize + hiddenSize, hiddenSize, learningRate, std, opt);
        }

        public (FloatArray z, FloatArray fGate, FloatArray iGate, FloatArray cGate, FloatArray cellState, FloatArray oGate,
           FloatArray hState)
           FeedForward(FloatArray x, FloatArray hiddenState, FloatArray cellState)
        {
            var z = hiddenState.Union(x);

            Parallel.For(0, W.Length, i => W[i].FeedForward(z));

            var c = W[0].Output * cellState + W[1].Output * W[2].Output;
            var h = W[3].Output * c.Tanh();

            return (z, W[0].Output, W[1].Output, W[2].Output, c, W[3].Output, h);
        }

        public (FloatArray DF, FloatArray DI, FloatArray DC, FloatArray DO, FloatArray dhprev, FloatArray dcprev)
            Backward(FloatArray dh, FloatArray dcnext, FloatArray cprev,
                FloatArray fGate, FloatArray iGate, FloatArray cGate, FloatArray cellState, FloatArray oGate)
        {
            var cellStateAct = cellState.Tanh();
            var cellStateDer = (1 - cellStateAct * cellStateAct);

            // cell state gradient
            var dcellSate = dcnext.Clone();
            dcellSate += dh * oGate * cellStateDer;

            // output gate gradient
            var doGate = W[3].BackWard(dh) * cellStateAct;
            
            // cell gate gradient
            var dcGate = W[2].BackWard(dcellSate) * iGate;

            // input gate gradient
            var diGate = W[1].BackWard(dcellSate) * cGate;

            // forget gate gradient
            var dfGate = W[0].BackWard(dcellSate) * cprev;

            // lstm next
            var dz = (W[0].Weights * dfGate).SumLine() + (W[1].Weights * diGate).SumLine() + (W[2].Weights * dcGate).SumLine() + (W[3].Weights * doGate).SumLine();

            //  dhPrev, cprev
            return (dfGate, diGate, dcGate, doGate, dz, fGate * dcellSate);
        }

        public (Array<FloatArray2D> dw, Array<FloatArray> db) ComputeGradient(FloatArray input)
        {
            Parallel.For(1, W.Length, i => W[i].ComputeGradient(input));
            return (W.GetWeightsGradient(), W.GetBiasGradient());
        }

        public void UpdateParams(Array<FloatArray2D> dw, Array<FloatArray> db)
        {
            Parallel.For(0, W.Length, i => W[i].UpdateParams(dw[i], db[i]));
        }
    }
}
