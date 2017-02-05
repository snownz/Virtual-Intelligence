using Brain.Train.Models;

namespace Brain.Train
{
    public interface ISupervisedTrain
    {
        /// <summary>
        /// Runs learning iteration.
        /// </summary>
        /// 
        /// <param name="input">Input vector.</param>
        /// <param name="output">Desired output vector.</param>
        /// 
        /// <returns>Returns learning error.</returns>
        /// 
        double Run(InputTrainning input);

        /// <summary>
        /// Runs learning epoch.
        /// </summary>
        /// 
        /// <param name="input">Array of input vectors.</param>
        /// <param name="output">Array of output vectors.</param>
        /// 
        /// <returns>Returns sum of learning errors.</returns>
        /// 
        double RunEpoch(InputTrainning[] inputs);
    }
}
