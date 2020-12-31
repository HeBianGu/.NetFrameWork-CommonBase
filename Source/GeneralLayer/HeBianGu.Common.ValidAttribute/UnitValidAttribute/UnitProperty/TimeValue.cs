using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class TimeValue
    {
        public TimeValue(double value, TimeUnits units=TimeUnits.s)
        {
            _value = value;
            _units = units;
        }
        public TimeValue(double value)
        {
            
            if (value >= 1 || value == 0)
            {
                Value = value;
                Units =TimeUnits.s;
            }
            else if (value >= 0.001)
            {
                Value = value*1000;
                Units = TimeUnits.ms;
                
            }
            else if (value >= 0.001 * 0.001)
            {
                Value =value*1000*1000;
                Units = TimeUnits.μs;
               
            }
            else if (value > 0)
            {
                Value = value * 1000 * 1000 * 1000;
                Units = TimeUnits.ns;
            }

        }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private TimeUnits _units = TimeUnits.s;

        public TimeUnits Units
        {
            get { return _units; }
            set
            {
                _units = value;
            }
        }
        public double OriginalValue
        {
            get

            {
                if (double.IsNaN(Value) || double.IsInfinity(Value))
                    return double.NaN;
                double ret = 0;
                if (Units == TimeUnits.s)
                {
                    ret = Value;
                }
                else if (Units == TimeUnits.ms)
                {
                    ret = Value / 1000;
                }
                else if (Units == TimeUnits.μs)
                {
                    ret = Value /( 1000 * 1000);
                }
                else if (Units == TimeUnits.ns)
                {
                    ret = Value /( 1000 * 1000 * 1000);
                }
                return ret;
            }
        }
        public override string ToString()
        {
            //switch (Units)
            //{
            //    case TimeUnits.s:
            //        {
            //            return Value.ToString("#0.00000000") + Units.ToString();
            //        }
            //    case TimeUnits.ms:
            //        {
            //            return Value.ToString("#0.00000") + Units.ToString();
            //        }
            //    case TimeUnits.μs:
            //        {
            //            return Value.ToString("#0.00") + Units.ToString();
            //        }
            //    default:
            //        {
            //            return Value.ToString() + Units.ToString();
            //        }
            //}
            return Value.ToString() + Units.ToString();
        }
    }
}
