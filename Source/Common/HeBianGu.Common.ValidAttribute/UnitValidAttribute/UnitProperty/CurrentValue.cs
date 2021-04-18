using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    /// <summary> 电流单位 </summary>
    public class CurrentValue
    {
        public CurrentValue(double value, CurrentUnits units)
        {
            _value = value;
            _units = units;
        }

        public CurrentValue(double value)
        {
            if (value / (1000) >= 1)
            {
                Value = value / (1000);
                Units = CurrentUnits.kA;
            }
            else if (value >= 1)
            {
                Value = value;
                Units = CurrentUnits.A;

            }
            else if (value >= 0.001)
            {
                Value = value * 1000;
                Units = CurrentUnits.mA;
            }
            else if (value >= 0.001 * 0.001)
            {
                Value = value * 1000 * 1000;
                Units = CurrentUnits.μA;
            }
            else
            {
                Value = value;
                Units = CurrentUnits.A;
            }

        }
        public CurrentValue(double[] value)
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

        private CurrentUnits _units = CurrentUnits.A;

        public CurrentUnits Units
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

                if (Units == CurrentUnits.A)
                {
                    ret = Value;
                }
                else if (Units == CurrentUnits.kA)
                {
                    ret = Value * 1000;
                }
                else if (Units == CurrentUnits.mA)
                {
                    ret = Value / 1000;
                }
                else if (Units == CurrentUnits.μA)
                {
                    ret = Value / (1000 * 1000);
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
