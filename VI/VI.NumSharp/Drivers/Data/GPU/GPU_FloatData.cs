using ILGPU;
using ILGPU.Runtime;
using System.Linq;

namespace VI.NumSharp.Drivers.Data.GPU
{
    public class GPU_FloatData : IFloatData
    {
        private MemoryBuffer<float> _view;

        public GPU_FloatData()
        {
        }

        public GPU_FloatData(int size)
        {
            _view = ILGPUMethods.Allocate<float>(size);
            AxesX = Enumerable.Range(0, size).ToArray();
        }

        public GPU_FloatData(MemoryBuffer<float> data)
        {
            _view = data;
            AxesX = Enumerable.Range(0, data.Length).ToArray();
        }

        public GPU_FloatData(float[] data)
        {
            _view = ILGPUMethods.Allocate(data);
            AxesX = Enumerable.Range(0, data.Length).ToArray();
        }

        public ArrayView<float> MemoryView => _view.View;

        public float this[int x]
        {
            get => _view[x];
            set => _view[x] = value;
        }

        public int[] AxesX { get; }

        public float[] AsArray()
        {
            return _view.GetAsArray();
        }

        public int Length => _view.Length;

        public float[] View { get => AsArray(); set => _view = ILGPUMethods.Allocate<float>(value); }

        public float[] Clone()
        {
            return ILGPUMethods.Clone(_view).GetAsArray();
        }
    }
}