using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public interface IUnitValue
    {
        /// <summary>
        /// 用于验证当前值是否时该单位的有效类型
        /// </summary>
        /// <param name="str"> 页面上输入的数据 </param>
        /// <param name="value"> 如果正确返回的值 </param>
        /// <param name="message"> 如果错误返回的消息 </param>
        /// <returns> 结果 </returns>
        bool TryParse(string str, out double value, out string message);

        /// <summary>
        /// 获取值对应的单位显示文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetView(double value);
    }

    public abstract class UnitValueBase : IUnitValue
    {
        private static System.Text.RegularExpressions.Regex _doubleRege = new System.Text.RegularExpressions.Regex(@"([-+]?(?:\b[0-9]+(?:\.[0-9]*)?|\.[0-9]+\b)(?:[eE][-+]?[0-9]+\b)?)", System.Text.RegularExpressions.RegexOptions.Compiled);
        private static System.Text.RegularExpressions.Regex _unitRegex = new System.Text.RegularExpressions.Regex(@"[a-zA-z%]+");

        public string GetView(double value)
        {
            return Activator.CreateInstance(this.GetType(), value).ToString();
        }

        public abstract bool TryUnit(string unit, out string message);

        public bool TryParse(string str, out double value, out string message)
        {
            message = string.Empty;

            value = double.NaN;

            var ss = _doubleRege.Split(str).Where(t =>
            {
                var s = t.Trim();
                return !(s == "," || s == "" || s == "，");
            });

            string num = "";

            string unit = "";

            foreach (var s in ss)
            {
                if (_doubleRege.IsMatch(s))
                {
                    num = s;
                    continue;
                }
                if (_unitRegex.IsMatch(s))
                {
                    unit = s;
                    continue;
                }
            }

            string vl = string.IsNullOrEmpty(unit) ? string.Empty : str.Replace(unit, string.Empty);

            //  Do ：验证单位部分是否合法
            if (!this.TryUnit(unit, out message))
            {
                return false;
            }

            //  Do ：验证值部分是否合法
            if (double.TryParse(vl, out value))
            {
                return true;
            }

            message = $"{vl}值不合法,请尝试输入<{num}{unit}>格式";

            return false;
        }
    }
}
