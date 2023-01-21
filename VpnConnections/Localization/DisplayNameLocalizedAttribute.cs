using System.ComponentModel;

namespace VpnConnections.Localization
{
    public class DisplayNameLocalizedAttribute : DisplayNameAttribute
    {
        public DisplayNameLocalizedAttribute(string displayName)
            : base(Localize.Value(displayName))
        {
        }
    }
}