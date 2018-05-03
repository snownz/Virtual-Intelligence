using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecursiveNeuralNetworkWithContext
    {        
        private FloatArray2D wpl, wpr, wHP, wHC, wC, wS;
        private FloatArray bC, bP, bH;
        private FloatArray2D mwpl, mwpr, mwHP, mwHC, mwC, mwS;
        private FloatArray mbC, mbH, mbP;
        private FloatArray2D Adam_m_ws,  Adam_v_ws,
                             Adam_m_wHP, Adam_v_wHP,
                             Adam_m_wHC, Adam_v_wHC,
                             Adam_m_wpl, Adam_v_wpl,
                             Adam_m_wpr, Adam_v_wpr,
                             Adam_m_wC,  Adam_v_wC;

        private FloatArray Adam_m_bP, Adam_v_bP,
                           Adam_m_bC, Adam_v_bC,
                           Adam_m_bH, Adam_v_bH;
                             
        private float Adam_Beta_1 = 0.9f, Adam_Beta_2 = 0.999f, RMSprop_v = 0.999f;
        private float Adam_e = 0.00000001f;

        private readonly float _learningRate;

        public RecursiveNeuralNetworkWithContext (int inputSize, float learningRate, float std)
        {
            int hSize = 20;

            wpl = NumMath.Random( inputSize, inputSize, std    );
            wpr = NumMath.Random( inputSize, inputSize, std    );
            wC  = NumMath.Random( inputSize, inputSize, std    );
            wHP = NumMath.Random( hSize,     inputSize, std    );
            wHC = NumMath.Random( hSize,     inputSize, std    );
            wS  = NumMath.Random( 1,         hSize,     1e-10f );
           
            bC = NumMath.Repeat( inputSize, 1f / (float)inputSize );
            bH = NumMath.Repeat( hSize,     1f / (float)hSize     );
            bP = NumMath.Repeat( inputSize, 1f / (float)inputSize );
            
            mwpl = NumMath.Array( inputSize, inputSize );
            mwpr = NumMath.Array( inputSize, inputSize );
            mwC  = NumMath.Array( inputSize, inputSize );
            mwHP = NumMath.Array( hSize,     inputSize );
            mwHC = NumMath.Array( hSize,     inputSize );
            mwS  = NumMath.Array( 1,         hSize     );
                      
            mbC = NumMath.Array( inputSize );
            mbH = NumMath.Array( hSize     );
            mbP = NumMath.Array( inputSize );

            Adam_m_wpl = NumMath.Array( inputSize, inputSize );
            Adam_m_wpr = NumMath.Array( inputSize, inputSize );
            Adam_m_wC  = NumMath.Array( inputSize, inputSize );
            Adam_m_wHP = NumMath.Array( hSize,     inputSize );
            Adam_m_wHC = NumMath.Array( hSize,     inputSize );
            Adam_m_ws  = NumMath.Array( 1,         hSize     );
                      
            Adam_m_bC = NumMath.Array( inputSize );
            Adam_m_bH = NumMath.Array( hSize     );
            Adam_m_bP = NumMath.Array( inputSize );

            Adam_v_wpl = NumMath.Array( inputSize, inputSize );
            Adam_v_wpr = NumMath.Array( inputSize, inputSize );
            Adam_v_wC  = NumMath.Array( inputSize, inputSize );
            Adam_v_wHP = NumMath.Array( hSize,     inputSize );
            Adam_v_wHC = NumMath.Array( hSize,     inputSize );
            Adam_v_ws  = NumMath.Array( 1,         hSize     );
                     
            Adam_v_bC = NumMath.Array( inputSize );
            Adam_v_bH = NumMath.Array( hSize     );
            Adam_v_bP = NumMath.Array( inputSize );

            _learningRate = learningRate;
        }

        public (FloatArray p, FloatArray s, FloatArray h, FloatArray c) FeedForward(FloatArray cl, FloatArray cr, FloatArray ctx)
        {
            // Node Tree Vector
            var p = ActivationFunctions.Tanh(((cl.T * wpl).SumLine() + (cr.T * wpr).SumLine() + bP));
            
            // Context Vector
            var c = ActivationFunctions.Tanh((ctx.T * wC).SumLine() + bC);
           
            // Hidden Layer
            var h = ActivationFunctions.Tanh((p.T * wHP).SumLine() + (c.T * wHC).SumLine() + bH);

            // Scoring value
            var s = ActivationFunctions.Sigmoid((h.T * wS).SumLine());

            return (p, s, h, c);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray2D dws, FloatArray2D dwhp, FloatArray2D dwhc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbh, FloatArray dbc, FloatArray dbp) ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray h, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray targetScore)
        {
            var ce = s - targetScore;

            var des = ActivationFunctions.Dsigmoid(s) * ce;
            var bkpS = (des * wS).SumColumn();

            var deh = ActivationFunctions.Dtanh(h) * bkpS;
            var bkpHp = (deh * wHP).SumColumn();
            var bkpHc = (deh * wHC).SumColumn();

            var dep = ActivationFunctions.Dtanh(p) * bkpHp;
            var bkpPl = (dep * wpl).SumColumn();
            var bkpPr = (dep * wpr).SumColumn();

            var dec = ActivationFunctions.Dtanh(c) * bkpHc;
            var bkpC = (dec * wC).SumColumn();

            var dws  = (h.T * des);
            var dwhp = (p.T * deh);
            var dwhc = (c.T * deh);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc  = (cx.T * dec);

            return (bkpPl, bkpPr, bkpC, dws, dwhp, dwhc, dwpl, dwpr, dwc, deh, dec, dep);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray2D dws, FloatArray2D dwhp, FloatArray2D dwhc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbh, FloatArray dbc, FloatArray dbp) ComputeErrorNBackWard(FloatArray s, FloatArray p, FloatArray c, FloatArray h, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray targetScore, FloatArray ep, FloatArray ec)
        {
            var scoreError = s - targetScore;

            var des  = ActivationFunctions.Dsigmoid(s) * scoreError;
            var bkpS = (des * wS).SumColumn();

            var deh   = ActivationFunctions.Dtanh(h) * bkpS;
            var bkpHp = (deh * wHP).SumColumn();
            var bkpHc = (deh * wHC).SumColumn();

            var dep   = ActivationFunctions.Dtanh(p) * ( bkpHp + ep );
            var bkpPl = (dep * wpl).SumColumn();
            var bkpPr = (dep * wpr).SumColumn();

            var dec  = ActivationFunctions.Dtanh(c) * ( bkpHc + ec );
            var bkpC = (dec * wC).SumColumn();

            var dws  = (h.T  * des);
            var dwhp = (p.T  * deh);
            var dwhc = (c.T  * deh);
            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc  = (cx.T * dec);

            return (bkpPl, bkpPr, bkpC, dws, dwhp, dwhc, dwpl, dwpr, dwc, deh, dec, dep);
        }

        public (FloatArray errorL, FloatArray errorR, FloatArray errorC, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbc, FloatArray dbp) BackWard(FloatArray p, FloatArray c, FloatArray pl, FloatArray pr, FloatArray cx, FloatArray ep, FloatArray ec)
        {
            var dep   = ActivationFunctions.Dtanh(p) * ep;
            var bkpPl = (dep * wpl).SumColumn();
            var bkpPr = (dep * wpr).SumColumn();

            var dec  = ActivationFunctions.Dtanh(c) * ec;
            var bkpC = (dec * wC).SumColumn();

            var dwpl = (pl.T * dep);
            var dwpr = (pr.T * dep);
            var dwc  = (cx.T * dec);

            return (bkpPl, bkpPr, bkpC, dwpl, dwpr, dwc, dec, dep);
        }

        public void AdamUpdateParams(FloatArray2D dws, FloatArray2D dwhp, FloatArray2D dwhc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbh, FloatArray dbc, FloatArray dbp)
        {
            // M
            Adam_m_ws  = Adam_Beta_1 * Adam_m_ws  + ( 1-Adam_Beta_1 ) * ( dws  );
            Adam_m_wHP = Adam_Beta_1 * Adam_m_wHP + ( 1-Adam_Beta_1 ) * ( dwhp );
            Adam_m_wHC = Adam_Beta_1 * Adam_m_wHC + ( 1-Adam_Beta_1 ) * ( dwhc );
            Adam_m_wpl = Adam_Beta_1 * Adam_m_wpl + ( 1-Adam_Beta_1 ) * ( dwpl );
            Adam_m_wpr = Adam_Beta_1 * Adam_m_wpr + ( 1-Adam_Beta_1 ) * ( dwpr );
            Adam_m_wC  = Adam_Beta_1 * Adam_m_wC  + ( 1-Adam_Beta_1 ) * ( dwc  );
            Adam_m_bP  = Adam_Beta_1 * Adam_m_bP  + ( 1-Adam_Beta_1 ) * ( dbp  );
            Adam_m_bC  = Adam_Beta_1 * Adam_m_bC  + ( 1-Adam_Beta_1 ) * ( dbc  );
            Adam_m_bH  = Adam_Beta_1 * Adam_m_bH  + ( 1-Adam_Beta_1 ) * ( dbh  );
            
            // V
            Adam_v_ws  = Adam_Beta_2 * Adam_v_ws  + ( 1-Adam_Beta_2 ) * ( dws  * dws  );
            Adam_v_wHP = Adam_Beta_2 * Adam_v_wHP + ( 1-Adam_Beta_2 ) * ( dwhp * dwhp );
            Adam_v_wHC = Adam_Beta_2 * Adam_v_wHC + ( 1-Adam_Beta_2 ) * ( dwhc * dwhc );
            Adam_v_wpl = Adam_Beta_2 * Adam_v_wpl + ( 1-Adam_Beta_2 ) * ( dwpl * dwpl );
            Adam_v_wpr = Adam_Beta_2 * Adam_v_wpr + ( 1-Adam_Beta_2 ) * ( dwpr * dwpr );
            Adam_v_wC  = Adam_Beta_2 * Adam_v_wC  + ( 1-Adam_Beta_2 ) * ( dwc  * dwc  );
            Adam_v_bP  = Adam_Beta_2 * Adam_v_bP  + ( 1-Adam_Beta_2 ) * ( dbp  * dbp  );
            Adam_v_bC  = Adam_Beta_2 * Adam_v_bC  + ( 1-Adam_Beta_2 ) * ( dbc  * dbc  );
            Adam_v_bH  = Adam_Beta_2 * Adam_v_bH  + ( 1-Adam_Beta_2 ) * ( dbh  * dbh  );
        
            // MG
            var Adam_m_ws_hat  = Adam_m_ws  / ( 1-Adam_Beta_1 );        
            var Adam_m_wHP_hat = Adam_m_wHP / ( 1-Adam_Beta_1 );        
            var Adam_m_wHC_hat = Adam_m_wHC / ( 1-Adam_Beta_1 );        
            var Adam_m_wpl_hat = Adam_m_wpl / ( 1-Adam_Beta_1 );        
            var Adam_m_wpr_hat = Adam_m_wpr / ( 1-Adam_Beta_1 );        
            var Adam_m_wC_hat  = Adam_m_wC  / ( 1-Adam_Beta_1 );        
            var Adam_m_bP_hat  = Adam_m_bP  / ( 1-Adam_Beta_1 );        
            var Adam_m_bC_hat  = Adam_m_bC  / ( 1-Adam_Beta_1 );        
            var Adam_m_bH_hat  = Adam_m_bH  / ( 1-Adam_Beta_1 );        
            
            // VG
            var Adam_v_ws_hat  = Adam_v_ws  / ( 1-Adam_Beta_2 );        
            var Adam_v_wHP_hat = Adam_v_wHP / ( 1-Adam_Beta_2 );        
            var Adam_v_wHC_hat = Adam_v_wHC / ( 1-Adam_Beta_2 );        
            var Adam_v_wpl_hat = Adam_v_wpl / ( 1-Adam_Beta_2 );        
            var Adam_v_wpr_hat = Adam_v_wpr / ( 1-Adam_Beta_2 );        
            var Adam_v_wC_hat  = Adam_v_wC  / ( 1-Adam_Beta_2 );        
            var Adam_v_bP_hat  = Adam_v_bP  / ( 1-Adam_Beta_2 );        
            var Adam_v_bC_hat  = Adam_v_bC  / ( 1-Adam_Beta_2 );        
            var Adam_v_bH_hat  = Adam_v_bH  / ( 1-Adam_Beta_2 );   
        
            // update
            wS  -= ( _learningRate / ( ( Adam_v_ws_hat  ).Sqrt() + Adam_e ) ) * Adam_m_ws_hat;
            wHP -= ( _learningRate / ( ( Adam_v_wHP_hat ).Sqrt() + Adam_e ) ) * Adam_m_wHP_hat;
            wHC -= ( _learningRate / ( ( Adam_v_wHC_hat ).Sqrt() + Adam_e ) ) * Adam_m_wHC_hat;
            wpl -= ( _learningRate / ( ( Adam_v_wpl_hat ).Sqrt() + Adam_e ) ) * Adam_m_wpl_hat;
            wpr -= ( _learningRate / ( ( Adam_v_wpr_hat ).Sqrt() + Adam_e ) ) * Adam_m_wpr_hat;
            wC  -= ( _learningRate / ( ( Adam_v_wC_hat  ).Sqrt() + Adam_e ) ) * Adam_m_wC_hat;
            bP  -= ( _learningRate / ( ( Adam_v_bP_hat  ).Sqrt() + Adam_e ) ) * Adam_m_bP_hat;
            bC  -= ( _learningRate / ( ( Adam_v_bC_hat  ).Sqrt() + Adam_e ) ) * Adam_m_bC_hat;
            bH  -= ( _learningRate / ( ( Adam_v_bH_hat  ).Sqrt() + Adam_e ) ) * Adam_m_bH_hat;
        }

        public void RmsPropUpdateParams(FloatArray2D dws, FloatArray2D dwhp, FloatArray2D dwhc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbh, FloatArray dbc, FloatArray dbp)
        {
            mwS  = RMSprop_v * mwS  + ( ( 1 - RMSprop_v ) * (dws  * dws  ) );
            mwHP = RMSprop_v * mwHP + ( ( 1 - RMSprop_v ) * (dwhp * dwhp ) );
            mwHC = RMSprop_v * mwHC + ( ( 1 - RMSprop_v ) * (dwhc * dwhc ) );
            mwpl = RMSprop_v * mwpl + ( ( 1 - RMSprop_v ) * (dwpl * dwpl ) );
            mwpr = RMSprop_v * mwpr + ( ( 1 - RMSprop_v ) * (dwpr * dwpr ) );
            mwC  = RMSprop_v * mwC  + ( ( 1 - RMSprop_v ) * (dwc  * dwc  ) );
            mbP  = RMSprop_v * mbP  + ( ( 1 - RMSprop_v ) * (dbp  * dbp  ) );
            mbC  = RMSprop_v * mbC  + ( ( 1 - RMSprop_v ) * (dbc  * dbc  ) );
            mbH  = RMSprop_v * mbH  + ( ( 1 - RMSprop_v ) * (dbh  * dbh  ) );

            wS  -= ( _learningRate / (mwS  + 1e-8f ).Sqrt()) * dws;
            wHP -= ( _learningRate / (mwHP + 1e-8f ).Sqrt()) * dwhp;
            wHC -= ( _learningRate / (mwHC + 1e-8f ).Sqrt()) * dwhc;
            wpl -= ( _learningRate / (mwpl + 1e-8f ).Sqrt()) * dwpl;
            wpr -= ( _learningRate / (mwpr + 1e-8f ).Sqrt()) * dwpr;
            wC  -= ( _learningRate / (mwC  + 1e-8f ).Sqrt()) * dwc;
            bP  -= ( _learningRate / (mbP  + 1e-8f ).Sqrt()) * dbp;
            bC  -= ( _learningRate / (mbC  + 1e-8f ).Sqrt()) * dbc;
            bH  -= ( _learningRate / (mbH  + 1e-8f ).Sqrt()) * dbh;
        }

        public void AdagradUpdateParams(FloatArray2D dws, FloatArray2D dwhp, FloatArray2D dwhc, FloatArray2D dwpl, FloatArray2D dwpr, FloatArray2D dwc, FloatArray dbh, FloatArray dbc, FloatArray dbp)
        {
            mwS  += ( dws  * dws  );
            mwHP += ( dwhp * dwhp );
            mwHC += ( dwhc * dwhc );
            mwpl += ( dwpl * dwpl );
            mwpr += ( dwpr * dwpr );
            mwC  += ( dwc  * dwc  );
            mbP  += ( dbp  * dbp  );
            mbC  += ( dbc  * dbc  );
            mbH  += ( dbh  * dbh  );

            wS  -= ( _learningRate / (mwS  + 1e-8f ).Sqrt()) * dws;
            wHP -= ( _learningRate / (mwHP + 1e-8f ).Sqrt()) * dwhp;
            wHC -= ( _learningRate / (mwHC + 1e-8f ).Sqrt()) * dwhc;
            wpl -= ( _learningRate / (mwpl + 1e-8f ).Sqrt()) * dwpl;
            wpr -= ( _learningRate / (mwpr + 1e-8f ).Sqrt()) * dwpr;
            wC  -= ( _learningRate / (mwC  + 1e-8f ).Sqrt()) * dwc;
            bP  -= ( _learningRate / (mbP  + 1e-8f ).Sqrt()) * dbp;
            bC  -= ( _learningRate / (mbC  + 1e-8f ).Sqrt()) * dbc;
            bH  -= ( _learningRate / (mbH  + 1e-8f ).Sqrt()) * dbh;
        }

        public (FloatArray2D ws, FloatArray2D whp, FloatArray2D whc, FloatArray2D wpl, FloatArray2D wpr, FloatArray2D wc, FloatArray bp, FloatArray bc, FloatArray bh) Params
        {
            get => ( wS, wHP, wHC, wpl, wpr, wC, bP, bC, bH );
            set => ( wS, wHP, wHC, wpl, wpr, wC, bP, bC, bH ) = ( value.ws, value.whp, value.whc, value.wpl, value.wpr, value.wc, value.bp, value.bc, value.bh );
        }
    }
}