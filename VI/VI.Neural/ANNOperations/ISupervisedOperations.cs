﻿using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface ISupervisedOperations
    {
        void FeedForward(Array<float> feed);
        void BackWard(Array<float> values);
        void ErrorGradient(Array<float> inputs);
        void UpdateParams();
        void SetLayer(ILayer layer);
    }
}