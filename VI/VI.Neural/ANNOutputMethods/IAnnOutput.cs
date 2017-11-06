using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.Node;
using VI.NumSharp.Array;

namespace VI.Neural.ANNOutputMethods
{
    public interface IAnnOutput
    {
        Array<float> Output(INeuron node, Array<float> inputs);
        Array<float> Output(INeuron node, float[] inputs);
    }
}
