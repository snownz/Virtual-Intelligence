using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.Data
{
    public class RecurrentValues
    {
        public FloatArray PL { get; set; }
        public FloatArray PR { get; set; }
        public FloatArray P { get; set; }
        public FloatArray S { get; set; }
        public FloatArray C { get; set; }
        public FloatArray R { get; set; }
        public FloatArray H { get; set; }
        public FloatArray TargetS { get; set; }
        public Array<FloatArray> StackedP { get; set; }
    }
}