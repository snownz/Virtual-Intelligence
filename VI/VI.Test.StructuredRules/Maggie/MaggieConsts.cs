using System;
using VI.Test.StructuredRules.DataTools;

namespace VI.Test.StructuredRules.Maggie
{
    public class MaggieConsts
    {
        public static int GroupValues = Enum.GetNames(typeof(Group)).Length;
        public static int SubGroupValues = Enum.GetNames(typeof(SubGroup)).Length;
        public static int TypeValues = Enum.GetNames(typeof(TypeP)).Length;
        public static int SubTypeValues = Enum.GetNames(typeof(SubType)).Length;
        public static int PackingValues = Enum.GetNames(typeof(Packing)).Length;
        public static int CubeValues = 3;
        public static int QuantityValues = 10;

        //public static int Size = QuantityValues;
        //public static int Size = QuantityValues + GroupValues; 
        public static int Size = QuantityValues + GroupValues + CubeValues;
        //public static int Size = QuantityValues + GroupValues + CubeValues + SubGroupValues;
        //public static int Size = QuantityValues + GroupValues + CubeValues + SubGroupValues + TypeValues;
        //public static int Size = QuantityValues + GroupValues + CubeValues + SubGroupValues + TypeValues + SubTypeValues;
    }
}
