using System;
using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.NumSharp.Prototypes.Data;
using VI.Test.StructuredRules.DataTools;

namespace VI.Test.StructuredRules.Maggie
{
    public class NodeValue : IJoiner
    {
        public string Code { get; set; }
        public List<int> Group { get; set; }
        public List<Input> NodeData { get; set; }
        public RecurrentValues RecurrentValues { get; set; }

        public NodeValue()
        {
            Group = new List<int>();
            NodeData = new List<Input>();
        }

        public IJoiner Join(IJoiner B)
        {
            if (B is NodeValue)
            {
                var b = B as NodeValue;

                if (b.Group.Count == 1 && Group.Contains(b.Group[0]))
                {
                    return new NodeValue
                    {
                        Code = Code + ";" + b.Code,
                        Group = Group,
                        NodeData = NodeData
                    };
                }
                else
                {
                    var g = new List<int>();
                    g.AddRange(Group);
                    g.AddRange(b.Group);

                    var i = new List<Input>();
                    i.AddRange(NodeData);
                    i.AddRange(b.NodeData);

                    return new NodeValue
                    {
                        Code = Code + ";" + b.Code,
                        Group = g.Distinct().ToList(),
                        NodeData = i.Distinct().ToList()
                    };
                }
            }
            throw new NotImplementedException();
        }
    }
}
