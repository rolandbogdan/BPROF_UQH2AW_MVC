namespace PcStoreWpfClient.UI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    public class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = (int)value;
            return $"{p} Ft";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string[] input = value.ToString().Split(' ');
                int p = int.Parse(input[0]);
                return p;
            }
            else
            {
                return 0;
            }
        }
    }
}
