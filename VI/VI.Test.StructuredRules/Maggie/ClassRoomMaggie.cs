using System;
using System.Collections.Generic;
using System.Linq;
using VI.Algorithm.BinaryTree;
using VI.Algorithm.RecurrentScoringStructure;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.Data;
using VI.Roslyn.ConsoleTools.Extensions;
using VI.Test.StructuredRules.DataTools;
using VI.Test.StructuredRules.OCP;
using VI.Test.StructuredRules.Tools;

namespace VI.Test.StructuredRules.Maggie
{
    public class ClassRoomMaggie
    {
        //
        static readonly MaggieModel maggie = new MaggieModel();
        static readonly RulesOCP ocpFunc = new RulesOCP(maggie);
        static readonly DefaultRules defaultFunc = new DefaultRules(maggie);
        static readonly RulesMaggie maggieFunc = new RulesMaggie(maggie);

        //
        readonly IList<ItemDetails> _itemsDetails;

        //
        readonly ConstructAlgorithim constructTrain;

        //
        readonly ConstructAlgorithim constructANN;

        //
        private readonly FileWriter fw = new FileWriter();
        private Dictionary<int, IList<(string code, Input data)>> similarbase;

        //
        float lScore = 100;
        
        public ClassRoomMaggie(IList<ItemDetails> itemsDetails)
        {
            _itemsDetails = itemsDetails;

            constructTrain = new ConstructAlgorithim(defaultFunc);
            constructANN = new ConstructAlgorithim(maggieFunc);
        }

        public void Train(IList<string> pallets)
        {
            Console.Clear();

            long cout = 0;

            var maker = new DataMaker();

            // Get list of pallets
            var list = pallets
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x=> x.Length > 5)
                //.Where(x=> x == "838;982;982;982;982;982;982;982;982;982;982;982;982;982;982;982;982;982;982;982")
                .OrderBy(x=>x.Length)
                //.Take(10000)
                .Take(10000)
                .ToList();

            // Make a Similar Data
            similarbase = maker.GetSimilar(list, _itemsDetails);

            // Make a node List
            var data = list
                .Select(x => x.Split(";"))
                .Select(x => x.Select(p =>
                                            new Node(p, 0f, 0, true, false)
                                            {
                                                Value = new NodeValue
                                                {
                                                    Code = p,
                                                    Group = new List<int> { similarbase.FirstOrDefault(y => y.Value.Any(z => z.code == p)).Key },
                                                    NodeData = new List<Input> { similarbase.FirstOrDefault(y => y.Value.Any(z => z.code == p)).Value.First().data }                                              
                                                }
                                            }
                                            ).ToList()
                       )
            .Where(x=>x.Select(y=> (y.Value as NodeValue).Group[0]).Distinct().Count() > 1)
            .ToList();
            
            while (true)
            {
                for (int pl = 0; pl < data.Count; pl++)
                {
                    var nodes = data[pl]
                        .Clone()
                        .ToList();

                    CreateNodes(nodes);
                    
                    var resultGroupedTrain = constructTrain.Construct(nodes.Clone().ToList());
                    var lossScore = maggie.TrainScore(resultGroupedTrain);
                    
                     lScore = 0.999f * lScore + 0.001f * lossScore;

                    Console.Title = $"Loss: {lScore}";

                    if (cout % 100 == 0)
                    {
                        var input = nodes.OrderBy(x => int.Parse(x.Name)).ToList().Clone().ToList();
                        var resultGrouped = constructANN.Construct(input);

                        Console.WriteLine($"Pallet: {pl}");
                        //Console.WriteLine($"Original       Result  :[{string.Join(";", data[pl].Select(x => x.Name).ToList())}]");
                        Console.WriteLine($"Original       Group   :[{string.Join(";", data[pl].Select(x => x.Value as NodeValue).Select(x=>x.Group[0]).ToList())}]");
                        //Console.WriteLine($"Maggie Teacher Result  :[{resultGroupedTrain.Name}]");
                        Console.WriteLine($"Maggie Teacher Group   :[{string.Join(";", resultGroupedTrain.Name.Split(";").Select(x => similarbase.FirstOrDefault(y => y.Value.Any(z => z.code == x)).Key).ToList())}]");
                        //Console.WriteLine($"Maggie         Result  :[{resultGrouped.Name}]");
                        Console.WriteLine($"Maggie         Group   :[{string.Join(";", resultGrouped.Name.Split(";").Select(x => similarbase.FirstOrDefault(y => y.Value.Any(z => z.code == x)).Key).ToList())}]");

                        Console.WriteLine("\n\n");
                    }
                    cout++;
                }
            }
        }

        private void CreateNodes(List<Node> nodes)
        {
            var quanti = nodes.Select(x => x.Value as NodeValue).Select(x => x.Group.First()).GroupBy(x => x);
            var sum = nodes.Count();
            var percents = quanti.Select(x => (x.Key, (float)x.Count() / (float)sum)).OrderByDescending(x => x.Item2).ToList();

            foreach (var n in nodes)
            {
                var nd = (n.Value as NodeValue);
                nd.NodeData[0].QuantityPercent = percents.FirstOrDefault(x => x.Item1 == (n.Value as NodeValue).Group.First()).Item2;
                var loc = percents.IndexOf(percents.FirstOrDefault(x => x.Item1 == (n.Value as NodeValue).Group.First()));
                nd.RecurrentValues = new RecurrentValues
                {
                    P = new  FloatArray(nd.NodeData[0].AsArray(loc)),
                };
            }
        }
    }
}
