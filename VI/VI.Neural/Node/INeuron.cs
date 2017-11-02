namespace VI.Neural.Node
{
    public interface INeuron
    {
        int Nodes { get; }
        int Connections { get; }
        void Synapsis(int node, int connection);
        void Synapsis(int node, int connection, float w);
    }
}
