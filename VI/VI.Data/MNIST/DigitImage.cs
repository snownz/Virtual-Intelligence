namespace VI.Data.MNIST
{
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