using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI.Cognitive.Node
{
    public interface INeuron
    {
        int Nodes { get; }
        int Connections { get; }
        void Synapsis(int node, int connection);
    }
}
