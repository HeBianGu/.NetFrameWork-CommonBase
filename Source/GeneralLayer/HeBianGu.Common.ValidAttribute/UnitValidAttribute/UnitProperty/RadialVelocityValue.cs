using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class RadialVelocityValue
    {

        public RadialVelocityValue(double value, RateUnits units = RateUnits.kilometer)
        {
            _value = value;
            _units = RateUnits.kilometer;
        }


        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private RateUnits _units = RateUnits.kilometer;

        public RateUnits Units
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
            return Value.ToString() + EnumDescription.GetDescriptionByEnum(Units);
        }
    }
}
