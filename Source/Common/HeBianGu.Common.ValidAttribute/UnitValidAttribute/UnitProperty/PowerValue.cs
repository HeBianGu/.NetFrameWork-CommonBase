using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class PowerValue
    {
        public PowerValue(double value, PowerUnits units=PowerUnits.dBm)
        {
            this.Value = value;
            this.Units = units;
            //double t = value;
            //if (units==PowerUnits.dBm)
            //{
                
            //    if (t > 54)
            //    {
            //        t = 54;
            //    }
            //    else
            //    {
            //        Value = t;
            //        Units = units;
            //    }
            //}
            //else
            //{
            //    if (t > 250) t = 250;
            //    Value = t;
            //  //  Units = PowerUnits.w;
            //}
        }
        public PowerValue(double value):this(value,PowerUnits.dBm)
        { }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private PowerUnits _units = PowerUnits.dBm;

        public PowerUnits Units
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
                if (Units == PowerUnits.dBm)
                {
                    ret = Value;
                }
                //else if (Units == PowerUnits.w)
                //{
                //    ret = 10 * Math.Log10(Value * 1000);
                //}
                
                return Math.Round(ret,1);
            }
        }
        public override string ToString()
        {
            if (double.IsNaN(Value)) return "无效值";

            //if (Units==PowerUnits.dBm&&Value >= 30)
            //{
            //   return Math.Round(Math.Pow(10, Value / 10) / 1000, 1).ToString()+ PowerUnits.w.ToString();
            //}
            return Value.ToString() + Units.ToString();
        }
    }
}
