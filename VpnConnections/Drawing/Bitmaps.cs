namespace VpnConnections.Drawing
{
    public static class Bitmaps
    {
        private const decimal Divisor = byte.MaxValue;

        public static Bitmap AlphaBlend(Bitmap background, Bitmap foreground)
        {
            if (background is null)
                throw new ArgumentNullException(nameof(background));

            if (foreground is null)
                throw new ArgumentNullException(nameof(foreground));

            var bitmap = new Bitmap(background.Width, background.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var backColor = background.GetPixel(i, j);
                    var foreColor = foreground.GetPixel(i, j);

                    var newColor = AlphaBlend(backColor, foreColor);
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            return bitmap;
        }

        public static Bitmap Colorize(Bitmap grayBitmap, Color color)
        {
            if (grayBitmap is null)
                throw new ArgumentNullException(nameof(grayBitmap));

            var bitmap = new Bitmap(grayBitmap);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);

                    if (pixel.A > 0
                        && pixel.R == pixel.G
                        && pixel.G == pixel.B)
                    {
                        var newColor = Color.FromArgb(pixel.A, color);
                        bitmap.SetPixel(i, j, newColor);
                    }
                }
            }

            return bitmap;
        }

        private static Color AlphaBlend(Color backColor, Color foreColor)
        {
            var fa = foreColor.A / Divisor;
            var fr = foreColor.R / Divisor;
            var fg = foreColor.G / Divisor;
            var fb = foreColor.B / Divisor;

            var ba = backColor.A / Divisor;
            var br = backColor.R / Divisor;
            var bg = backColor.G / Divisor;
            var bb = backColor.B / Divisor;

            var a = fa + ba - fa * ba;

            if (a <= 0)
                return Color.Transparent;

            var r = (fa * (1 - ba) * fr + fa * ba * fa + (1 - fa) * ba * br) / a;
            var g = (fa * (1 - ba) * fg + fa * ba * fa + (1 - fa) * ba * bg) / a;
            var b = (fa * (1 - ba) * fb + fa * ba * fa + (1 - fa) * ba * bb) / a;

            return Color.FromArgb(
                (int)(a * byte.MaxValue),
                (int)(r * byte.MaxValue),
                (int)(g * byte.MaxValue),
                (int)(b * byte.MaxValue));
        }
    }
}