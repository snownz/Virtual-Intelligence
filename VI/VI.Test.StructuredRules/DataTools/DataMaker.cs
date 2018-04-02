using System;
using System.Collections.Generic;
using System.Linq;

namespace VI.Test.StructuredRules.DataTools
{
    public class DataMaker
    {
        public Dictionary<(int bx0, int bx1), int> GetFrquency(string[][] data,
            Dictionary<int, IList<(string code, Input data)>> newCodes)
        {
            var d = new Dictionary<(int bx0, int bx1), int>();

            for (var i = 0; i < data.Length; i++)
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    var bx0 = newCodes.FirstOrDefault(x => x.Value.Any(y => y.code == data[i][j])).Key;
                    var bx1 = newCodes.FirstOrDefault(x => x.Value.Any(y => y.code == data[i][j + 1])).Key;

                    if (d.ContainsKey((bx0, bx1)))
                        d[(bx0, bx1)]++;
                    else
                        d.Add((bx0, bx1), 1);
                }

            return d;
        }

        public Dictionary<(string bx0, string bx1), int> GetFrquency(string[] data, int maxGroupSize)
        {
            var d1 = new Dictionary<(string bx0, string bx1), int>();

            for (var d = 0; d < data.Length; d++)
                for (var size = 1; size < maxGroupSize; size++)
                {
                    if (size + d > data.Length) continue;

                    var bx0 = "";
                    for (var i = 0; i < size; i++)
                        if (string.IsNullOrEmpty(bx0))
                            bx0 += data[i + d];
                        else
                            bx0 += ";" + data[i + d];

                    for (var size_next = 1; size_next < maxGroupSize; size_next++)
                    {
                        if (size + size_next + d > data.Length) continue;

                        var bx1 = "";
                        for (var i = 0; i < size_next; i++)
                            if (string.IsNullOrEmpty(bx1))
                                bx1 += data[i + size + d];
                            else
                                bx1 += ";" + data[i + size + d];

                        if (d1.ContainsKey((bx0, bx1)))
                            d1[(bx0, bx1)]++;
                        else
                            d1.Add((bx0, bx1), 1);
                    }
                }

            return d1;
        }

        public Dictionary<(int bx0, int bx1), int> GetFrquency(string[] data,
            Dictionary<int, IList<(string code, Input data)>> newCodes)
        {
            var d = new Dictionary<(int bx0, int bx1), int>();

            for (var i = 0; i < data.Length - 1; i++)
            {
                var bx0 = newCodes.FirstOrDefault(x => x.Value.Any(y => y.code == data[i])).Key;
                var bx1 = newCodes.FirstOrDefault(x => x.Value.Any(y => y.code == data[i + 1])).Key;

                if (d.ContainsKey((bx0, bx1)))
                    d[(bx0, bx1)]++;
                else
                    d.Add((bx0, bx1), 1);
            }

            return d;
        }

        public Dictionary<int, IList<(string code, Input data)>> GetSimilar(List<string> pl, IList<ItemDetails> it)
        {
            var result = new Dictionary<int, IList<(string code, Input data)>>();
            for (int i = 0; i < pl.Count; i++)
            {
                var p = pl[i].Split(";").Distinct().ToList();

                for (int j = 0; j < p.Count(); j++)
                {
                    var item = GetInputValueFromCode(p[j], it);
                    if (item == null) continue;
                    if (result.Any(x => x.Value.Any(y => y.data.Equals(item))))
                    {
                        var dc = result.FirstOrDefault(x => x.Value.Any(y => y.data.Equals(item)));
                        if (dc.Value.All(x => x.code != p[j]))
                            dc.Value.Add((p[j], item));
                    }
                    else
                    {
                        if (result.Count == 0)
                        {
                            result.Add(0, new List<(string code, Input data)> { (p[j], item) });
                        }
                        else
                        {
                            result.Add(result.Max(x => x.Key) + 1, new List<(string code, Input data)> { (p[j], item) });
                        }

                    }
                }
            }

            return result;
        }
        
        public int ArrayToInt(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1) return i;
            }

            return 0;
        }

        private int[] IntToArray(int v, int max)
        {
            var a = Enumerable.Repeat(0, max).ToArray();
            a[v] = 1;
            return a;
        }

        private Input GetInputValueFromCode(string code, IList<ItemDetails> it)
        {
            var input = new Input();
            var GroupValues = Enum.GetNames(typeof(Group)).Length;
            var SubGroupValues = Enum.GetNames(typeof(SubGroup)).Length;
            var TypeValues = Enum.GetNames(typeof(TypeP)).Length;
            var SubTypeValues = Enum.GetNames(typeof(SubType)).Length;
            var PackingValues = Enum.GetNames(typeof(Packing)).Length;

            if (it.All(x => x.CdProdutoPromax != code)) return null;

            var item = it.FirstOrDefault(x => x.CdProdutoPromax == code);

            input.Height = item.ComprimentoNormalizado;
            input.Length = item.AlturaNormalizado;
            input.Width = item.LarguraNormalizado;

            input.Group = IntToArray((int)item.Grupo, GroupValues);
            input.SubGroup = IntToArray((int)item.SubGrupo, SubGroupValues);
            input.TypeP = IntToArray((int)item.Type, TypeValues);
            input.SubType = IntToArray((int)item.SubType, SubTypeValues);
            input.Packing = IntToArray((int)item.Packing, PackingValues);

            return input;
        }
    }
}
