using System;
using VI.NumSharp.Arrays;

namespace VI.Neural.Drivers.Executors
{
    public class CpuOptimizationExecutor : IOptimizationExecutor
    {
        public void Adadelta(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate)
        {
            for(int x = 0; x < param.W; x++)
            {
                for(int y = 0; y < param.H; y++)
                {
                    m[x, y] = ( 0.001f * m[x,y] ) + ( 0.999f * ( grad[x, y] * grad[x, y] ) );
                    float mid = -( ( (float)Math.Sqrt( v[x, y] + 0.00000001f ) / (float)Math.Sqrt( m[x,y] + 0.00000001f ) ) * grad[x, y] );
                    v[x, y] = ( 0.001f * v[x, y] ) + ( 0.999f * ( mid * mid ) );
                    param[x, y] -= mid;
                }
            }
        }
        public void Adadelta(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate)
        {
            for(int x = 0; x < param.Length; x++)
            {
                m[x] = ( 0.001f * m[x] ) + ( 0.999f * ( grad[x] * grad[x] ) );
                float mid = -( ( (float)Math.Sqrt( v[x] + 0.00000001f ) / (float)Math.Sqrt( m[x] + 0.00000001f ) ) * grad[x] );
                v[x] = ( 0.001f * v[x] ) + ( 0.999f * ( mid * mid ) );
                param[x] -= mid;
            }
        }

        public void AdaGrad(FloatArray2D param, FloatArray2D grad, FloatArray2D m, float learningRate)
        {
            for(int x = 0; x < param.W; x++)
            {
                for(int y = 0; y < param.H; y++)
                {
                    m[x, y] += ( grad[x, y] * grad[x, y] );
                    param [x, y] -= ( ( learningRate / (float)Math.Sqrt( m[x, y] + 1e-8f ) ) *  grad[x, y]  );
                }
            }
        }
        public void AdaGrad(FloatArray param, FloatArray grad, FloatArray m, float learningRate)
        {
            for(int x = 0; x < param.Length; x++)
            {
                m[x] += ( grad[x] * grad[x] );
                param [x] -= ( ( learningRate / (float)Math.Sqrt( m[x] + 1e-8f ) ) *  grad[x]  );
            }
        }

        public void Adam(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate)
        {
            for(int x = 0; x < param.W; x++)
            {
                for(int y = 0; y < param.H; y++)
                {
                    m[x, y]  = ( 0.9f   * m[x, y] )  + ( 0.1f   * grad[x, y] );
                    v[x, y]  = ( 0.999f * v[x, y] )  + ( 0.001f * ( grad[x, y] * grad[x, y] ) );
                    var Adam_m_ws_hat  = m[x, y]  /  0.1f;
                    var Adam_v_ws_hat  = v[x, y]  /  0.001f;
                    param[x, y] -= ( learningRate / ( (float)Math.Sqrt( Adam_v_ws_hat ) + 1e-8f ) ) * Adam_m_ws_hat;
                }
            }
        }
        public void Adam(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate)
        {
            for(int x = 0; x < param.Length; x++)
            {
                m[x]  = ( 0.9f   * m[x] )  + ( 0.1f  * grad[x] );
                v[x]  = ( 0.999f * v[x] )  + ( 0.001f * ( grad[x] * grad[x] ) );
                var Adam_m_ws_hat  = m[x]  / 0.1f;
                var Adam_v_ws_hat  = v[x]  / 0.001f;
                param[x] -= ( learningRate / ( (float)Math.Sqrt( Adam_v_ws_hat ) + 1e-8f ) ) * Adam_m_ws_hat;
            }
        }

        public void Nadam(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate)
        {
            for(int x = 0; x < param.W; x++)
            {
                for(int y = 0; y < param.H; y++)
                {
                    m[x, y]  = ( 0.9f   * m[x, y] )  + ( 0.1f   * grad[x, y] );
                    v[x, y]  = ( 0.999f * v[x, y] )  + ( 0.001f * ( grad[x, y] * grad[x, y] ) );
                    var Adam_m_ws_hat  = m[x, y]  /  0.1f;
                    var Adam_v_ws_hat  = v[x, y]  /  0.001f;
                    param[x, y] -= ( learningRate / ( (float)Math.Sqrt(Adam_v_ws_hat) + 1e-8f ) ) * ( 0.9f * Adam_m_ws_hat + ( ( ( 1 - 0.9f ) * grad[x, y] ) / ( 1 - 0.9f ) ) );
                }
            }
        }
        public void Nadam(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate)
        {
            for(int x = 0; x < param.Length; x++)
            {
                m[x]  = ( 0.9f   * m[x] )  + ( 0.1f  * grad[x] );
                v[x]  = ( 0.999f * v[x] )  + ( 0.001f * ( grad[x] * grad[x] ) );
                var Adam_m_ws_hat  = m[x]  / 0.1f;
                var Adam_v_ws_hat  = v[x]  / 0.001f;
                param[x] -= ( learningRate / ( (float)Math.Sqrt(Adam_v_ws_hat) + 1e-8f ) ) * ( 0.9f * Adam_m_ws_hat + ( ( ( 1 - 0.9f ) * grad[x] ) / ( 1 - 0.9f ) ) );
            }
        }

        public void RMSProp(FloatArray2D param, FloatArray2D grad, FloatArray2D m, float learningRate)
        {
            for(int x = 0; x < param.W; x++)
            {
                for(int y = 0; y < param.H; y++)
                {
                    m[x, y] = (0.999f * m[x, y]) + (0.001f * ( grad[x, y] * grad[x, y] ) );
                    param [x, y] -= ( ( learningRate / (float)Math.Sqrt( m[x, y] + 1e-8f ) ) *  grad[x, y]  );
                }
            }
        }
        public void RMSProp(FloatArray param, FloatArray grad, FloatArray m, float learningRate)
        {
            for(int x = 0; x < param.Length; x++)
            {
                m[x] = (0.999f * m[x]) + (0.001f * ( grad[x] * grad[x] ) );
                param [x] -= ( ( learningRate / (float)Math.Sqrt( m[x] + 1e-8f ) ) *  grad[x]  );
            }
        }    
    }
}