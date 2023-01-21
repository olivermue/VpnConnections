using System.ComponentModel;

namespace VpnConnections.Localization
{
    public class CategoryLocalizedAttribute : CategoryAttribute
    {
        public CategoryLocalizedAttribute(string category)
            : base(Localize.Value(category))
        {
        }
    }
}