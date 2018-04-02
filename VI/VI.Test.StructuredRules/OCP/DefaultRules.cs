using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.Algorithm.RecurrentScoringStructure;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.Data;
using VI.Test.StructuredRules.Maggie;

namespace VI.Test.StructuredRules.OCP
{
    public class DefaultRules : IScoring
    {

        private readonly MaggieModel model;

        public DefaultRules(MaggieModel model)
        {
            this.model = model;
        }

        public (float sc, IJoiner value) Score(Node itemA, Node itemB, IList<Node> context, int depth)
        {
            var value = itemA.Value.Join(itemB.Value) as NodeValue;

            value.RecurrentValues = new RecurrentValues();

            var contextData = context.Select(x => x.Value as NodeValue).Select(x => x.RecurrentValues.P).ToList().AsArray();

            var a = (itemA.Value as NodeValue).RecurrentValues.P;
            var b = (itemB.Value as NodeValue).RecurrentValues.P;

            (value.RecurrentValues.P, value.RecurrentValues.S, value.RecurrentValues.X) = model.GetScore(a, b);

            value.RecurrentValues.TargetS = new FloatArray(new[] { 1f });

            return (1f, value);
        }
    }
}
