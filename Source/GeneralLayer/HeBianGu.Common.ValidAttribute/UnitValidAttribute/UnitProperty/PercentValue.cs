
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeBianGu.Common.ValidAttribute
{
    public class PercentValue
    {
        public PercentValue(double value, PercentUnits units = PercentUnits.hundredth)
        {
            _value = value;
            _units = units;
        }

    
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private PercentUnits _units = PercentUnits.hundredth;

        public PercentUnits Units
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
                if (double.IsNaN(Value) || double.IsInfinity(Value) || Value >1)
                    return double.NaN;
                return Math.Round(Value, 2);
            }
        }
        public override string ToString()
        {
            if (double.IsNaN(Value)) return "无效值";
            if (Units == PercentUnits.hundredth)
            {
                
                return string.Format($"±{Value.ToString("00")}%");
            }
            else
            {
                return Value.ToString();
            }
        }
    }
}