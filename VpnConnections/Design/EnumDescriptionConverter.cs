using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace VpnConnections.Design
{
    public class EnumDescriptionConverter : EnumConverter
    {
        private readonly Type enumType;

        public EnumDescriptionConverter(Type type) : base(type)
        {
            enumType = type;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            var name = (string)value;

            foreach (var field in enumType.GetFields())
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if (attribute?.Description == name)
                    return Enum.Parse(enumType, field.Name);
            }

            return string.IsNullOrWhiteSpace(name)
                ? Activator.CreateInstance(enumType)
                : Enum.Parse(enumType, name);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is null)
                return null;

            var name = Enum.GetName(enumType, value);

            if (name is null)
                return value.ToString();

            var field = enumType.GetField(name);
            var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute));

            return attribute == null
                ? value.ToString()
                : attribute.Description;
        }
    }
}