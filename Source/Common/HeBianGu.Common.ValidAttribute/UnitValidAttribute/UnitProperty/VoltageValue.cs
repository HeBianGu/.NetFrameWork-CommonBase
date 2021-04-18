using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HeBianGu.Common.ValidAttribute
{
    /// <summary> 电压单位 </summary>
    public class VoltageValue : UnitValueBase
    {
        public VoltageValue(double value, VoltageUnits units)
        {
            _value = value;
            _units = units;
        }

        public VoltageValue(double value)
        {
            if (value / (1000) >= 1)
            {
                Value = value / (1000);
                Units = VoltageUnits.kV;
            }
            else if (value >= 1)
            {
                Value = value;
                Units = VoltageUnits.V;

            }
            else if (value >= 0.001)
            {
                Value = value * 1000;
                Units = VoltageUnits.mV;
            }
            else if (value >= 0.001 * 0.001)
            {
                Value = value * 1000 * 1000;
                Units = VoltageUnits.μV;
            }
            else if (value >= 0.001 * 0.001 * 0.001)
            {
                Value = value * 1000 * 1000 * 1000;
                Units = VoltageUnits.nV;
            }
            else
            {
                Value = value;
                Units = VoltageUnits.V;
            }

        }

        public VoltageValue(double[] value)
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

        private VoltageUnits _units = VoltageUnits.V;

        public VoltageUnits Units
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

                if (Units == VoltageUnits.V)
                {
                    ret = Value;
                }
                else if (Units == VoltageUnits.kV)
                {
                    ret = Value * 1000;
                }
                else if (Units == VoltageUnits.mV)
                {
                    ret = Value / 1000;
                }
                else if (Units == VoltageUnits.μV)
                {
                    ret = Value / (1000 * 1000);
                }
                else if (Units == VoltageUnits.nV)
                {
                    ret = Value / (1000 * 1000 * 1000);
                }

                return ret;
            }
        }

        public override string ToString()
        {
            if (double.IsNaN(Value)) return "无效值";

            return Value.ToString() + Units.ToString();
        }

        bool TryGetUnit(string unit, out VoltageUnits units)
        {
            units = VoltageUnits.V;

            if (unit.ToLower() == "v")
            {
                units = VoltageUnits.V;
            }
            else if (unit.ToLower() == "kv")
            {
                units = VoltageUnits.kV;
            }
            else if (unit.ToLower() == "mv")
            {
                units = VoltageUnits.mV;
            }
            else if (unit.ToLower() == "nv")
            {
                units = VoltageUnits.nV;
            }
            else if (unit.ToLower() == "uv")
            {
                units = VoltageUnits.μV;
            }
            else if (unit.ToLower() == "μv")
            {
                units = VoltageUnits.μV;
            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool TryUnit(string unit, out string message)
        {
            message = string.Empty;

            if (this.TryGetUnit(unit, out VoltageUnits units))
            {
                return true;
            }

            message = $"<{unit}>电压单位不合法,有效单位[kv,v,mv,μv,nv]";

            return false;
        }
    }
}
