using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Train.Models
{
    public class InputTrainning
    {
        public List<TrainningValues> Values = new List<TrainningValues>();
        public List<Desired> DesiredValues = new List<Desired>();
        public string Class { get; set; }
    }
    public class TrainningValues
    {
        public double? Value { get; set; }
        public string InputName { get; set; }
    }
}
