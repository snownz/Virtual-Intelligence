using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ANNOperations;
using VI.Neural.Layer;
using VI.Neural.Node;
using VI.NumSharp.Array;

namespace VI.Neural.LearningMethods
{
    public class AnnAdaGradHiddenOperations : IAnnSupervisedLearningMethod
    {
        private readonly AnnBasicOperations _ann;

        public AnnAdaGradHiddenOperations(AnnBasicOperations ann)
        {
            _ann = ann;
        }

        public Array<float> Learn(INeuron neuron, Array<float> inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                return null;
            }
        }
        public Array<float> Learn(INeuron neuron, float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                return null;
            }
        }
        public Array<float> Learn(INeuron neuron, Array<float> inputs, Array<float> error)
        {
            return null;
        }
        
        public void AdaGrad(ILayer target, Array2D<float> gradient, Array2D<float> mem)
        {
            target.GradientMatrix = -target.LearningRate * gradient / (mem + 1e-8f).Sqrt();
        }
    }
}
