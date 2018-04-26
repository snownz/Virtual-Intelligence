using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.Roslyn.ConsoleTools.Extensions;

namespace VI.Algorithm.RecurrentScoringStructure
{
    public class ConstructAlgorithim
    {
        private readonly IScoring _score;

        public ConstructAlgorithim(IScoring func)
        {
            _score = func;
        }

        public Node ConstructDesiredTarget(List<Node> item)
        {
            int depth = 0;

            while (item.Count > 1)
            {
                // Create Scores
                var scores = CreateDesiredScores(item, depth);

                // Create a new Tree Layer
                var newLayer = SelectDesiredGroups(scores, item, depth);

                // Move to next layer
                item = newLayer;
                depth++;
            }
            return item[0];
        }

        public Node Construct(List<Node> item)
        {
            int depth = 0;

            while (item.Count > 1)
            {
                // Create Scores
                var scores = CreateScores(item, depth);

                // Create a new Tree Layer
                var newLayer = SelectGroups(scores, item, depth);

                // Move to next layer
                item = newLayer;
                depth++;
            }
            return item[0];
        }

        private List<Node> CreateDesiredScores(List<Node> items, int depth)
        {
            // List Results
            var resultNodes = new List<Node>();

            // Create Scores foreach combination
            for (int i = 0; i < items.Count - 1; i++)
            {
                var j = i + 1;

                // Get nodes
                var itemA = items[i];
                var itemB = items[j];

                // Get Score
                (var sc, var obj) = _score.Score(itemA, itemB, items, depth);

                // Create Node
                resultNodes.Add(new Node(itemA, itemB, sc, depth) { Value = obj });
            }
            return resultNodes;
        }

        private List<Node> CreateScores(List<Node> items, int depth)
        {
            // List Results
            var resultNodes = new List<Node>();

            // Create Scores foreach combination
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < items.Count; j++)
                {
                    if (i == j) continue;

                    // Get nodes
                    var itemA = items[i];
                    var itemB = items[j];

                    // Get Score
                    (var sc, var obj) = _score.Score(itemA, itemB, items, depth);

                    // Create Node
                    resultNodes.Add(new Node(itemA, itemB, sc, depth) { Value = obj });
                }
            }
            return resultNodes;
        }

        private List<Node> SelectDesiredGroups(List<Node> resultNodes, List<Node> prior, int depth)
        {
            // Get Winner
            var winner = resultNodes.First();

            // Delete all from prior
            var include = prior.Where(x => x.Id != winner.NodeA.Id && x.Id != winner.NodeB.Id)
                .ToList()
                .Clone();

            var result = new List<Node>();
            result.Add(winner);
            result.AddRange(include);

            return result;
        }

        private List<Node> SelectGroups(List<Node> resultNodes, List<Node> prior, int depth)
        {
            // Get Winner
            var winner = resultNodes.OrderByDescending(x => x.Score).First();

            // Delete all from prior
            var include = prior.Where(x => x.Id != winner.NodeA.Id && x.Id != winner.NodeB.Id)
                .ToList()
                .Clone();

            var result = new List<Node>();
            result.Add(winner);
            result.AddRange(include);

            return result;
        }
    }
}