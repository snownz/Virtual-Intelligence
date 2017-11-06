using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.Layer;

namespace VI.Neural.Network
{
    public class MLPNetwork
    {
        private int _inputs;
        private ILayer[][] _hiddens;
        private ILayer _outputs;

        public MLPNetwork(int inputs, int outputs, int[][] deepHiddens)
        {
            _inputs = inputs;

        }
    }
}
