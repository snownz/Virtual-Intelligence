using System.Collections.Generic;
using VI.Algorithm.BinaryTree;

namespace VI.Algorithm.RecurrentScoringStructure
{
    public interface IScoring
    {
        (float sc, IJoiner value) Score(Node itemA, Node ItemB, IList<Node> context, int depth);
    }
}