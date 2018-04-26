namespace VI.Neural.Factory
{
    /// <summary>
    /// (Supervised)   LayerCreatorSupervised
    /// (Output) LayerCreatorOutput
    /// </summary>
    public class LayerCreatorSupervised
    {
        private int size;
        private int connections;
        private float lr;
        private float mo;

        public LayerCreatorSupervised(int size, int connections, float lr, float mo)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
        }

        public LayerCreatorHidden Hidden_f()
        {
            return new LayerCreatorHidden(size, connections, lr, mo);
        }

        public LayerCreatorOutput Output_f()
        {
            return new LayerCreatorOutput(size, connections, lr, mo);
        }
    }
}