using ILGPU;
using ILGPU.Lightning;
using ILGPU.Lightning.Sequencers;
using ILGPU.ReductionOperations;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;
using ILGPU.ShuffleOperations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace VI.Test.GPU
{
    class Program
    {
        public static void MathKernel(Index2 index, ArrayView2D<float> singleView)
        {
            singleView[index.X, index.Y] = GPUMath.Sqrt(index.X * index.Y);
        }

        static void Reduce(Accelerator accl)
        {
            using (var buffer = accl.Allocate<int>(64))
            {
                using (var target = accl.Allocate<int>(1))
                {
                    accl.Sequence(buffer.View, new Int32Sequencer());

                    // This overload requires an explicit output buffer but
                    // uses an implicit temporary cache from the associated accelerator.
                    // Call a different overload to use a user-defined memory cache.
                    accl.Reduce(
                        buffer.View,
                        target.View,
                        new ShuffleDownInt32(),
                        new AddInt32());

                    accl.Synchronize();

                    var data = target.GetAsArray();
                    for (int i = 0, e = data.Length; i < e; ++i)
                        Console.WriteLine($"Reduced[{i}] = {data[i]}");
                }
            }
        }        
        static void AtomicReduce(Accelerator accl)
        {
            using (var buffer = accl.Allocate<int>(64))
            {
                using (var target = accl.Allocate<int>(1))
                {
                    accl.Sequence(buffer.View, new Int32Sequencer());

                    // This overload requires an explicit output buffer but
                    // uses an implicit temporary cache from the associated accelerator.
                    // Call a different overload to use a user-defined memory cache.
                    accl.AtomicReduce(
                        buffer.View,
                        target.View,
                        new ShuffleDownInt32(),
                        new AtomicAddInt32());

                    accl.Synchronize();

                    var data = target.GetAsArray();
                    for (int i = 0, e = data.Length; i < e; ++i)
                        Console.WriteLine($"AtomicReduced[{i}] = {data[i]}");
                }
            }
        }

        static void Performance()
        {
            using (var context = new Context())
            {
                using (var accelerator = new CudaAccelerator(context))
                {
                    using (var b = accelerator.CreateBackend())
                    {
                        using (var c = accelerator.Context.CreateCompileUnit(b))
                        {
                            var method = typeof(Program).GetMethod("MathKernel", BindingFlags.Static | BindingFlags.Public);
                            var compiled = b.Compile(c, method);

                            var kernel = accelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>>(MathKernel);
                            //var kernel = accelerator.LoadAutoGroupedKernel(compiled);

                            int size = 100000;
                            var W = new[] { 50 };
                            var H = new[] { 50 };

                            for (int n = 0; n < W.Length; n++)
                            {
                                for (int m = 0; m < H.Length; m++)
                                {
                                    int x = W[n];
                                    int y = H[m];

                                    Console.WriteLine($"\n\nW {x}, H {y} \n\n");

                                    //var watch = Stopwatch.StartNew();
                                    //for (int k = 0; k < size; k++)
                                    //{
                                    //    var v = new float[x, y];
                                    //    for (int i = 0; i < x; i++)
                                    //    {
                                    //        for (int j = 0; j < y; j++)
                                    //        {
                                    //            v[i, j] = (float)Math.Sqrt(i * j);
                                    //        }
                                    //    }
                                    //}
                                    //watch.Stop();
                                    //Console.WriteLine($"\n\nElapsed CPU Time Linear: {watch.ElapsedMilliseconds}ms\n");
                                    //GC.Collect();
                                    //
                                    //watch = Stopwatch.StartNew();
                                    //Parallel.For(0, size, k =>
                                    //{
                                    //    var v = new float[x, y];
                                    //    Parallel.For(0, x, i =>
                                    //    {
                                    //        Parallel.For(0, y, j =>
                                    //        {
                                    //            v[i, j] = (float)Math.Sqrt(i * j);
                                    //        });
                                    //    });
                                    //});
                                    //watch.Stop();
                                    //Console.WriteLine($"Elapsed CPU Time Parallel: {watch.ElapsedMilliseconds}ms\n\n");
                                    //GC.Collect();

                                    //var watch = Stopwatch.StartNew();
                                    //for (int k = 0; k < size; k++)
                                    //{
                                    //    var idx = new Index2(x, y);
                                    //    var buffer = accelerator.Allocate<float>(idx);
                                    //    kernel(idx, buffer.View);
                                    //    accelerator.Synchronize();
                                    //    buffer.Dispose();
                                    //}
                                    //watch.Stop();
                                    //Console.WriteLine($"\n\nElapsed GPU Time Linear: {watch.ElapsedMilliseconds}ms\n");
                                    //GC.Collect();

                                    var kn = Enumerable.Repeat(accelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>>(MathKernel), size).ToList();

                                    var watch = Stopwatch.StartNew();
                                    Parallel.For(0, size, k =>
                                    {
                                        var idx = new Index2(x, y);
                                        var buffer = accelerator.Allocate<float>(idx);
                                        //kn[k](idx, buffer.View);
                                        //kernel.Launch(idx, buffer.View);
                                        kernel(idx, buffer.View);
                                        accelerator.Synchronize();
                                        buffer.Dispose();
                                    });
                                    watch.Stop();
                                    Console.WriteLine($"Elapsed GPU Time Parallel: {watch.ElapsedMilliseconds}ms\n\n");
                                    GC.Collect();
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            //Performance();
            ProcessingDevice.Device = DeviceType.CUDA;
            var m0 = NumMath.Random(1000, 1000, 1f);
            var arr = new Array<FloatArray2D>(200);
            var a = m0 * m0;

            Parallel.For(0, 200, i =>
            {
                Console.WriteLine($"Computing {i}");
                arr[i] = m0 * m0;
            });
        }
    }
}
