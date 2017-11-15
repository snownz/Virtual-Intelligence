using System;
using System.Collections.Generic;
using System.Text;
using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
    public interface ILayer
    {
        Array2D<float> KnowlodgeMatrix { get; set; }
        Array2D<float> GradientMatrix { get; set; }
        Array<float> BiasVector { get; set; }
        Array<float> OutputVector { get; set; }
        Array<float> SumVector { get; set; }
        Array<float> ErrorVector { get; set; }
        Array<float> ErrorWeightVector { get; set; }
        Array<float> DropOutProbability { get; set; }

        int Size { get; set; }
        int ConectionsSize { get; set; }
        float LearningRate { get; set; }
        float CachedLearningRate { get; set; }
        float Momentum { get; set; }
        float CachedMomentum { get; set; }
    }
}
