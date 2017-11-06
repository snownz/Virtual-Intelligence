using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.Node;
using VI.NumSharp.Array;

namespace VI.Neural.LearningMethods
{
    public interface IAnnSupervisedLearningMethod
    {
        Array<float> Learn(INeuron neuron, Array<float> inputs, float[] error);
        Array<float> Learn(INeuron neuron, float[] inputs, Array<float> error);
        Array<float> Learn(INeuron neuron, Array<float> inputs, Array<float> error);
    }
}
