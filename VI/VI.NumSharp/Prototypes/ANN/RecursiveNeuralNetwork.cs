using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecursiveNeuralNetwork
    {
        private FloatArray2D w;
        private FloatArray2D wScore;
        private FloatArray b;

        private float learningRate;

        public RecursiveNeuralNetwork(int inputSize, float learningRate, float std)
        {
            w = NumMath.Random(inputSize, inputSize + inputSize, std);
            wScore = NumMath.Random(1, inputSize, std);
            b = NumMath.Repeat(inputSize, 1);

            this.learningRate = learningRate;
        }

        public (FloatArray p, FloatArray s, FloatArray x) FeedForward(FloatArray c1, FloatArray c2)
        {
            var x = c1.Union(c2);

            var p = ActivationFunctions.Tanh(((x.T * w).SumLine() + b));
            var s = ActivationFunctions.Sigmoid((p.T * wScore).SumLine());

            return (p, s, x);
        }

        public (float loss, FloatArray errorC1, FloatArray errorC2, FloatArray2D dw, FloatArray2D dwScore, FloatArray db)
            ComputeErrorNBackWard(FloatArray p, FloatArray score, FloatArray inputs, FloatArray target)
        {
            var loss = 1 * (score - target).Pow(2).Sum();

            var ce = score - target;

            var deScore = ActivationFunctions.Dsigmoid(score) * ce;
            var error = (deScore * wScore).SumColumn();

            var de = ActivationFunctions.Dtanh(p) * error;
            var dh = (de * w).SumColumn();
            var backProp = dh.Split(2);

            var dwScore = (p.T * deScore);
            var dw = (inputs.T * de);

            return (loss, backProp[0], backProp[0], dw, dwScore, de);
        }

        public (float loss, FloatArray errorC1, FloatArray errorC2, FloatArray2D dw, FloatArray2D dwScore, FloatArray db)
           ComputeErrorNBackWard(FloatArray p, FloatArray score, FloatArray inputs, FloatArray target, FloatArray error)
        {
            var loss = 1 * (score - target).Pow(2).Sum();

            var ce = score - target;

            var deScore = ActivationFunctions.Dsigmoid(score) * ce;
            error += (deScore * wScore).SumColumn();

            var de = ActivationFunctions.Dtanh(p) * error;
            var backProp = (de * w).SumColumn().Split(2);

            var dwScore = (p.T * deScore);
            var dw = (inputs.T * de);

            return (loss, backProp[0], backProp[1], dw, dwScore, de);
        }

        public (FloatArray errorC1, FloatArray errorC2, FloatArray2D dw, FloatArray db)
            BackWard(FloatArray error, FloatArray p, FloatArray inputs)
        {
            var de = ActivationFunctions.Dtanh(p) * error;
            var dh = (de * w).SumColumn();

            var backProp = dh.Split(2);

            var dw = (inputs.T * de);

            return (backProp[0], backProp[1], dw, de);
        }

        public void UpdateParams(FloatArray2D dw, FloatArray2D dwScore, FloatArray db)
        {
            w -= learningRate * dw;
            wScore -= learningRate * dwScore;
            b -= learningRate * db;
        }
    }
}