using System;
using System.Collections.Generic;

namespace VI.Algorithm.BinaryTree
{
    public class Node : ICloneable
    {
        private List<Node> nodes;

        public Node(string name, float sc, int depth, bool isBase = false, bool isMain = true)
        {
            Name = name;
            Score = sc;
            Depth = depth;
            IsMain = isMain;
            IsBase = isBase;
            Id = Guid.NewGuid();
            nodes = new List<Node>();
        }

        public Node(Node nodeA, Node nodeB, float sc, int depth, bool isBase = false, bool isMain = true)
        {
            Name = nodeA.Name + ";" + nodeB.Name;
            NodeA = nodeA;
            NodeB = nodeB;
            Score = sc;
            Depth = depth;
            IsMain = isMain;
            IsBase = isBase;
            Id = Guid.NewGuid();
            nodes = new List<Node>();
            Value = NodeA.Value.Join(NodeB.Value);
        }

        public string Name { get; }
        public Guid Id { get; protected set; }
        public float Score { get; }
        public int Depth { get; }
        public bool IsMain { get; }
        public bool IsBase { get; }

        public Node NodeA { get; set; }
        public Node NodeB { get; set; }

        public void AddNode(Node n)
        {
            nodes.Add(n);
        }

        public IJoiner Value { get; set; }

        public int Count => IsBase ? 1 : NodeA.Count + NodeB.Count;

        public object Clone()
        {
            return new Node(Name, Score, Depth, IsBase, IsMain)
            {
                Id = Id,
                Value = Value,
                NodeA = NodeA,
                NodeB = NodeB
            };
        }
    }
}