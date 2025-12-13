using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClient.Converters
{
    public class BoolToAvailabilityConverter : IValueConverter
    {
        int testpull; //pull-request
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool available)
                return available ? "Свободные места: Да" : "Свободные места: Нет";
            return "Свободные места: Нет данных";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
