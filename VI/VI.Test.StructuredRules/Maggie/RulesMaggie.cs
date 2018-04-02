using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.Algorithm.RecurrentScoringStructure;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.Data;

namespace VI.Test.StructuredRules.Maggie
{
    public class RulesMaggie : IScoring
    {
        private readonly MaggieModel model;
        private readonly FloatArray rule;

        public RulesMaggie(MaggieModel model)
        {
            this.model = model;
            rule = new FloatArray(MaggieConsts.Size);
            for (int i = 0; i < MaggieConsts.QuantityValues; i++)
            {
                rule[i] = 1;
            }
        }

        public (float sc, IJoiner value) 
            Score(Node itemA, Node itemB, IList<Node> context, int depth)
        {
            var value = itemA.Value.Join(itemB.Value) as NodeValue;
            value.RecurrentValues = new RecurrentValues();

            var contextData = context.Select(x => x.Value as NodeValue).Select(x => x.RecurrentValues.P).ToList().AsArray();
            
            var a = (itemA.Value as NodeValue).RecurrentValues.P * rule;
            var b = (itemB.Value as NodeValue).RecurrentValues.P * rule;

            (value.RecurrentValues.P, value.RecurrentValues.S, value.RecurrentValues.X) = model.GetScore(a, b);

            return (value.RecurrentValues.S[0], value);
        }        
    }
}
