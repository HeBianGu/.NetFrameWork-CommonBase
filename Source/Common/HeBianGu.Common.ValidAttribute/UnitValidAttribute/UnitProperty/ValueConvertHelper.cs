using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public static class ValueConvertHelper
    {
        private static System.Text.RegularExpressions.Regex _doubleRege = new System.Text.RegularExpressions.Regex(@"([-+]?(?:\b[0-9]+(?:\.[0-9]*)?|\.[0-9]+\b)(?:[eE][-+]?[0-9]+\b)?)", System.Text.RegularExpressions.RegexOptions.Compiled);
        private static System.Text.RegularExpressions.Regex _unitRegex = new System.Text.RegularExpressions.Regex(@"[a-zA-z%]+");

        public static FreqValue GetFreqValue(double value)
        {
            FreqValue freqValue;

            freqValue = new FreqValue(1000, FrequencyUnits.GHz);
            if (value > 1000f * 1000f * 1000f * 100f)
            {
                freqValue = new FreqValue(100, FrequencyUnits.GHz);
            }
            else if (value / (1000 * 1000 * 1000) >= 1)
            {
                freqValue = new FreqValue(value / (1000 * 1000 * 1000), FrequencyUnits.GHz);
            }
            else if (value / (1000 * 1000) >= 1)
            {
                freqValue = new FreqValue(value / (1000 * 1000), FrequencyUnits.MHz);
            }
            else if (value / 1000 >= 1)
            {
                freqValue = new FreqValue(value / 1000, FrequencyUnits.kHz);
            }
            else
            {
                freqValue = new FreqValue(value, FrequencyUnits.Hz);
            }
            return freqValue;
        }
        public static RateValue GetRateValue(double value)
        {
            RateValue freqValue;
            if (value / (1000 * 1000 * 1000) >= 1)
            {
                freqValue = new RateValue(value / (1000 * 1000 * 1000), CodeRateUnits.Gsps);
            }
            else if (value / (1000 * 1000) >= 1)
            {
                freqValue = new RateValue(value / (1000 * 1000), CodeRateUnits.Msps);
            }
            else if (value / 1000 >= 1)
            {
                freqValue = new RateValue(value / 1000, CodeRateUnits.Ksps);
            }
            else
            {
                freqValue = new RateValue(value, CodeRateUnits.sps);
            }
            return freqValue;
        }
        public static FreqValue StrToFreqValue(string value, out string message)
        {
            message = string.Empty;

            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : value.Replace(unit, string.Empty);

            FrequencyUnits units = FrequencyUnits.Hz;

            if (unit.ToLower() == "hz")
            {
                units = FrequencyUnits.Hz;
            }
            else if (unit.ToLower() == "khz")
            {
                units = FrequencyUnits.kHz;
            }
            else if (unit.ToLower() == "mhz")
            {
                units = FrequencyUnits.MHz;
            }
            else if (unit.ToLower() == "ghz")
            {
                units = FrequencyUnits.GHz;
            }
            //else if (unit.ToLower().Contains("k"))
            //{
            //    units = FrequencyUnits.kHz;
            //}
            //else if (unit.ToLower().Contains("m"))
            //{
            //    units = FrequencyUnits.MHz;
            //}
            //else if (unit.ToLower().Contains("g"))
            //{
            //    units = FrequencyUnits.GHz;
            //}
            else
            {
                if (double.TryParse(vl, out double d))
                {
                    message = $"{unit}不是频率的有效单位,请尝试输入<{vl}Hz/MHz/GHz>格式";
                    return null;
                }
                else
                {
                    message = $"{unit}不是频率的有效单位,请尝试输入{num}Hz/MHz/GHz格式";
                    return null;
                }
            }

            if (vl.EndsWith("."))
            {
                message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";
                return null;
            }

            if (double.TryParse(vl, out double dd))
            {
                return new FreqValue(dd, units);
            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return null;
        }
        public static RateValue StrToRateValue(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            CodeRateUnits units = CodeRateUnits.sps;
            var u = unit.ToLower();
            if (u == "b" || u == "bp" || u == "sps")
            {
                units = CodeRateUnits.sps;
            }
            else if (u == "ks" || u == "ksp" || u == "ksps")
            {
                units = CodeRateUnits.Ksps
                    ;
            }
            else if (u == "ms" || u == "msp" || u == "msps")
            {
                units = CodeRateUnits.Msps;
            }
            else if (u == "gs" || u == "gsp" || u == "gsps")
            {
                units = CodeRateUnits.Gsps;
            }
            else if (unit.ToLower().Contains("k"))
            {
                units = CodeRateUnits.Ksps;
            }
            else if (unit.ToLower().Contains("m"))
            {
                units = CodeRateUnits.Msps;
            }
            else if (unit.ToLower().Contains("g"))
            {
                units = CodeRateUnits.Gsps;
            }
            else
            {
                units = CodeRateUnits.sps;
            }


            if (double.TryParse(num, out double dd))
            {
                return new RateValue(dd, units);
            }

            return null;
        }
        public static SweepCycleValue StrToSweepCycleValue(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            SweepCycleUnit units = SweepCycleUnit.rpm;
            var u = unit.ToLower();
            if (u == "r" || u == "rp" || u == "rpm")
            {
                units = SweepCycleUnit.rpm;
            }

            if (double.TryParse(num, out double dd))
            {
                return new SweepCycleValue(dd, units);
            }

            return null;
        }
        public static SNRValue StrToSNRValue(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            SNRUnits units = SNRUnits.dB;
            var u = unit.ToLower();
            if (u == "d" || u == "db" || u == "dB")
            {
                units = SNRUnits.dB;
            }

            if (double.TryParse(num, out double dd))
            {
                return new SNRValue(dd, units);
            }

            return null;
        }
        public static SweepCycleValue GetSweepCycleValue(double value)
        {
            SweepCycleValue sweepCycleValue = null;
            sweepCycleValue = new SweepCycleValue(value, SweepCycleUnit.rpm);
            return sweepCycleValue;
        }
        public static TimeValue StrToTimeValue(string value, out string message)
        {
            message = string.Empty;

            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : value.Replace(unit, string.Empty);

            TimeUnits units = TimeUnits.s;

            if (unit.ToLower() == "s")
            {
                units = TimeUnits.s;
            }
            else if (unit.ToLower() == "ms")
            {
                units = TimeUnits.ms;
            }
            else if (unit.ToLower() == "μs")
            {
                units = TimeUnits.μs;
            }
            else if (unit.ToLower() == "us")
            {
                units = TimeUnits.μs;
            }
            else if (unit.ToLower() == "ns")
            {
                units = TimeUnits.ns;
            }
            else if (unit.ToLower() == "h")
            {
                units = TimeUnits.h;
            }
            else if (unit.ToLower() == "m")
            {
                units = TimeUnits.m;
            }
            //else if (unit.ToLower().Contains("m"))
            //{
            //    units = TimeUnits.ms;
            //}
            //else if (unit.ToLower().Contains("u"))
            //{
            //    units = TimeUnits.μs;
            //}
            //else if (unit.ToLower().Contains("n"))
            //{
            //    units = TimeUnits.ns;
            //}
            //else if (unit.ToLower().Contains("μs"))
            //{
            //    units = TimeUnits.μs;
            //}
            else
            {
                if (double.TryParse(vl, out double d))
                {
                    message = $"{unit}不是频率的有效单位,请尝试输入<{vl}h/m/s/ms/μs/ns>格式";
                    return null;
                }
                else
                {
                    message = $"{unit}不是频率的有效单位,请尝试输入<{num}h/m/s/ms/μs/ns>格式";
                    return null;
                }
            }

            if (vl.EndsWith("."))
            {
                message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";
                return null;
            }

            if (double.TryParse(vl, out double dd))
            {
                return GetTimeValue(Convert.ToDouble(SetTimeValue(new TimeValue(dd, units))));

            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return null;

            //if (double.TryParse(num, out double dd))
            //{
            //    return GetTimeValue(Convert.ToDouble(SetTimeValue(new TimeValue(dd, units))));
            //}
            //return null;
        }
        public static double SetTimeValue(TimeValue timeValue)
        {
            if (timeValue == null)
            {
                return 0;
            }
            double ret = 0;
            if (timeValue.Units == TimeUnits.s)
            {
                ret = timeValue.Value;

            }
            else if (timeValue.Units == TimeUnits.ms)
            {
                ret = timeValue.Value * 0.001;
            }
            else if (timeValue.Units == TimeUnits.μs)
            {
                ret = timeValue.Value * 0.001 * 0.001;
            }
            else if (timeValue.Units == TimeUnits.ns)
            {
                ret = timeValue.Value * 0.001 * 0.001 * 0.001;
            }
            else if (timeValue.Units == TimeUnits.h)
            {
                ret = timeValue.Value * 60 * 60;
            }
            else if (timeValue.Units == TimeUnits.m)
            {
                ret = timeValue.Value * 60;
            }
            return ret;
        }

        public static double SetDegreeValue(DegreeValue degreeValue)
        {
            if (degreeValue == null)
            {
                return 0;
            }
            double ret = 0;
            if (degreeValue.Units == DegreeUnits.DegreeValue)
            {
                ret = degreeValue.Value;

            }
            return ret;
        }

        public static TimeValue GetTimeValue(double value)
        {
            TimeValue timeValue = null;
            if (value >= 1 || value == 0)
            {
                timeValue = new TimeValue(value, TimeUnits.s);
            }
            else if (value >= 0.001)
            {
                timeValue = new TimeValue(value * 1000, TimeUnits.ms);
            }
            else if (value >= 0.001 * 0.001)
            {
                timeValue = new TimeValue(value * 1000 * 1000, TimeUnits.μs);
            }
            else if (value > 0)
            {
                timeValue = new TimeValue(value * 1000 * 1000 * 1000, TimeUnits.ns);
            }
            return timeValue;

        }
        public static SweepRateValue GetSweepRateValue(double value)
        {
            SweepRateValue sweepRateValue = null;
            if (value >= 1 || value == 0)
            {
                sweepRateValue = new SweepRateValue(value, SweepRateUnits.Second);
            }
            return sweepRateValue;

        }
        public static DegreeValue GetDegreeValue(double value)
        {
            DegreeValue degreeValue = null;
            if (value >= 1 || value == 0)
            {
                degreeValue = new DegreeValue(value, DegreeUnits.DegreeValue);
            }
            return degreeValue;
        }
        public static RadialVelocityValue GetRadialVelocityValue(double value)
        {
            RadialVelocityValue radialVelocityValue = null;
            if (value >= 1 || value == 0)
            {
                radialVelocityValue = new RadialVelocityValue(value, RateUnits.kilometer);
            }
            return radialVelocityValue;
        }
        public static SNRValue GetSNRValue(double value)
        {
            SNRValue sNRValue = null;
            if (value >= 1 || value == 0)
            {
                sNRValue = new SNRValue(value, SNRUnits.dB);
            }
            return sNRValue;
        }

        public static PowerValue StrToPowerValue(string value, out string message)
        {
            message = string.Empty;

            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : value.Replace(unit, string.Empty);

            PowerUnits units = PowerUnits.dBm;
            if (unit.ToLower() == "d")
            {
                units = PowerUnits.dBm;
            }
            else if (unit.ToLower() == "db")
            {
                units = PowerUnits.dBm;
            }
            else if (unit.ToLower() == "dbm")
            {
                units = PowerUnits.dBm;
            }
            else
            {
                if (double.TryParse(vl, out double d))
                {
                    message = $"{unit}不是功率的有效单位,请尝试输入<{vl}dbm/db/d>格式";
                    return null;
                }
                else
                {
                    message = $"{unit}不是功率的有效单位,请尝试输入{num}dbm/db/d格式";
                    return null;
                }

            }
            //else if (unit.ToLower() == "w")
            //{
            //    units = PowerUnits.w;
            //}

            if (vl.EndsWith("."))
            {
                message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";
                return null;
            }

            if (double.TryParse(vl, out double dd))
            {
                return new PowerValue(dd, units);
            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return null;
        }


        public static PercentValue GetPercentValue(double value)
        {


            PercentValue percentValue = null;
            if (value <= 1 && value >= 0)
            {
                percentValue = new PercentValue(value * 100, PercentUnits.hundredth);
            }
            else
            {
                percentValue = new PercentValue(double.NaN, PercentUnits.hundredth);
            }
            return percentValue;
        }

        public static PercentValue StrToPercentValue(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            PercentUnits units = PercentUnits.hundredth;
            if (unit.ToLower() == "%")
            {
                units = PercentUnits.hundredth; ;
            }

            if (double.TryParse(num, out double dd))
            {
                return new PercentValue(dd, units);
            }
            return null;

        }

        public static DegreeValue StrToDegree(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            DegreeUnits units = DegreeUnits.DegreeValue;
            if (unit.ToLower() == "°")
            {
                units = DegreeUnits.DegreeValue; ;
            }

            if (double.TryParse(num, out double dd))
            {
                return new DegreeValue(dd, units);
            }
            return null;
        }
        public static RadialVelocityValue StrToRadialVelocity(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            RateUnits units = RateUnits.kilometer;
            if (unit.ToLower() == "k" || unit.ToLower() == "km" || unit.ToLower() == "km/" || unit.ToLower() == "km/h")
            {
                units = RateUnits.kilometer;
            }

            if (double.TryParse(num, out double dd))
            {
                return new RadialVelocityValue(dd, units);
            }
            return null;
        }
        public static SweepRateValue StrToSweepRate(string value)
        {
            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            SweepRateUnits units = SweepRateUnits.Second;
            if (unit.ToLower() == "°")
            {
                units = SweepRateUnits.Second; ;
            }

            if (double.TryParse(num, out double dd))
            {
                return new SweepRateValue(dd, units);
            }
            return null;
        }

        public static CurrentValue StrToCurrentValue(string value, out string message)
        {
            message = string.Empty;

            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : value.Replace(unit, string.Empty);

            CurrentUnits units = CurrentUnits.A;

            if (unit.ToLower() == "a")
            {
                units = CurrentUnits.A;
            }
            else if (unit.ToLower() == "ka")
            {
                units = CurrentUnits.kA;
            }
            else if (unit.ToLower() == "ma")
            {
                units = CurrentUnits.mA;
            }
            else if (unit.ToLower() == "μa")
            {
                units = CurrentUnits.μA;
            }
            else if (unit.ToLower() == "ua")
            {
                units = CurrentUnits.μA;
            }
            else
            {
                if (double.TryParse(vl, out double d))
                {
                    message = $"{unit}不是电流的有效单位,请尝试输入<{vl}A/kA/mA/μA>格式";
                    return null;
                }
                else
                {
                    message = $"{unit}不是电流的有效值,请尝试输入{num}A/kA/mA/μA>格式";
                    return null;
                }
            }

            if (vl.EndsWith("."))
            {
                message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";
                return null;
            }

            if (double.TryParse(vl, out double dd))
            {
                return new CurrentValue(dd, units);
            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return null;
        }

        public static VoltageValue StrToVoltageValue(string value, out string message)
        {
            message = string.Empty;

            var ss = _doubleRege.Split(value).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });
            string num = "";
            string unit = "";
            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : value.Replace(unit, string.Empty);

            VoltageUnits units = VoltageUnits.V;

            if (unit.ToLower() == "v")
            {
                units = VoltageUnits.V;
            }
            else if (unit.ToLower() == "kv")
            {
                units = VoltageUnits.kV;
            }
            else if (unit.ToLower() == "mv")
            {
                units = VoltageUnits.mV;
            }
            else if (unit.ToLower() == "nv")
            {
                units = VoltageUnits.nV;
            }
            else if (unit.ToLower() == "uv")
            {
                units = VoltageUnits.μV;
            }
            else if (unit.ToLower() == "μv")
            {
                units = VoltageUnits.μV;
            }

            else
            {
                if (double.TryParse(vl, out double d))
                {
                    message = $"{unit}不是电压的有效单位,请尝试输入<{vl}kv/v/mv/μv/nv>格式";
                    return null;
                }
                else
                {
                    message = $"{unit}不是电压的有效值,请尝试输入<{num}kv/v/mv/μv/nv>格式";
                    return null;
                }
            }

            if (vl.EndsWith("."))
            {
                message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";
                return null;
            }

            if (double.TryParse(vl, out double dd))
            {
                return new VoltageValue(dd, units);
            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return null;
        }

        public static string NumArrayToString<T>(IEnumerable<T> data) where T : struct
        {
            StringBuilder sb = new StringBuilder();
            foreach (var d in data)
            {
                sb.Append(d.ToString());
                sb.Append(",");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

    }

    public class EnumDescription
    {
        public static object GetDescription(ref IDictionary<object, string> dic, ref IDictionary<string, object> dic2, Type type, object value)
        {
            if (dic == null)
            {
                dic = new Dictionary<object, string>();
                dic2 = new Dictionary<string, object>();
                Type reflectionType = TypeDescriptor.GetReflectionType(type);
                if (reflectionType == null)
                {
                    reflectionType = type;
                }
                FieldInfo[] fields = reflectionType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                ArrayList list = null;
                if ((fields != null) && (fields.Length) > 0)
                {
                    list = new ArrayList(fields.Length);
                }
                if (list != null)
                {
                    foreach (FieldInfo info in fields)
                    {
                        object fieldValue = info.GetValue(info);
                        string desc = fieldValue.ToString();
                        DescriptionAttribute descriptionAttribute = null;
                        foreach (DescriptionAttribute a in info.GetCustomAttributes(typeof(DescriptionAttribute), false))
                        {
                            descriptionAttribute = a;
                            break;
                        }
                        if (descriptionAttribute != null)
                        {
                            desc = descriptionAttribute.Description;
                        }
                        dic[fieldValue] = desc;
                        dic2[desc] = fieldValue;
                    }
                }
            }
            if (!dic.ContainsKey(value))
            {
                return null;
            }
            return dic[value];
        }

        public static string GetDescriptionByEnum(Enum enumValue)
        {
            string value = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }

}
