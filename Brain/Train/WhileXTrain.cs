using Brain.Train.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Train
{
    public static class WhileXTrain
    {
        public static TrainStatistcs TrainError(double min, ISupervisedTrain teacher, InputTrainning[] trainValues, int minEpoch = 0)
        {
            double error = double.MaxValue;
            int i = 0;

            while (error > min || i <= minEpoch)
            {
                error = teacher.RunEpoch(trainValues);
                error /= 4;
                i++;
            }
            return new TrainStatistcs { Epoch = i, Error = error};
        }

        public static TrainStatistcs TrainTxError(double min, ISupervisedTrain teacher, InputTrainning[] trainValues, int minEpoch = 0)
        {
            double error = double.MaxValue;
            double e1 = -1;
            double tx = double.MaxValue;
            int i = 0;

            while (tx > min || i <= minEpoch)
            {
                error = teacher.RunEpoch(trainValues);
                error /= 4;
                if (e1 == -1)
                {
                    e1 = e1 * 2;
                }
                tx = Math.Abs(e1 - error);
                e1 = (e1 * 2.0 + error) / 3.0;
                i++;
            }
            return new TrainStatistcs { Epoch = i, Error = error };
        }
    }

    public class TrainStatistcs
    {
        public double Error { get; set; }
        public int Epoch { get; set; }
    }
}
