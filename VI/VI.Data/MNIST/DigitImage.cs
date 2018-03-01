namespace VI.Data.MNIST
{
    public class DigitImage
    {
        public byte label;
        public byte[][] pixels;

        public DigitImage(byte[][] pixels,
            byte label)
        {
            this.pixels = new byte[28][];
            for (var i = 0; i < this.pixels.Length; ++i)
                this.pixels[i] = new byte[28];

            for (var i = 0; i < 28; ++i)
                for (var j = 0; j < 28; ++j)
                    this.pixels[i][j] = pixels[i][j];

            this.label = label;
        }

        public override string ToString()
        {
            var s = "";
            for (var i = 0; i < 28; ++i)
            {
                for (var j = 0; j < 28; ++j)
                    if (pixels[i][j] == 0)
                        s += " ";
                    else if (pixels[i][j] == 255)
                        s += "O";
                    else
                        s += ".";
                s += "\n";
            }

            s += label.ToString();
            return s;
        }
    }
}