using System.Collections.Generic;
using System.Linq;
using RoslynTools.Extensions;

namespace VI.Algorithm.BinaryTree
{
    public static class NodeExtension
    {
        public static List<Node> Union(this List<Node> ls, List<Node> next)
        {
            var l = ls.Clone().ToList();
            var n = next.Clone().ToList();
            l.AddRange(n);
            return l;
        }
    }
}