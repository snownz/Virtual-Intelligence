using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.Node;
using VI.NumSharp.Arrays;

namespace VI.Neural.LearningMethods
{
    public interface IAnnUnsupervisedLearningMethod
    {
        Array<float> Learn(INeuron neuron, Array<float> inputs);
        Array<float> Learn(INeuron neuron, float[] inputs);
    }
}
