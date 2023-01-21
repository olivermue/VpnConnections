using System.Drawing.Imaging;
using System.Text;
using ColorConverter = VpnConnections.Design.ColorConverter;

namespace VpnConnections.Drawing
{
    public static class Icons
    {
        public static Icon ConvertToIcon(Bitmap inputBitmap)
        {
            if (inputBitmap is null)
                throw new ArgumentNullException(nameof(inputBitmap));

            var iconStream = new MemoryStream();

            using var memoryStream = new MemoryStream();
            inputBitmap.Save(memoryStream, ImageFormat.Png);

            using (var iconWriter = new BinaryWriter(iconStream, Encoding.Default, true))
            {
                // 0-1 reserved, 0
                iconWriter.Write((byte)0);
                iconWriter.Write((byte)0);

                // 2-3 image type, 1 = icon, 2 = cursor
                iconWriter.Write((short)1);

                // 4-5 number of images
                iconWriter.Write((short)1);

                // image entry 1
                // 0 image width
                iconWriter.Write((byte)inputBitmap.Width);
                // 1 image height
                iconWriter.Write((byte)inputBitmap.Height);

                // 2 number of colors
                iconWriter.Write((byte)0);

                // 3 reserved
                iconWriter.Write((byte)0);

                // 4-5 color planes
                iconWriter.Write((short)0);

                // 6-7 bits per pixel
                iconWriter.Write((short)32);

                // 8-11 size of image data
                iconWriter.Write((int)memoryStream.Length);

                // 12-15 offset of image data
                iconWriter.Write(6 + 16);

                // write image data
                // png data must contain the whole png data file
                iconWriter.Write(memoryStream.ToArray());

                iconWriter.Flush();
            }

            iconStream.Position = 0;
            return new Icon(iconStream);
        }

        public static Icon CreateIcon(string? colorValue, Bitmap foreground, Bitmap background)
        {
            var color = ColorConverter.From(colorValue);

            if (color.A == 0)
            {
                return ConvertToIcon(foreground);
            }
            else
            {
                var coloredBackground = Bitmaps.Colorize(background, color);
                var image = Bitmaps.AlphaBlend(coloredBackground, foreground);

                return ConvertToIcon(image);
            }
        }
    }
}