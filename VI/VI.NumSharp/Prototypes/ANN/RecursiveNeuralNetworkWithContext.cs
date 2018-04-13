using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecursiveNeuralNetworkWithContext
    {
        private FloatArray2D wpl;
        private FloatArray2D wpr;
        private FloatArray2D wSP;
        private FloatArray2D wSC;
        private FloatArray2D wC;

        private FloatArray bC;
        private FloatArray b;

        private FloatArray2D mwpl;
        private FloatArray2D mwpr;
        private FloatArray2D mwSP;
        private FloatArray2D mwSC;
        private FloatArray2D mwC;

        private FloatArray mbC;
        private FloatArray mb;

        private float learningRate;

        public (FloatArray2D wpl, FloatArray2D wpr, FloatArray2D wSP, FloatArray2D wSC, FloatArray2D wC, FloatArray bC, FloatArray b) Params
        {
            get => (wpl, wpr, wSP, wSC, wC, bC, b);
            set => (wpl, wpr, wSP, wSC, wC, bC, b) = (value.wpl, value.wpr, value.wSP, value.wSC, value.wC, value.bC, value.b);
        }

        public RecursiveNeuralNetworkWithContext(int inputSize, int contextSize, float learningRate, float std)
        {
            wpl = NumMath.Random(inputSize, inputSize, std);
            wpr = NumMath.Random(inputSize, inputSize, std);
            wSP = NumMath.Random(1, inputSize, std);
            wSC = NumMath.Random(1, contextSize, std);
            wC = NumMath.Random(contextSize, inputSize, std);
            bC = NumMath.Repeat(contextSize, 1);
            b = NumMath.Repeat(inputSize, 1);

            mwpl = NumMath.Random(inputSize, inputSize, 0);
            mwpr = NumMath.Random(inputSize, inputSize, 0);
            mwSP = NumMath.Random(1, inputSize, 0);
            mwSC = NumMath.Random(1, contextSize, 0);
            mwC = NumMath.Random(contextSize, inputSize, 0);
            mbC = NumMath.Repeat(contextSize, 0);
            mb = NumMath.Repeat(inputSize, 0);

            this.learningRate = learningRate;
        }

        public (FloatArray p, FloatArray s, FloatArray c) FeedForward(FloatArray cl, FloatArray cr, FloatArray ctx)
        {
            // Node Tree Vector
            var p = ActivationFunctions.Tanh(((cl.T * wpl).SumLine() + (cr.T * wpr).SumLine() + b));
            
            // Context Vector
            var c = ActivationFunctions.Tanh((ctx.T * wC).SumLine() + bC);

            // Hidden Layer
            //var h = ActivationFunctions.Tanh(((p.T * wSP).SumLine() + (c.T * wSC).SumLine() + bH);

            // Scoring value
            var s = ActivationFunctions.Sigmoid((p.T * wSP).SumLine() + (c.T * wSC).SumLine());

            return (p, s, c);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC,
                 FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray db, FloatArray dbc)
            ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray targetScore)
        {
            var ce = s - targetScore;

            var des = ActivationFunctions.Dsigmoid(s) * ce;
            var bkp_sp = (des * wSP).SumColumn();
            var bkp_sc = (des * wSC).SumColumn();

            var dep = ActivationFunctions.Dtanh(p) * bkp_sp;
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * bkp_sc;
            var bkp_c = (dec * wC).SumColumn();

            var dwsp = (p.T * des);
            var dwsc = (c.T * des);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_c, dwsp, dwsc, dwpl, dwpr, dwc, dep, dec);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC,
                  FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray db, FloatArray dbc)
             ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray targetScore, FloatArray ep, FloatArray ec)
        {
            var ce = s - targetScore;

            var des = ActivationFunctions.Dsigmoid(s) * ce;
            var bkp_sp = (des * wSP).SumColumn();
            var bkp_sc = (des * wSC).SumColumn();

            var dep = ActivationFunctions.Dtanh(p) * (bkp_sp + ep);
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * (bkp_sc + ec);
            var bkp_c = (dec * wC).SumColumn();

            var dwsp = (p.T * des);
            var dwsc = (c.T * des);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_c, dwsp, dwsc, dwpl, dwpr, dwc, dep, dec);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC,
                  FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray db, FloatArray dbc)
             BackWard(FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray ep, FloatArray ec)
        {
            var dep = ActivationFunctions.Dtanh(p) * ep;
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * ec;
            var bkp_c = (dec * wC).SumColumn();

            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_c, dwpl, dwpr, dwc, dep, dec);
        }

        public void UpdateParams(FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray db, FloatArray dbc)
        {
            //mwSP = (9e-1f * mwSP) + (1e-1f * ( dwsp  * dwsp));
            //mwSC = (9e-1f * mwSC) + (1e-1f * ( dwsc * dwsc));
            //mwpl = (9e-1f * mwpl) + (1e-1f * ( dwpl * dwpl));
            //mwpr = (9e-1f * mwpr) + (1e-1f * ( dwpr * dwpr));
            //mwC  = (9e-1f * mwC)  + (1e-1f * ( dwc  * dwc));
            //mb   = (9e-1f * mb)   + (1e-1f * ( db   * db));
            //mbC  = (9e-1f * mbC)  + (1e-1f * ( dbc  * dbc));
            
            //wSP -= (learningRate / (mwSP + 1e-8f).Sqrt()) * dwsp;
            //wSC -= (learningRate / (mwSC + 1e-8f).Sqrt()) * dwsc;
            //wpl -= (learningRate / (mwpl + 1e-8f).Sqrt()) * dwpl;
            //wpr -= (learningRate / (mwpr + 1e-8f).Sqrt()) * dwpr;
            //wC  -= (learningRate / (mwC  + 1e-8f).Sqrt()) * dwc;
            //b   -= (learningRate / (mb   + 1e-8f).Sqrt()) * db;
            //bC  -= (learningRate / (mbC  + 1e-8f).Sqrt()) * dbc;

            wSP -= learningRate * dwsp;
            wSC -= learningRate * dwsc;
            wpl -= learningRate * dwpl;
            wpr -= learningRate * dwpr;
            wC  -= learningRate * dwc;
            b   -= learningRate * db;
            bC  -= learningRate * dbc;
        }
    }

    public class RecursiveNeuralNetworkWithContext2
    {
        private FloatArray2D wpl;
        private FloatArray2D wpr;
        private FloatArray2D wSP;
        private FloatArray2D wSC;
        private FloatArray2D wC;
        private FloatArray2D wV;

        private FloatArray bC;
        private FloatArray b;

        private FloatArray2D mwpl;
        private FloatArray2D mwpr;
        private FloatArray2D mwSP;
        private FloatArray2D mwSC;
        private FloatArray2D mwC;
        private FloatArray2D mwV;
        
        private FloatArray mbC;
        private FloatArray mb;

        private float learningRate;

        public (FloatArray2D wpl, FloatArray2D wpr, FloatArray2D wSP, FloatArray2D wSC, FloatArray2D wC, FloatArray bC, FloatArray b) Params
        {
            get => (wpl, wpr, wSP, wSC, wC, bC, b);
            set => (wpl, wpr, wSP, wSC, wC, bC, b) = (value.wpl, value.wpr, value.wSP, value.wSC, value.wC, value.bC, value.b);
        }

        public RecursiveNeuralNetworkWithContext2(int inputSize, float learningRate, float std)
        {
            wpl = NumMath.Random(inputSize, inputSize, std);
            wpr = NumMath.Random(inputSize, inputSize, std);
            wV  = NumMath.Random(inputSize, inputSize, std);
            wC  = NumMath.Random(inputSize, inputSize, std);
            wSP = NumMath.Random(1,         inputSize, std);
            wSC = NumMath.Random(1,         inputSize, std);

            bC  = NumMath.Repeat(inputSize, 1);
            b   = NumMath.Repeat(inputSize, 1);

            mwpl = NumMath.Random(inputSize, inputSize, std);
            mwpr = NumMath.Random(inputSize, inputSize, std);
            mwV  = NumMath.Random(inputSize, inputSize, std);
            mwC  = NumMath.Random(inputSize, inputSize, std);
            mwSP = NumMath.Random(1,         inputSize, std);
            mwSC = NumMath.Random(1,         inputSize, std);

            mbC  = NumMath.Repeat(inputSize, 0);
            mb   = NumMath.Repeat(inputSize, 0);

            this.learningRate = learningRate;
        }

        public (FloatArray p, FloatArray s, FloatArray c) FeedForward(FloatArray cl, FloatArray cr, FloatArray ctx, FloatArray v)
        {
            var p = ActivationFunctions.Tanh(((cl.T * wpl).SumLine() + (cr.T * wpr).SumLine() + (v.T * wV).SumLine() + b));

            var c = ActivationFunctions.Tanh((ctx.T * wC).SumLine() + bC);

            var s = ActivationFunctions.Sigmoid((p.T * wSP).SumLine() + (c.T * wSC).SumLine());

            return (p, s, c);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray errorDeep,
                 FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray2D dwv, FloatArray db, FloatArray dbc)
            ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray v, FloatArray targetScore)
        {
            var ce = s - targetScore;

            var des = ActivationFunctions.Dsigmoid(s) * ce;
            var bkp_sp = (des * wSP).SumColumn();
            var bkp_sc = (des * wSC).SumColumn();

            var dep = ActivationFunctions.Dtanh(p) * bkp_sp;
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();
            var bkp_v  = (dep * wV) .SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * bkp_sc;
            var bkp_c = (dec * wC).SumColumn();

            var dwsp = (p .T * des);
            var dwsc = (c .T * des);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwv  = (v .T * dep);
            var dwc  = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_c, bkp_v, dwsp, dwsc, dwpl, dwpr, dwc, dwv, dep, dec);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray errorDeep,
                  FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray2D dwv, FloatArray db, FloatArray dbc)
              ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray targetScore, FloatArray v, FloatArray ep, FloatArray ec)
        {
            var ce = s - targetScore;

            var des = ActivationFunctions.Dsigmoid(s) * ce;
            var bkp_sp = (des * wSP).SumColumn();
            var bkp_sc = (des * wSC).SumColumn();

            var dep = ActivationFunctions.Dtanh(p) * (bkp_sp + ep);
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();
            var bkp_v = (dep * wV).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * (bkp_sc + ec);
            var bkp_c = (dec * wC).SumColumn();

            var dwsp = (p .T * des);
            var dwsc = (c .T * des);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwv  = (v .T * dep);
            var dwc  = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_c, bkp_v, dwsp, dwsc, dwpl, dwpr, dwc, dwv, dep, dec);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray errorDeep,
                FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray2D dwv, FloatArray db, FloatArray dbc)
           BackWard(FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray v, FloatArray ep, FloatArray ec)
        {
            var dep = ActivationFunctions.Dtanh(p) * ep;
            var bkp_pl = (dep * wpl).SumColumn();
            var bkp_pr = (dep * wpr).SumColumn();
            var bkp_v = (dep * wV).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * ec;
            var bkp_c = (dec * wC).SumColumn();
            
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwv  = (v .T * dep);
            var dwc  = (cx.T * dec);

            return (bkp_pl, bkp_pr, bkp_v, bkp_c, dwpl, dwpr, dwc, dwv, dep, dec);
        }

        public void UpdateParams(FloatArray2D dwsp, FloatArray2D dwsc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray2D dwv, FloatArray db, FloatArray dbc)
        {
            mwSP = (9e-1f * mwSP) + (1e-1f * (dwsp * dwsp));
            mwSC = (9e-1f * mwSC) + (1e-1f * (dwsc * dwsc));
            mwpl = (9e-1f * mwpl) + (1e-1f * (dwpl * dwpl));
            mwpr = (9e-1f * mwpr) + (1e-1f * (dwpr * dwpr));
            mwC  = (9e-1f * mwC)  + (1e-1f * (dwc  * dwc));
            mwV  = (9e-1f * mwV)  + (1e-1f * (dwv  * dwv));
            mb   = (9e-1f * mb)   + (1e-1f * (db   * db));
            mbC  = (9e-1f * mbC)  + (1e-1f * (dbc  * dbc));

            wSP -= (learningRate / (mwSP + 1e-8f).Sqrt()) * dwsp;
            wSC -= (learningRate / (mwSC + 1e-8f).Sqrt()) * dwsc;
            wpl -= (learningRate / (mwpl + 1e-8f).Sqrt()) * dwpl;
            wpr -= (learningRate / (mwpr + 1e-8f).Sqrt()) * dwpr;
            wC  -= (learningRate / (mwC  + 1e-8f).Sqrt()) * dwc;
            wV  -= (learningRate / (mwV  + 1e-8f).Sqrt()) * dwv;
            b   -= (learningRate / (mb   + 1e-8f).Sqrt()) * db;
            bC  -= (learningRate / (mbC  + 1e-8f).Sqrt()) * dbc;

            //wSP -= learningRate * dwsp;
            //wSC -= learningRate * dwsc;
            //wpl -= learningRate * dwpl;
            //wpr -= learningRate * dwpr;
            //wC  -= learningRate * dwc;
            //b   -= learningRate * db;
            //bC  -= learningRate * dbc;
        }
    }
}