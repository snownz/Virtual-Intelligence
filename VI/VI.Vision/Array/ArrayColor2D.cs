using ILGPU;
using ILGPU.Runtime;

namespace VI.Vision.Array
{
	public class ArrayColor2D<T>
		where T : struct
	{
		public ArrayColor2D(int w, int h)
		{
			View = ImageProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
		}

		public ArrayColor2D(T[,] data)
		{
			View = ImageProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
		}

		public ArrayColor2D(MemoryBuffer2D<T> memoryBuffer)
		{
			View = memoryBuffer;
		}

		public MemoryBuffer2D<T> View { get; }

		public T this[int x, int y]
		{
			get => View[new Index2(x, y)];
			set => View[new Index2(x, y)] = value;
		}

		public void Dispose()
		{
			View.Dispose();
		}
	}
}