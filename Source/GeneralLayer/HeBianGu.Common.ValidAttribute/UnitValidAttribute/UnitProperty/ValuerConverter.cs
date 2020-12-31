using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HeBianGu.Common.ValidAttribute
{
    class InputPowerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
                return (new PowerValue(data)).ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToPowerValue(data,out string message);
                    if (d != null) return d.OriginalValue;
                }
            }

            return double.NaN;
        }
    }

    class InputPowerValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PowerValue data)
                return data.ToString();
            return new PowerValue(double.NaN);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToPowerValue(data,out string message);
                    if (d != null) return d;
                }
            }
            

            return new PowerValue(double.NaN);
        }
    }
    class InputFreqConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(double.TryParse(value?.ToString(), out double data))
            {
                return ValueConvertHelper.GetFreqValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToFreqValue(data,out string message);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        // Edit:去除0是无效数据的问题
                        //if (t == 0)
                        //    return double.NaN;
                        return Math.Round(t);
                    }
                }

            }

            return double.NaN;
        }
    }
    public class SweepCycleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
                return ValueConvertHelper.GetSweepCycleValue(data).ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                    return ValueConvertHelper.StrToSweepCycleValue(data).OriginalValue;
            }

            return double.NaN;
        }
    }
    public class InputDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
                return data.ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                    return ValueConvertHelper.StrToFreqValue(data,out string message).OriginalValue;
            }

            return double.NaN;
        }
    }

    public class InputDoubleAntherConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
                return data.ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value is string data)
            //{
            //    if (!string.IsNullOrWhiteSpace(data))
            //        return ValueConvertHelper.StrToFreqValue(data).OriginalValue;
            //}

            return value;
        }
    }

    class InputTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetTimeValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToTimeValue(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        // Edit:LHJ 修改BUG“所有输入框，显示0.001为0，不报错 直接输入0报错”
                        //if (t == 0)
                        //    return double.NaN;
                        return t;
                    }
                    if (d != null) return d.OriginalValue;
                }

            return double.NaN;
        }
    }
    class SweepRateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetSweepRateValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToSweepRate(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        if (t == 0)
                            return double.NaN;
                        return t;
                    }
                    if (d != null) return d.OriginalValue;
                }

            return double.NaN;
        }
    }
    class SNRConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetSNRValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (!string.IsNullOrWhiteSpace(data))
                    return ValueConvertHelper.StrToSNRValue(data).OriginalValue;
            }

            return double.NaN;
        }
    }
    class DegreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetDegreeValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToDegree(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        if (t == 0)
                            return double.NaN;
                        return t;
                    }
                    if (d != null) return d.OriginalValue;
                }

            return double.NaN;
        }
    }
    
         class RadialVelocityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetRadialVelocityValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToRadialVelocity(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        if (t == 0)
                            return double.NaN;
                        return t;
                    }
                    if (d != null) return d.OriginalValue;
                }

            return double.NaN;
        }
    }
    class InputPercentConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
               
                return ValueConvertHelper.GetPercentValue(data).ToString();

            }
            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {

                    var d = ValueConvertHelper.StrToPercentValue(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        if (t == 0)
                            return double.NaN;
                        return Math.Round( t,2);
                    }

                }

            return double.NaN;
        }
    }

    class InputCodeRateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double data)
            {
                if (double.IsInfinity(data) || double.IsNaN(data))
                    return "无效值";
                return ValueConvertHelper.GetRateValue(data).ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var d = ValueConvertHelper.StrToRateValue(data);
                    if (d != null)
                    {
                        var t = d.OriginalValue;
                        if (t == 0)
                            return double.NaN;
                        return t;
                    }
                    if (d != null) return d.OriginalValue;
                }

            return double.NaN;
        }

        
    }

    public class BinaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int||value is double)
            {
                if (System.Convert.ToInt32(value) != 0 && System.Convert.ToInt32(value) != 1)
                    return "0";
                return value.ToString();
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if(value as string == "1")
                {
                    return 1;
                }
            }
            return 0;
        }
    }

    public class OneThreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int || value is double)
            {
                if (System.Convert.ToInt32(value) != 0 && System.Convert.ToInt32(value) != 1 && System.Convert.ToInt32(value) != 2 && System.Convert.ToInt32(value) != 3)
                    return "0";
                return value.ToString();
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string data)
            {
                if (value as string == "1" || value as string == "2" || value as string == "3")
                {
                    return int.Parse((string)value);
                }
            }
            return 0;
        }
    }

    
}
