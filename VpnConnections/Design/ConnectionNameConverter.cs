using System.ComponentModel;
using VpnConnections.Vpn;

namespace VpnConnections.Design
{
    public class ConnectionNameConverter : StringConverter
    {
        public override StandardValuesCollection? GetStandardValues(ITypeDescriptorContext? context)
        {
            return new StandardValuesCollection(VpnConnection.ConnectionNames.ToList());
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;
    }
}