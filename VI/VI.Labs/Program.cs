using System;
using System.Collections.Generic;
using VI.Neural.Node;
using VI.NumSharp;
using VI.Neural.LossFunction;
using System.IO;
using VI.NumSharp.Arrays;

namespace VI.Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            var rd = new Random();
            var values = new[] { .3f, .000f };

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessingDevice.Device = Device.CPU;

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var hiddens = new LayerBuilder(15, 784, values[0])
                .Supervised()
                .WithSigmoid()
                .Hidden()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse()
                .Build();

            var hiddens2 = new LayerBuilder(10, 15, values[0])
                .Supervised()
                .WithSigmoid()
                .Hidden()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse()
                .Build();

            var outputs = new LayerBuilder(10, 10, values[0])
                .Supervised()
                .WithSigmoid()
                .Output()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse()
                .Build();

            var loss = new SquareLossFunction();

            watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            var trainingValues = OpenMnist();            

            int cont = 0;
            int sizeTrain = (int)(trainingValues.Count * .9);
     
            var e = double.MaxValue;

            while (true)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                e = 0;
                for (int i = 0; i < sizeTrain; i++)
                {
                    var index = i; //= rd.Next(0, trainingValues.Count);

                    Console.WriteLine(trainingValues[index].ToString());
                    var inputs = ByteToArray(trainingValues[index].pixels, 28, 28);
                    var desireds = ByteToArray(trainingValues[index].label, 10);

                    //watch = System.Diagnostics.Stopwatch.StartNew();
                    // Feed Forward
                    var _h = hiddens.Output(inputs);
                    var _h2 = hiddens2.Output(_h);
                    var _o = outputs.Output(_h2);
                    //watch.Stop();
                    //Console.WriteLine($"\nForward Time: { watch.ElapsedMilliseconds}ms");
                    //Thread.Sleep(100);

                    //watch = System.Diagnostics.Stopwatch.StartNew();
                    // Backward
                    var _oe = ((ISupervisedLearning) outputs).Learn(_h2, desireds);
                    var _he2 = ((ISupervisedLearning) hiddens2).Learn(_h, _oe);
                    ((ISupervisedLearning) hiddens).Learn(inputs, _he2);
                    //watch.Stop();
                    //Console.WriteLine($"\nBackward Time: { watch.ElapsedMilliseconds}ms");

                    //Console.WriteLine("Desired: " +ArrayToInt(desireds, 10));
                    //Console.WriteLine("Output: " + ArrayToInt(_o, 10));
                    //PrintArray(_oe, 5);
                    PrintArray(_o, 10);
                    Console.WriteLine("\n");

                    // Error
                    var e0 = Math.Abs(_o[0] - desireds[0]);
                    var e1 = Math.Abs(_o[1] - desireds[1]);
                    var error = Math.Sqrt(Math.Abs(e0 * e0 + e1 * e0));
                    e += error / 2.0;
                }


                e /= sizeTrain;
                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;
                Console.WriteLine($"Interactions: {cont}\nError: {e}");
                //Console.WriteLine($"Interactions: {cont}\nError: {e}\nTime: {time / (double)sizeTrain}ms");
                Console.Title =
                    $"Error: {e} --- TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double) time / (double)sizeTrain))}";
            }
        }

        private static float[] ByteToArray(byte b, int range)
        {
            var f = Convert.ToInt32(b);
            var br = new float[range];
            br[f] = 1;
            return br;
        }

        private static void PrintArray(float[] b, int range)
        {
            var str = "[";
            for (int i = 0; i < range-1; i++)
            {
                str += $"{Math.Round(b[i], 2)}, ";
            }
            str += Math.Round(b[range - 1], 2) + "] = " + ArrayToInt(b, range);
            Console.WriteLine(str);
        }

        private static void PrintArray(Array<float> b, int range)
        {
            var str = "[";
            for (int i = 0; i < range - 1; i++)
            {
                str += $"{b[i]}, ";
            }
            str += b[range - 1] + "] = " + ArrayToInt(b,range);
            Console.WriteLine(str);
        }

        private static int ArrayToInt(Array<float> b, int range)
        {
            int r = 0;
            float max = 0;
            for (int i = 0; i < range; i++)
            {
                if(b[i] > max)
                {
                    max = b[i];
                    r = i;
                }
            }
            return r;
        }

        private static int ArrayToInt(float[] b, int range)
        {
            int r = 0;
            float max = 0;
            for (int i = 0; i < range; i++)
            {
                if (b[i] > max)
                {
                    max = b[i];
                    r = i;
                }
            }
            return r;
        }

        private static float[] ByteToArray(byte[][] b, int rangeX, int rangeY)
        {
            var f = new float[rangeX * rangeY];

            for (int x = 0; x < rangeX; x++)
            {
                for (int y = 0; y < rangeY; y++)
                {
                    f[x + y * rangeX] = (b[x][y] / 255f);
                }
            }
            return f;
        }

        public static IList<DigitImage> OpenMnist()
        {
            try
            {
                IList<DigitImage> ret = new List<DigitImage>();
                FileStream ifsLabels =
                                 new FileStream(@"C:\Users\lucas.fernandes\Downloads\Img\train-labels.idx1-ubyte",
                                 FileMode.Open); // test labels
                FileStream ifsImages =
                                 new FileStream(@"C:\Users\lucas.fernandes\Downloads\Img\train-images.idx3-ubyte",
                                 FileMode.Open); // test images

                BinaryReader brLabels =
                                new BinaryReader(ifsLabels);
                BinaryReader brImages =
                                new BinaryReader(ifsImages);

                int magic1 = brImages.ReadInt32(); // discard
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();

                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];

                // each test image
                for (int di = 0; di < 10000; ++di)
                {
                    for (int i = 0; i < 28; ++i)
                    {
                        for (int j = 0; j < 28; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }

                    byte lbl = brLabels.ReadByte();

                    DigitImage dImage =
                      new DigitImage(pixels, lbl);
                    
                    ret.Add(dImage);
                } // each image

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();

                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public class DigitImage
        {
            public byte[][] pixels;
            public byte label;

            public DigitImage(byte[][] pixels,
              byte label)
            {
                this.pixels = new byte[28][];
                for (int i = 0; i < this.pixels.Length; ++i)
                    this.pixels[i] = new byte[28];

                for (int i = 0; i < 28; ++i)
                    for (int j = 0; j < 28; ++j)
                        this.pixels[i][j] = pixels[i][j];

                this.label = label;
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        if (this.pixels[i][j] == 0)
                            s += " ";
                        else if (this.pixels[i][j] == 255)
                            s += "O"; 
                        else
                            s += "."; 
                    }
                    s += "\n";
                }
                s += this.label.ToString();
                return s;
            }
        }

    }
}
