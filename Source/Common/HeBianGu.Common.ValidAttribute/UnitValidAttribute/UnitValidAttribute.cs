using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class UnitValidAttribute : ValidationAttribute, IUnitValidAttribute
    {
        public Type UnitType { get; set; }

        /// <summary> 重写验证规则 </summary>
        public override bool IsValid(object value)
        {
            var result = this.TryGetValue(value?.ToString(), out double d, out string message);

            this.ErrorMessage = message;

            return result;

        }

        /// <summary> 重写输出错误消息 </summary>
        public override string FormatErrorMessage(string name)
        {
            return $"参数{name},{this.ErrorMessage}";
        }

        /// <summary> 根据当前单位返回值 </summary>
        bool TryGetValue(string value, out double d, out string message)
        {
            message = string.Empty;

            d = double.NaN;

            if (string.IsNullOrEmpty(value))
            {
                message = "参数不能为空";
                return false;
            }

            IUnitValue uv = Activator.CreateInstance(this.UnitType) as IUnitValue;

            if (uv == null)
            {
                if (!double.TryParse(value?.ToString(), out d))
                {
                    message = "不是double的有效类型";
                    return false;
                }
                else
                {
                    return true;
                }
            }

            bool result = uv.TryParse(value, out d, out message);

            if (!result)
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "转换限制类型错误" + this.UnitType;
                }
                return false;
            }

            return true;

        }

        /// <summary> 获取当前值 </summary>
        public double GetValue(string value)
        {
            TryGetValue(value, out double d, out string message);

            return d;
        }

        /// <summary> 获取显示值 </summary>
        public string GetView(double value)
        {
            IUnitValue uv = Activator.CreateInstance(this.UnitType) as IUnitValue;

            if (uv == null)
            {
                return value.ToString();
            }

           return uv.GetView(value);

        }

        public string FormatToView(object value)
        {
            this.TryGetValue(value?.ToString(), out double d, out string message);

            return GetView(d);
        }
    }
}
