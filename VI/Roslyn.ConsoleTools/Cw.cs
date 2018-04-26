using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SkiaSharp;

namespace Roslyn.ConsoleTools
{
    public class Cw
    {
        static int[] cColors = { 0x000000, 0x000080, 0x008000, 0x008080, 0x800000, 0x800080, 0x808000, 0xC0C0C0, 0x808080, 0x0000FF, 0x00FF00, 0x00FFFF, 0xFF0000, 0xFF00FF, 0xFFFF00, 0xFFFFFF };
        
        protected void Print<T>(T msg)
        {
            Console.WriteLine(msg);
        }

        protected String Join<T>(string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }        

        public static void WriteImage(SKBitmap source)
        {
            int sMax = 39;
            decimal percent = Math.Min(decimal.Divide(sMax, source.Width), decimal.Divide(sMax, source.Height));
            Size dSize = new Size((int)(source.Width * percent), (int)(source.Height * percent));  

            var bmpMax = new SKBitmap(dSize.Width, dSize.Height);            
            source.Resize(bmpMax, SKBitmapResizeMethod.Lanczos3);

            for (int i = 0; i < dSize.Height; i++)
            {
                for (int j = 0; j < dSize.Width; j++)
                {
                    WritePixel(bmpMax.GetPixel(j * 2, i));
                    WritePixel(bmpMax.GetPixel(j * 2 + 1, i));
                }
                System.Console.WriteLine();
            }
            Console.ResetColor();

            void WritePixel(SKColor cValue)
            {
                Color[] cTable = cColors.Select(x => Color.FromArgb(x)).ToArray();
                char[] rList = new char[] { (char)9617, (char)9618, (char)9619, (char)9608 }; // 1/4, 2/4, 3/4, 4/4
                int[] bestHit = new int[] { 0, 0, 4, int.MaxValue }; //ForeColor, BackColor, Symbol, Score

                for (int rChar = rList.Length; rChar > 0; rChar--)
                {
                    for (int cFore = 0; cFore < cTable.Length; cFore++)
                    {
                        for (int cBack = 0; cBack < cTable.Length; cBack++)
                        {
                            int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
                            int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
                            int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
                            int iScore = (cValue.Red - R) * (cValue.Red - R) + (cValue.Green - G) * (cValue.Green - G) + (cValue.Blue - B) * (cValue.Blue - B);
                            if (!(rChar > 1 && rChar < 4 && iScore > 50000)) // rule out too weird combinations
                            {
                                if (iScore < bestHit[3])
                                {
                                    bestHit[3] = iScore; //Score
                                    bestHit[0] = cFore;  //ForeColor
                                    bestHit[1] = cBack;  //BackColor
                                    bestHit[2] = rChar;  //Symbol
                                }
                            }
                        }
                    }
                }
                Console.ForegroundColor = (ConsoleColor)bestHit[0];
                Console.BackgroundColor = (ConsoleColor)bestHit[1];
                Console.Write(rList[bestHit[2] - 1]);
            }
        }
    }
}