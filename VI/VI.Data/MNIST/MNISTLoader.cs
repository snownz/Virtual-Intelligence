using System;
using System.Collections.Generic;
using System.IO;

namespace VI.Data.MNIST
{
    public static class MnistLoader
    {
        public static string DataPath { get; set; }

        public static IList<DigitImage> OpenMnist()
        {
            try
            {
                IList<DigitImage> ret = new List<DigitImage>();
                var ifsLabels =
                    new FileStream($@"{DataPath}\train-labels.idx1-ubyte",
                        FileMode.Open); // test labels
                var ifsImages =
                    new FileStream($@"{DataPath}\train-images.idx3-ubyte",
                        FileMode.Open); // test images

                var brLabels =
                    new BinaryReader(ifsLabels);
                var brImages =
                    new BinaryReader(ifsImages);

                var magic1 = brImages.ReadInt32(); // discard
                var numImages = brImages.ReadInt32();
                var numRows = brImages.ReadInt32();
                var numCols = brImages.ReadInt32();

                var magic2 = brLabels.ReadInt32();
                var numLabels = brLabels.ReadInt32();

                var pixels = new byte[28][];
                for (var i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];

                // each test image
                for (var di = 0; di < 10000; ++di)
                {
                    for (var i = 0; i < 28; ++i)
                        for (var j = 0; j < 28; ++j)
                        {
                            var b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }

                    var lbl = brLabels.ReadByte();

                    var dImage =
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
    }
}