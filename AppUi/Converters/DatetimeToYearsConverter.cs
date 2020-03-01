using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppUi.Converters
{
    public class DatetimeToYearsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                var now = DateTime.Now;
                int years = now.Year - date.Year;

                if (now.Month > date.Month)
                    return years;

                if (now.Month == date.Month && now.Day >= date.Day)
                    return years;

                return --years;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
