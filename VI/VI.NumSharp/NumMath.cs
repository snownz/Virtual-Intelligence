﻿using System;
using System.Collections.Generic;
using System.Linq;
using VI.Maths.Random;
using VI.NumSharp.Arrays;

namespace VI.NumSharp
{
	public static class NumMath
	{
		private static readonly ThreadSafeRandom th = new ThreadSafeRandom();
		private static readonly Random           rd = new Random(DateTime.Now.Millisecond);

		public static FloatArray Array(int size)
		{
			return new FloatArray(size);
		}
        
        public static FloatArray Array(float[] data)
		{
			return new FloatArray(data);
		}
		
		public static FloatArray2D Array(int w, int h)
		{
			return new FloatArray2D(w, h);
		}
		
		public static FloatArray2D Array(float[,] data)
		{
			return new FloatArray2D(data);
		}
		
		public static FloatArray2D Random(int w, int h, float factor)
		{
			var arr = new FloatArray2D(w, h);

			for (var x = 0; x < w; x++)
			for (var y = 0; y < h; y++)
				arr[x, y] = th.NextFloat() * factor;

			return arr;
		}

		public static FloatArray Random(int w, float factor)
		{
			var arr = new FloatArray(w);

			for (var x = 0; x < w; x++) arr[x] = th.NextFloat() * factor;

			return arr;
		}

		public static FloatArray2D Repeat(int w, int h, float value)
		{
			var arr = new FloatArray2D(w, h);

			for (var x = 0; x < w; x++)
			for (var y = 0; y < h; y++)
				arr[x, y] = value;

			return arr;
		}
		
		public static ByteArray2D Repeat(int w, int h, byte value)
		{
			var arr = new ByteArray2D(w, h);

			for (var x = 0; x < w; x++)
			for (var y = 0; y < h; y++)
				arr[x, y] = value;

			return arr;
		}

		public static FloatArray Repeat(int w, float value)
		{
			var arr = new FloatArray(w);

			for (var x = 0; x < w; x++) arr[x] = value;

			return arr;
		}
		
		public static FloatArray Max(FloatArray v, FloatArray v1)
		{
			var arr = new FloatArray(v.Length);

			for (var x = 0; x < v.Length; x++) arr[x] = Math.Max(v[x], v1[x]);

			return arr;
		}

		public static IEnumerable<T> Choice<T>(IList<T> sequence, int size, float[] distribution)
		{
			double sum        = 0;
			var    cumulative = distribution.Select(c =>
			{
				var result = c + sum;
				sum        += c;
				return result;
			}).ToList();
			for (var i = 0; i < size; i++)
			{
				var r   = rd.NextDouble();
				var idx = cumulative.BinarySearch(r);
				if (idx < 0)
					idx = ~idx;
				if (idx > cumulative.Count - 1)
					idx = cumulative.Count    - 1;
				yield return sequence[idx];
			}
		}

		public static void Normalize(float p0, float p1, ref FloatArray2D m)
		{
			for (var x = 0; x  < m.W; x++)
			for (var y = 0; y  < m.H; y++)
				m[x, y] = m[x, y] < p0 ? p0 : (m[x, y] > p1 ? p1 : m[x, y]);
		}

		public static void Normalize(float p0, float p1, ref FloatArray v)
		{
			for (var x = 0; x < v.Length; x++)
                v[x] = v[x] < p0 ? p0 : (v[x] > p1 ? p1 : v[x]);
		}
	}
}