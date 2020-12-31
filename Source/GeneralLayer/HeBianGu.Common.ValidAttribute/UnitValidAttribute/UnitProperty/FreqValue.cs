using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class FreqValue
    {
        public FreqValue(double value, FrequencyUnits units)
        {
            _value = value;
            _units = units;
        }

        public FreqValue(double value)
        {
            if (value / (1000 * 1000 * 1000) >= 1)
            {
                Value =value / (1000 * 1000 * 1000);
                Units =FrequencyUnits.GHz;
            }
            else if (value / (1000 * 1000) >= 1)
            {
                Value =value / (1000 * 1000);
                Units = FrequencyUnits.MHz;
            }
            else if (value / 1000 >= 1)
            {
                Value = value / 1000;
                Units = FrequencyUnits.kHz;
            }
            else
            {
                Value = value;
                Units=FrequencyUnits.Hz;
            }
            
        }
        public FreqValue(double[] value)
        {
            if (value == null)
            {
                Value = 0;
            }
        }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private FrequencyUnits _units = FrequencyUnits.Hz;

        public FrequencyUnits Units
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
                if (Units == FrequencyUnits.Hz)
                {
                    ret = Value;

                }
                else if (Units == FrequencyUnits.kHz)
                {
                    ret = Value * 1000;
                }
                else if (Units == FrequencyUnits.MHz)
                {
                    ret =Value * 1000 * 1000;
                }
                else if (Units == FrequencyUnits.GHz)
                {
                    ret = Value * 1000 * 1000 * 1000;
                }
                return ret;
            }
        }
        public override string ToString()
        {
            if (double.IsNaN(Value)) return "无效值";
            //switch (Units)
            //{
            //    case FrequencyUnits.GHz:
            //        {
            //            return Value.ToString("#0.000000") + Units.ToString();
            //        }
            //    case FrequencyUnits.MHz:
            //        {
            //            return Value.ToString("#0.000") + Units.ToString();
            //        }
            //    case FrequencyUnits.kHz:
            //        {
            //            return Value.ToString() + Units.ToString();
            //        }
            //    default:
            //        {
            //            return Value.ToString() + Units.ToString();
            //        }
            //}
           return Value.ToString()  + Units.ToString();
        }
    }
}
