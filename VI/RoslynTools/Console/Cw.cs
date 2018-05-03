using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoslynTools.Console
{
    public class Cw
    {
        protected static void Write<T>(T msg)
        {
            System.Console.WriteLine(msg);
        }  

        protected static void Clear()
        {
            System.Console.Clear();
        }
        
        protected static void WriteR<T>(T msg)
        {
            System.Console.WriteLine(msg);
            Environment.Exit(0);
        }  
    }
}