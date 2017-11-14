using System;
using System.Collections.Generic;
using System.Linq;
using VI.Data;
using VI.Neural.Node;
using VI.Labs.Models;
using VI.NumSharp;

namespace VI.Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new[] { .9f, .01f, 0f, 0f };
           
            ProcessingDevice.Device = Device.CPU;

            var data = TextFile.Open("kafka.txt");
            (var char_x, var x_char) = data.GetUnique();
            (var dataSize, var vocabSize) = (data.Length, char_x.Count);

            var hidenSize = 100;
            var sequenceLenght = 25;
            var learning_rate = 1e-1f;

          

        }
    }
}
