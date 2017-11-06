using VI.Neural.Layer;

namespace VI.Neural.Node
{
    public interface INeuron
    {
        int NodesSize { get; }
        ILayer Nodes { get; }
        int Connections { get; }
        void Synapsis(int node, int connection);
        void Synapsis(int node, int connection, float w);
    }
}
