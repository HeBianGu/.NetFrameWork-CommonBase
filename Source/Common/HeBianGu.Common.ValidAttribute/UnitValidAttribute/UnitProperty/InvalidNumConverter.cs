using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HeBianGu.Common.ValidAttribute
{
    public class InvalidNumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null)
            {
                if(value is double d)
                {
                    return double.IsNaN(d) ;
                }
                if(value is float f)
                {
                    return float.IsNaN(f);
                }
                if(value is double[] dd)
                {
                    if(dd.Length>0)
                    {
                        return dd.Any(t => double.IsNaN(t));
                    }
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
