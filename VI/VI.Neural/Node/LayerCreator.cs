using System;
using VI.Neural.Factory;

namespace VI.Neural.Node
{ 
    public class LayerCreator
    {
        static ILayerFactory _factory = new LayerFactory();
        
        
        public static void SynapseFull(INeuron n)
        {
            for (var i = 0; i < n.NodesSize; i++)
            {
                for (var j = 0; j < n.Connections; j++)
                {
                    Synapse(n, i, j);
                }
            }
        }
        public static void Synapse(INeuron n, int node, int connection)
        {
            n.Synapsis(node, connection);
        }

        public static void SynapseFull(INeuron n, float w)
        {
            for (var i = 0; i < n.NodesSize; i++)
            {
                for (var j = 0; j < n.Connections; j++)
                {
                    Synapse(n, i, j, w);
                }
            }
        }

        public static void Synapse(INeuron n, int node, int connection, float w)
        {
            n.Synapsis(node, connection, w);
        }
        
    }
}
