using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    /// <summary>
    /// https://www.google.com.br/search?q=squared+error+loss+function&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiS7LncpdrYAhUITJAKHaV4D-YQ_AUICigB&biw=1280&bih=893#imgrc=_3j0NRW4x0TYsM:
    /// </summary>
    public class SquareLossFunction : ILossFunction
    {
        
        public float Loss(Array<float> targets, Array<float> prediction)
        {
            return (1 / targets.Length) * (((prediction - targets).Pow(2).Sum()));
        }

        public float Loss(float[] targets, Array<float> prediction)
        {
            using (var t = new Array<float>(targets))
            {
                return Loss(t, prediction);
            }
        }
    }
}