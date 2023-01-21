using System.ComponentModel;

namespace VpnConnections.Localization
{
    public class DescriptionLocalizedAttribute : DescriptionAttribute
    {
        public DescriptionLocalizedAttribute(string name)
            : base(Localize.Value(name))
        {
        }
    }
}