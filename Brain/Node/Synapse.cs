using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Node
{
    public class Synapse
    {
        public double WeightPrior { get; private set; }
        private double _Weight { get; set; }
        public BaseNode ConnectedNode { get; set; }
        public double Weight
        {
            get { return this._Weight; }
            set
            {
                this.WeightPrior = (this._Weight == 0 ? Math.Round(value, 15) : this._Weight);
                this._Weight = Math.Round(value, 15);
            }
        }
    }
}
