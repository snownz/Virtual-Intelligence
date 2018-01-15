using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VI.Neural.Layer;

namespace VI.Neural.OptimizerFunction
{
    public class AdagradOptimizerFunction : IOptimizerFunction
    {
        public void CalculateParams(ILayer target)
        {
            throw new NotImplementedException();
        }

        public void UpdateBias(ILayer target)
        {
            throw new NotImplementedException();
        }

        public void UpdateWeight(ILayer target)
        {
            throw new NotImplementedException();
        }
    }
}