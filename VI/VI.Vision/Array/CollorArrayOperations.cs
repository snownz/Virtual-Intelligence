using ILGPU;
using VI.Vision.SpaceColor;

namespace VI.Vision.Array
{
	public class colorArrayOperations
	{
		public static void reversed_heat_map(Index2 size, ArrayView2D<HSV> output, ArrayView2D<float> data, float max)
		{
			var x = size.X;
			var y = size.Y;

			var c  = 270f            * data[x, y] / max;
			var c1 = 1f - data[x, y] / max;

			var color        = new HSV();
			color.Hue        = c;
			color.Saturation = c1;
			color.Value      = c1;

			output[x, y] = color;
		}

		//TODO SIMD in this operation
		public static void hsv_2_rgb(Index2 size, ArrayView2D<RGB> output, ArrayView2D<HSV> data)
		{
			var x = size.X;
			var y = size.Y;

			var hue        = data[x, y].Hue;
			var saturation = data[x, y].Saturation;
			var value      = data[x, y].Value;

			var hi = hue / 60       % 6;
			var f  = hue / 60 - hue / 60;

			value = value * 255;
			var v = (int) value;
			var p = (int) (value * (1 - saturation));
			var q = (int) (value * (1 - f       * saturation));
			var t = (int) (value * (1 - (1 - f) * saturation));

			var color = new RGB();

			if (hi == 0)
			{
				color.Red   = v;
				color.Green = t;
				color.Blue  = p;
			}
			else
			{
				if (hi == 1)
				{
					color.Red   = q;
					color.Green = v;
					color.Blue  = p;
				}
				else
				{
					if (hi == 2)
					{
						color.Red   = p;
						color.Green = v;
						color.Blue  = t;
					}
					else
					{
						if (hi == 3)
						{
							color.Red   = p;
							color.Green = q;
							color.Blue  = v;
						}
						else
						{
							if (hi == 4)
							{
								color.Red   = t;
								color.Green = p;
								color.Blue  = v;
							}
							else
							{
								color.Red   = t;
								color.Green = p;
								color.Blue  = v;
							}
						}
					}
				}
			}

			output[x, y] = color;
		}
	}
}