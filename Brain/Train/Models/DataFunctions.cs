using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Train.Models
{
    public static class DataFunctions
    {
        static Random rnd = new Random();
        public static void RandomDate(this IList<InputTrainning> trData)
        {
            for (var i = 0; i < trData.Count; i++)
            {
                var a = rnd.Next(trData.Count);
                object temp = trData[i];
                trData[i] = trData[a];
                trData[a] = (InputTrainning)temp;
            }
        }
    }
}
