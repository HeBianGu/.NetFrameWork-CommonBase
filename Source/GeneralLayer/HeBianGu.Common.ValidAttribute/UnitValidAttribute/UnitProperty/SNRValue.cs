using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class SNRValue
    {
        public SNRValue(double value, SNRUnits units = SNRUnits.dB)
        {
            _value = value;
            _units = SNRUnits.dB;
        }

        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private SNRUnits _units = SNRUnits.dB;

        public SNRUnits Units
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
                return Math.Round(Value, 2);
            }
        }
        public override string ToString()
        {
            //      if (double.IsNaN(Value)) return "无效值";
            return Value.ToString() + Units.ToString();
        }
    }
}
