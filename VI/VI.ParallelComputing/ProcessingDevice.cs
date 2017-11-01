using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.ParallelComputing.ANN;

namespace VI.ParallelComputing
{
    public static class ProcessingDevice
    {
        public static IAnnParallelInterface ArrayDevice { get; set; }
    }
}
