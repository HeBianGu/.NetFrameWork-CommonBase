using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
   public class SweepCycleValue
    {
        public SweepCycleValue(double value, SweepCycleUnit units)
        {
            _value = value;
            _units = units;
        }

        public SweepCycleValue(double value)
        {
         
                Value = value;
                Units = SweepCycleUnit.rpm;

        }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private SweepCycleUnit _units = SweepCycleUnit.rpm;

        public SweepCycleUnit Units
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
                if (Units == SweepCycleUnit.rpm)
                {
                    ret = Value;
                }
                return ret;
            }
        }
        public override string ToString()
        {
            return Value.ToString() + Units.ToString();
        }
    }
}
