using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.Algorithm.RecurrentScoringStructure;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.Data;
using VI.Test.StructuredRules;
using VI.Test.StructuredRules.Maggie;

namespace VI.Test.StructuredRules.OCP
{
    public class RulesOCP : IScoring
    {
        private readonly MaggieModel model;

        public RulesOCP(MaggieModel model)
        {
            this.model = model;           
        }
        
        public (float sc, IJoiner value) Score(Node itemA, Node itemB, IList<Node> context, int depth)
        {
            var targetScore = ExecuteOCPRules(itemA, itemB, context);

            var value = itemA.Value.Join(itemB.Value) as NodeValue;

            CreateScoreANNValues(itemA, itemB, context, value, targetScore);

            return (targetScore, value);
        }
        
        private float ExecuteOCPRules(Node itemA, Node itemB, IList<Node> context)
        {
            var nodes = context
                .Select(x => x.Value as NodeValue)
                .Select(x => x.NodeData.Max(y => y.QuantityPercent))
                .OrderByDescending(x => x)
                .ToList();

            var targetScore = 0f;

            var a = (itemA.Value as NodeValue);
            var b = (itemB.Value as NodeValue);

            // Se o primeiro é o de maior quantidade
            if (a.NodeData.Max(y => y.QuantityPercent) == nodes[0])
            {
                // Se o segundo for identico ao primeiro
                if (a.Group.All(x => b.Group.All(y => y == x)))
                {
                    targetScore = 1;
                }
                else
                {
                    // Se ainda existe um item identico
                    var equal = context.Select(x => x.Value as NodeValue)
                        .Where(x => x.Group.Count == 1 && a.Group.Any(y => y == x.Group[0]) && x.Code != itemA.Name);

                    if (equal.Any())
                    {
                        // Se B é o identico
                        if (b.Group.Any(x => a.Group.Any(y => y == x)))
                        {
                            targetScore = 1;
                        }
                        else
                        {
                            targetScore = 0;
                        }
                    }
                    else
                    {
                        // Se B é o segundo maior
                        if (b.NodeData.Max(y => y.QuantityPercent) >= nodes[1])
                        {
                            targetScore = 1f;
                        }
                        else
                        {
                            targetScore = 0;
                        }
                    }
                }
            }
            else
            {
                targetScore = 0;
            }

            return targetScore;
        }

        private void CreateScoreANNValues(Node itemA, Node itemB, IList<Node> context, NodeValue value, float targetScore)
        {
            value.RecurrentValues = new RecurrentValues();

            var contextData = context.Select(x => x.Value as NodeValue).Select(x => x.RecurrentValues.P).ToList().AsArray();

            var a = (itemA.Value as NodeValue).RecurrentValues.P;
            var b = (itemB.Value as NodeValue).RecurrentValues.P;

            (value.RecurrentValues.P, value.RecurrentValues.S, value.RecurrentValues.X) = model.GetScore(a, b);

            value.RecurrentValues.TargetS = new FloatArray(new[] { targetScore });
        }

        private int ArrayToInt(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1) return i;
            }

            return 0;
        }
    }
}
