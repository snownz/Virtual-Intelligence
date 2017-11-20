using System.Threading.Tasks;
using VI.Neural.Node;

namespace VI.Neural.Network
{
    public class MLPNetwork
    {
        private int _inputs;
        private INeuron[][] _hiddens;
        private INeuron _outputs;

        public MLPNetwork(int inputs, int outputs, int[][] deepHiddens)
        {
            _inputs = inputs;

        }

        public void FeedForward()
        {
            foreach (var hiddens in _hiddens)
            {
                Parallel.ForEach(hiddens, hidden =>
                {
                    hidden.Output(new[] {1f});
                });
            }
        }
    }
}
