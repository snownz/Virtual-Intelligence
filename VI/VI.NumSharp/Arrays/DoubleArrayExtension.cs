namespace VI.NumSharp.Arrays
{
	public static class DoubleArrayExtension
	{
		public static DoubleArray ApplyMask(this DoubleArray arr, ByteArray mask)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.ApplyMask(arr.View, mask.View));
		}

		public static DoubleArray2D ApplyMask(this DoubleArray2D arr, ByteArray2D mask)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.ApplyMask(arr.View, mask.View));
		}

		public static DoubleArray Tanh(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Tanh(arr.Cache, arr.View));
		}

		public static DoubleArray Sin(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Sin(arr.Cache, arr.View));
		}

		public static DoubleArray Cos(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Cos(arr.Cache, arr.View));
		}

		public static DoubleArray Pow(this DoubleArray arr, double exp)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Pow(arr.Cache, arr.View, exp));
		}

		public static DoubleArray Exp(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Exp(arr.Cache, arr.View));
		}

		public static DoubleArray Log(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Log(arr.Cache, arr.View));
		}

		public static DoubleArray2D Sqrt(this DoubleArray2D arr)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.Sqrt(arr.Cache, arr.View));
		}

		public static DoubleArray Sqrt(this DoubleArray arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.Sqrt(arr.Cache, arr.View));
		}

		public static DoubleArray SumLine(this DoubleArray2D arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.SumLine(arr.View));
		}

		public static DoubleArray SumColumn(this DoubleArray2D arr)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.SumColumn(arr.View));
		}

		public static double Sum(this DoubleArray arr)
		{
			var sum                                  = 0d;
			for (var i = 0; i < arr.Length; i++) sum += arr[i];
			return sum;
		}
	}
}