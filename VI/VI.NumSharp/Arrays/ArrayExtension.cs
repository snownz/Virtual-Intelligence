using System.Threading.Tasks;

namespace VI.NumSharp.Arrays
{
	public static class ArrayExtension
	{
		public static FloatArray ApplyMask(this FloatArray arr, ByteArray mask)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.ApplyMask(arr.View, mask.View));
		}

		public static FloatArray2D ApplyMask(this FloatArray2D arr, ByteArray2D mask)
		{
			
			return new FloatArray2D(ProcessingDevice.FloatExecutor.ApplyMask(arr.View, mask.View));
		}

		public static FloatArray Tanh(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Tanh(arr.Cache, arr.View));
		}
		
		public static FloatArray Sin(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Sin(arr.Cache, arr.View));
		}
		
		public static FloatArray Cos(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Cos(arr.Cache, arr.View));
		}
		
		public static FloatArray Pow(this FloatArray arr, float exp)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Pow(arr.Cache, arr.View, exp));
		}

		public static FloatArray Exp(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Exp(arr.Cache, arr.View));
		}

		public static FloatArray Log(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Log(arr.Cache, arr.View));
		}

		public static FloatArray2D Sqrt(this FloatArray2D arr)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.Sqrt(arr.Cache, arr.View));
		}

		public static FloatArray Sqrt(this FloatArray arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.Sqrt(arr.Cache, arr.View));
		}

        public static int Pos(this FloatArray arr, float v)
        {
            int pos = -1;
            Parallel.For(0, arr.Length, i =>
            {
                if (arr[i] == v)
                {
                    pos = i;
                }
            });

            return pos;
        }

        public static FloatArray SumLine(this FloatArray2D arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.SumLine(arr.View));
		}

		public static FloatArray SumColumn(this FloatArray2D arr)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.SumColumn(arr.View));
		}

        public static FloatArray Join(this FloatArray arr0, FloatArray arr1)
        {
            var result = new FloatArray(arr0.Length + arr1.Length);
            var jump = 0;
            Parallel.For(0, arr0.Length, i => result[i + jump] = arr0[i]);
            jump += arr0.Length;
            Parallel.For(0, arr1.Length, i => result[i + jump] = arr1[i]);
            return result;
        }

        public static FloatArray Divide(this FloatArray arr)
        {
            var result = new FloatArray(arr.Length / 2);
            Parallel.For(0, arr.Length, i => result[i] = arr[i] + arr[i + result.Length]);
            return result;
        }

        public static (FloatArray p0, FloatArray p1) Take(this FloatArray arr, int size)
        {
            var p0 = new FloatArray(size);
            var p1 = new FloatArray(arr.Length - size);

            Parallel.For(0, size, i => p0[i] = arr[i]);
            Parallel.For(0, arr.Length - size, i => p0[i] = arr[size + i]);

            return (p0, p1);
        }

        public static float Sum(this FloatArray arr)
		{
			var sum                                  = 0f;
			for (var i = 0; i < arr.Length; i++) sum += arr[i];
			return sum;
		}
	}
}