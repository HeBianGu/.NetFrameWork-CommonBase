using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class RateValue
    {
        public RateValue(double value, CodeRateUnits units)
        {
            _value = value;
            _units = units;
        }

        public RateValue(double value)
        {
            if (value / (1000 * 1000 * 1000) >= 1)
            {
                Value = value / (1000 * 1000 * 1000);
                Units = CodeRateUnits.Gsps;
            }
            else if (value / (1000 * 1000) >= 1)
            {
                Value = value / (1000 * 1000);
                Units = CodeRateUnits.Msps;
            }
            else if (value / 1000 >= 1)
            {
                Value = value / 1000;
                Units = CodeRateUnits.Ksps;
            }
            else
            {
                Value = value;
                Units = CodeRateUnits.sps;
            }

        }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private CodeRateUnits _units = CodeRateUnits.sps;

        public CodeRateUnits Units
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
                if (Units == CodeRateUnits.sps)
                {
                    ret = Value;
                }
                else if (Units == CodeRateUnits.Ksps)
                {
                    ret = Value * 1000;
                }
                else if (Units == CodeRateUnits.Msps)
                {
                    ret = Value * 1000 * 1000;
                }
                else if (Units == CodeRateUnits.Gsps)
                {
                    ret = Value * 1000 * 1000 * 1000;
                }
                return ret;
            }
        }
        public override string ToString()
        {
            if (double.IsNaN(Value)) return "无效值";
            return Value.ToString() + Units.ToString();
        }
    }
}
