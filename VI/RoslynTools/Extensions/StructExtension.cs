using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RoslynTools.Extensions
{
    public static class StructExtension
    {
        public static float SmoothAdd(this float f, float value)
        {
            return (.999f * f) + (.001f * value);
        }
    }
}