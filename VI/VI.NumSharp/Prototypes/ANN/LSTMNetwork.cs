using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class LSTMNetwork
    {
        public int input_size;
        public int hidden_size;
        public int output_size;
        public float learning_rate;

        private FloatArray2D Wf;
        private FloatArray2D Wi;
        private FloatArray2D Wc;
        private FloatArray2D Wo;
        private FloatArray2D Wv;
        private FloatArray Bf;
        private FloatArray Bi;
        private FloatArray Bc;
        private FloatArray Bo;
        private FloatArray Bv;

        private FloatArray2D mWf;
        private FloatArray2D mWi;
        private FloatArray2D mWc;
        private FloatArray2D mWo;
        private FloatArray2D mWv;
        private FloatArray mBf;
        private FloatArray mBi;
        private FloatArray mBc;
        private FloatArray mBo;
        private FloatArray mBv;
        
        public LSTMNetwork(int input, int output, int hidden, float learning_rate, float std)
        {
            input_size = input;
            output_size = output;
            hidden_size = hidden;
            this.learning_rate = learning_rate;

            Wf = NumMath.Random(hidden_size, input_size + hidden_size, std) + .5f;
            Wi = NumMath.Random(hidden_size, input_size + hidden_size, std) + .5f;
            Wc = NumMath.Random(hidden_size, input_size + hidden_size, std);
            Wo = NumMath.Random(hidden_size, input_size + hidden_size, std) + .5f;

            Bf = NumMath.Repeat(hidden_size, 0);
            Bi = NumMath.Repeat(hidden_size, 0);
            Bc = NumMath.Repeat(hidden_size, 0);
            Bo = NumMath.Repeat(hidden_size, 0);

            Wv = NumMath.Random(output_size, hidden_size, std);
            Bv = NumMath.Repeat(output_size, 0);

            ResetAdagradParams();
        }

        public void ResetAdagradParams()
        {
            mWf = NumMath.Array(hidden_size, input_size + hidden_size);
            mWi = NumMath.Array(hidden_size, input_size + hidden_size);
            mWc = NumMath.Array(hidden_size, input_size + hidden_size);
            mWo = NumMath.Array(hidden_size, input_size + hidden_size);

            mBf = NumMath.Array(hidden_size);
            mBi = NumMath.Array(hidden_size);
            mBc = NumMath.Array(hidden_size);
            mBo = NumMath.Array(hidden_size);

            mWv = NumMath.Array(output_size, hidden_size);

            mBv = NumMath.Array(output_size);
        }

        public (FloatArray z, FloatArray f, FloatArray i, FloatArray c_bar, FloatArray c, FloatArray o,
            FloatArray h, FloatArray v, FloatArray y)
            FeedForward(FloatArray x, FloatArray hprev, FloatArray cprev)
        {

            var z = x.Union(hprev);

            var f = ActivationFunctions.Sigmoid((z.T * Wf).SumLine() + Bf);
            var i = ActivationFunctions.Sigmoid((z.T * Wi).SumLine() + Bi);
            var cbar = ActivationFunctions.Tanh((z.T * Wc).SumLine() + Bc);
            var o = ActivationFunctions.Sigmoid((z.T * Wo).SumLine() + Bo);

            var c = f * cprev + i * cbar;
            var h = o * ActivationFunctions.Tanh(c);

            var v = (h.T * Wv).SumLine() + Bv;
            var y = v.Exp() / v.Exp().Sum();

            return (z, f, i, cbar, c, o, h, v, y);
        }
        
        public (double loss, FloatArray2D dWf, FloatArray2D dWi, FloatArray2D dWc, FloatArray2D dWo, FloatArray2D dWv,
            FloatArray dBf, FloatArray dBi, FloatArray dBc, FloatArray dBo, FloatArray dBv,
            FloatArray hs, FloatArray cs) BPTT(int[] inputs, int[] targets, FloatArray hprev, FloatArray cprev)
        {
            // store states
            var x_s = new Array<FloatArray>(inputs.Length);
            var z_s = new Array<FloatArray>(inputs.Length);
            var f_s = new Array<FloatArray>(inputs.Length);
            var i_s = new Array<FloatArray>(inputs.Length);
            var c_s_s = new Array<FloatArray>(inputs.Length);
            var c_s = new Array<FloatArray>(inputs.Length);
            var o_s = new Array<FloatArray>(inputs.Length);
            var h_s = new Array<FloatArray>(inputs.Length);
            var v_s = new Array<FloatArray>(inputs.Length);
            var y_s = new Array<FloatArray>(inputs.Length);
            var t_g = new Array<FloatArray>(inputs.Length);

            // loss
            var loss = 0d;

            // init timing
            h_s[-1] = hprev.Clone();
            c_s[-1] = cprev.Clone();

            // forward	
            for (var t = 0; t < inputs.Length; t++)
            {
                x_s[t] = new FloatArray(input_size);
                x_s[t][inputs[t]] = 1;

                t_g[t] = new FloatArray(output_size);
                t_g[t][targets[t]] = 1;

                (z_s[t], f_s[t], i_s[t], c_s_s[t], c_s[t], o_s[t], h_s[t], v_s[t], y_s[t]) =
                    FeedForward(x_s[t], h_s[t - 1], c_s[t - 1]);

                loss += -(t_g[t] * y_s[t].Log()).Sum();
            }

            // gradients
            var dWf = NumMath.Array(Wf.W, Wf.H);
            var dWi = NumMath.Array(Wi.W, Wi.H);
            var dWc = NumMath.Array(Wc.W, Wc.H);
            var dWo = NumMath.Array(Wo.W, Wo.H);
            var dWv = NumMath.Array(Wv.W, Wv.H);
            var dBf = NumMath.Array(Bf.Length);
            var dBi = NumMath.Array(Bi.Length);
            var dBc = NumMath.Array(Bc.Length);
            var dBo = NumMath.Array(Bo.Length);
            var dBv = NumMath.Array(Bv.Length);

            var dhnext = NumMath.Array(hidden_size);
            var dcnext = NumMath.Array(hidden_size);

            // backward
            for (var t = inputs.Length - 1; t >= 0; t--)
            {
                // output gradient
                var dv = y_s[t].Clone();
                dv[targets[t]] -= 1;

                // baackpropagate
                var dh = NumMath.Normalize(-6, 6, (Wv * dv).SumColumn() + dhnext);
                                                              
                // output gate gradient                
                var DO = ActivationFunctions.Dsigmoid(o_s[t]) * (dh * ActivationFunctions.Tanh(c_s[t]));
                
                // cell gate gradient
                var dc = dcnext.Clone();
                dc += NumMath.Normalize(-6, 6, dh * o_s[t] * ActivationFunctions.Dtanh(ActivationFunctions.Tanh(c_s[t])));
                var dcbar = ActivationFunctions.Dtanh(c_s_s[t]) * (dc * i_s[t]);
                
                // input gate gradient
                var di = ActivationFunctions.Dsigmoid(i_s[t]) * (dc * c_s_s[t]);
                
                // forget gate gradient
                var df = ActivationFunctions.Dsigmoid(f_s[t]) * (dc * cprev);
                
                // lstm next
                var dz = (Wf * df).SumColumn() + (Wi * di).SumColumn() + (Wc * dcbar).SumColumn() + (Wo * DO).SumColumn();
                
                //  dhPrev, cprev
                dhnext = dz;
                dcnext = f_s[t] * dc;

                // Accumulate W Gradients
                dWv += (h_s[t].T * dv);
                dWo += (z_s[t].T * DO);
                dWc += (z_s[t].T * dcbar);
                dWi += (z_s[t].T * di);
                dWf += (z_s[t].T * df);

                // Accumulate B Gradients
                dBv += dv;
                dBo += DO;
                dBc += dcbar;
                dBi += di;
                dBf += df;
            }

            //dWf = NumMath.Normalize(-1, 1, dWf / inputs.Length);
            //dWi = NumMath.Normalize(-1, 1, dWi / inputs.Length);
            //dWc = NumMath.Normalize(-1, 1, dWc / inputs.Length);
            //dWo = NumMath.Normalize(-1, 1, dWo / inputs.Length);
            //dWv = NumMath.Normalize(-1, 1, dWv / inputs.Length);
            //dBf = NumMath.Normalize(-1, 1, dBf/ inputs.Length);
            //dBi = NumMath.Normalize(-1, 1, dBi/ inputs.Length);
            //dBc = NumMath.Normalize(-1, 1, dBc/ inputs.Length);
            //dBo = NumMath.Normalize(-1, 1, dBo / inputs.Length);
            //dBv = NumMath.Normalize(-1, 1, dBv / inputs.Length);

            return (loss, dWf, dWi, dWc, dWo, dWv, dBf, dBi, dBc, dBo, dBv, h_s[inputs.Length - 1], c_s[inputs.Length - 1]);
        }

        public void UpdateParams(FloatArray2D dWf, FloatArray2D dWi, FloatArray2D dWc, FloatArray2D dWo, FloatArray2D dWv,
            FloatArray dBf, FloatArray dBi, FloatArray dBc, FloatArray dBo, FloatArray dBv)
        {
            // memoria Adagrad
            mWf =  (0.9f * mWf) + (0.1f * (dWf * dWf)); //Wf * dWf;
            mWi =  (0.9f * mWi) + (0.1f * (dWi * dWi)); //Wi * dWi;
            mWc =  (0.9f * mWc) + (0.1f * (dWc * dWc)); //Wc * dWc;
            mWo =  (0.9f * mWo) + (0.1f * (dWo * dWo)); //Wo * dWo;
            mWv =  (0.9f * mWv) + (0.1f * (dWv * dWv)); //Wv * dWv;
            mBf =  (0.9f * mBf) + (0.1f * (dBf * dBf)); //Bf * dBf;
            mBi =  (0.9f * mBi) + (0.1f * (dBi * dBi)); //Bi * dBi;
            mBc =  (0.9f * mBc) + (0.1f * (dBc * dBc)); //Bc * dBc;
            mBo =  (0.9f * mBo) + (0.1f * (dBo * dBo)); //Bo * dBo;
            mBv =  (0.9f * mBv) + (0.1f * (dBv * dBv)); //dBv * dBv;

            // update params
            Wf -= learning_rate / (mWf + 1e-8f).Sqrt() * dWf; 
            Wi -= learning_rate / (mWi + 1e-8f).Sqrt() * dWi; 
            Wc -= learning_rate / (mWc + 1e-8f).Sqrt() * dWc; 
            Wo -= learning_rate / (mWo + 1e-8f).Sqrt() * dWo; 
            Wv -= learning_rate / (mWv + 1e-8f).Sqrt() * dWv; 
            Bf -= learning_rate / (mBf + 1e-8f).Sqrt() * dBf; 
            Bi -= learning_rate / (mBi + 1e-8f).Sqrt() * dBi; 
            Bc -= learning_rate / (mBc + 1e-8f).Sqrt() * dBc; 
            Bo -= learning_rate / (mBo + 1e-8f).Sqrt() * dBo; 
            Bv -= learning_rate / (mBv + 1e-8f).Sqrt() * dBv;  
        }       
    }
}
