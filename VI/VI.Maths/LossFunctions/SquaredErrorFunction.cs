using ILGPU;

namespace VI.Maths.LossFunctions
{
    public class SquaredErrorFunction
    {
        public static void Error(Index position, ArrayView<float> target, ArrayView<float> output, ArrayView<float> desired)
        {
            target[position.X] = desired[position.X] - output[position.X];
        }
    }
}
