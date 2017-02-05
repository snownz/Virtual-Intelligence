using Brain.Learning.Interface;
using System.Drawing;

namespace Brain.Node.Interface
{
    public interface IDistanceNode
    {
        double LearningRadius { get; set; }
        double SquaredRadius2 { get; set; }
        IDistanceNode[,] Neighbors { get; }
        IDistanceNode[,] NetLayer { get; set; }
        bool Updated { get; set; }
        Point Position { get; set; }
    }
}
