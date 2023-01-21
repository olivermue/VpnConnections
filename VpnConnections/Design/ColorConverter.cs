using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace VpnConnections.Design
{
    public class ColorConverter : TypeConverter
    {
        public static Color From(string? color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return Color.Transparent;

            var values = color
                .Split(',')
                .Select(v => int.TryParse(v, out int number) ? number : 0)
                .ToList();

            return values.Count switch
            {
                0 or 1 or 2 => Color.Transparent,
                3 => Color.FromArgb(values[0], values[1], values[2]),
                _ => Color.FromArgb(values[0], values[1], values[2], values[3]),
            };
        }

        public static string? From(Color color)
        {
            return color.A == byte.MaxValue
                ? $"{color.R}, {color.G}, {color.B}"
                : $"{color.A}, {color.R}, {color.G}, {color.B}";
        }

        public static Color From(Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            if (sourceType == typeof(Color))
                return true;

            if (sourceType == typeof(Windows.UI.Color))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            if (destinationType == typeof(Color))
                return true;

            if (destinationType == typeof(Windows.UI.Color))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            switch (value)
            {
                case string text:
                    return From(text);

                case Color color:
                    return color;

                case Windows.UI.Color color:
                    return Color.FromArgb(color.A, color.R, color.G, color.B);

                default:
                    return base.ConvertFrom(context, culture, value);
            }
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            var color = value as Color?;

            if (color.HasValue)
            {
                if (destinationType == typeof(string))
                    return From(color.Value);

                if (destinationType == typeof(Color))
                    return color.Value;

                if (destinationType == typeof(Windows.UI.Color))
                    return Windows.UI.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}