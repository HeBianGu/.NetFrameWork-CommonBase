using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class DegreeValue
    {

        public DegreeValue(double value, DegreeUnits units = DegreeUnits.DegreeValue)
        {
            _value = value;
            _units = DegreeUnits.DegreeValue;
        }


        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private DegreeUnits _units = DegreeUnits.DegreeValue;

        public DegreeUnits Units
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
                if (double.IsNaN(Value) || double.IsInfinity(Value) )
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
