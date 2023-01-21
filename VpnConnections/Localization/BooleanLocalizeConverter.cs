using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace VpnConnections.Localization
{
    public class BooleanLocalizeConverter : BooleanConverter
    {
        private static volatile StandardValuesCollection? standardValues;

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string text)
            {
                text = text.Trim();

                if (text == Properties.Resources.ResourceManager.GetString(false.ToString()))
                    return false;

                if (text == Properties.Resources.ResourceManager.GetString(true.ToString()))
                    return true;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is bool boolean
                && destinationType == typeof(string))
            {
                return Localize.Value(boolean.ToString());
            }

            if (value is string text
                && destinationType == typeof(string))
            {
                var values = GetStandardValues(null);
                var isTrue = text == (string)values[1]!;

                value = isTrue ? true.ToString() : false.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
        {
            return standardValues ??= new StandardValuesCollection(new object[]
            {
                Properties.Resources.ResourceManager.GetString(false.ToString()) ?? false.ToString(),
                Properties.Resources.ResourceManager.GetString(true.ToString()) ?? true.ToString(),
            });
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;
    }
}