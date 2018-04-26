using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecursiveNeuralUnity
    {
        private FloatArray2D wpl;
        private FloatArray2D wpr;
        private FloatArray2D wDeep;        
        private FloatArray b;

        private FloatArray2D mwpl;
        private FloatArray2D mwpr;
        private FloatArray2D mwDeep;
        private FloatArray mb;

        private float learningRate;
        
        public RecursiveNeuralUnity(int inputSize, float learningRate, float std)
        {
            wpl = NumMath.Random(inputSize, inputSize, std);
            wpr = NumMath.Random(inputSize, inputSize, std);
            wpr = NumMath.Random(inputSize, inputSize, std);
            wDeep = NumMath.Random(inputSize, inputSize, std);
            b = NumMath.Repeat(inputSize, 1);

            mwpl = NumMath.Random(inputSize, inputSize, 0);
            mwpr = NumMath.Random(inputSize, inputSize, 0);
            mwpr = NumMath.Random(inputSize, inputSize, 0);
            mwDeep = NumMath.Random(inputSize, inputSize, 0);
            mb = NumMath.Repeat(inputSize, 0);

            this.learningRate = learningRate;
        }
        
        public FloatArray FeedForward(FloatArray cl, FloatArray cr, FloatArray deep)
        {
            var p = ActivationFunctions.Tanh(((cl.T * wpl).SumLine() + (cr.T * wpr).SumLine() + (deep.T * wDeep).SumLine() + b));
            return p;
        }        

        public (FloatArray errorL, FloatArray errorR, FloatArray errorDeep, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwdeep, FloatArray db) BackWard(FloatArray p, FloatArray pl, FloatArray pr, FloatArray pprev, FloatArray ep)
        {
            var dep = ActivationFunctions.Dtanh(p) * ep;
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();
            var bkp_deep = (dep * wDeep).SumColumn();
            
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwdeep = (pprev.T * dep);

            return (bkp_pl, bkp_pr, bkp_deep, dwpl, dwpr, dwdeep, dep);
        }

        public void UpdateParams(FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwdeep, FloatArray db)
        {
            //mwpl += dwpl * dwpl;
            //mwpr += dwpr * dwpr;
            //mb += db * db;

            //wpl -= (learningRate / (mwpl + 1e-8f).Sqrt()) * dwpl;
            //wpr -= (learningRate / (mwpr + 1e-8f).Sqrt()) * dwpr;
            //b -= (learningRate / (mb + 1e-8f).Sqrt()) * db;
            
            wpl -= learningRate * dwpl;
            wpr -= learningRate * dwpr;
            wDeep  -= learningRate * dwdeep;
            b -= learningRate * db;
        }
    }
}