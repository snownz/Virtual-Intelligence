using VI.Test.StructuredRules.Maggie;

namespace VI.Test.StructuredRules.DataTools
{
    public class Input
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }

        public int[] Group { get; set; }
        public int[] SubGroup { get; set; }
        public int[] TypeP { get; set; }
        public int[] SubType { get; set; }
        public int[] Packing { get; set; }
        public int[] Ocupation { get; set; }

        public float QuantityPercent { get; set; }
        
        public override bool Equals(object obj)
        {
            var i = obj as Input;

            if (i.Width != Width) return false;
            if (i.Height != Height) return false;
            if (i.Length != Length) return false;

            if (ArrayToInt(Group) != ArrayToInt(i.Group)) return false;
            //if (ArrayToInt(Group) != ArrayToInt(i.Group)) return false;
            //if (ArrayToInt(Group) != ArrayToInt(i.Group)) return false;
            //if (ArrayToInt(Group) != ArrayToInt(i.Group)) return false;
            //if (ArrayToInt(Group) != ArrayToInt(i.Group)) return false;

            return true;
        }


        public float[] AsArray(int percent)
        {
            var quantity = new float[MaggieConsts.QuantityValues];
            quantity[percent] = 1;

            var cube =
                new float[]
                {
                   Height,
                   Width,
                   Length
                };

            var result = new float[MaggieConsts.Size];

            var jump = 0;

            for (int i = 0; i < quantity.Length; i++) result[i + jump] = quantity[i];
            jump += quantity.Length;
            
            for (int i = 0; i < cube.Length; i++) result[i + jump] = cube[i];
            jump += cube.Length;
            
            for (int i = 0; i < MaggieConsts.GroupValues; i++) result[i + jump] = Group[i];
            jump += MaggieConsts.GroupValues;
            
            //for (int i = 0; i < MaggieConsts.SubGroupValues; i++) result[i + jump] = input.SubGroup[i];
            //jump += MaggieConsts.SubGroupValues;
            
            //for (int i = 0; i < MaggieConsts.TypeValues; i++) result[i + jump] = input.TypeP[i];
            //jump += MaggieConsts.TypeValues;
            
            //for (int i = 0; i < MaggieConsts.SubTypeValues; i++) result[i + jump] = input.SubType[i];
            //jump += MaggieConsts.SubTypeValues;
            
            //for (int i = 0; i < MaggieConsts.PackingValues; i++) result[i + jump] = input.Packing[i];

            return result;
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

    public class ItemDetails
    {
        public string CdProdutoPromax { get; set; }
        public string Nome { get; set; }

        public float ComprimentoNormalizado { get; set; }
        public float AlturaNormalizado { get; set; }
        public float LarguraNormalizado { get; set; }

        public Group Grupo { get; set; }
        public SubGroup SubGrupo { get; set; }
        public TypeP Type { get; set; }
        public SubType SubType { get; set; }
        public Packing Packing { get; set; }
    }


    public enum Group
    {
        Alugavel = 0,
        Retornavel = 1,
        Descartavel = 2,
    }

    public enum SubGroup
    {
        Aluminio = 1,
        Lata = 2,
        Garrafa = 3,
        PET = 4,
        Tetrapak = 5,
        Bag = 6,
        BIB = 7,
        Chopp = 8,
        Copo = 9,
        PVC = 10,
    }

    public enum TypeP
    {
        Carbonatado,
        NCarbonatado
    }

    public enum SubType
    {
        Agua,
        BebidaAlcolicaAro,
        Cerveja,
        Energetico,
        Refrigerante,
        Isotonico,
        Suco,
        Cha,
    }

    public enum Packing
    {
        Bandeja,
        Barril,
        Garrafeira,
        Papelao,
        Plastico
    }
}
