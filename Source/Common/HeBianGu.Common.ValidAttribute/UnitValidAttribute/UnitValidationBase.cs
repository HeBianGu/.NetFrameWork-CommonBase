using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public class ValidationBase : IDataErrorInfo
    {
        public ValidationBase()
        {
            var froms = this.GetErrorMessage();

            this.Error = froms.FirstOrDefault();

        }
        #region - 属性 -

        public string Error { get; protected set; }

        // 验证
        public string this[string columnName]
        {
            get
            {
                List<string> results = new List<string>();

                var property = this.GetType().GetProperty(columnName);

                var attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();

                if (attrs == null || attrs.Count() == 0) return string.Empty;

                var display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();

                var value = property.GetValue(this);

                foreach (var r in attrs)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                    }
                    else
                    {
                        //  Do ：格式化
                        if (r is UnitValidAttribute unit)
                        {
                            var p = this.GetType().GetProperty(columnName);

                            string view = unit.FormatToView(value);

                            if (view != value.ToString())
                                p.SetValue(this, view);
                        }
                    }
                }

                return string.Join(Environment.NewLine, results);

            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        /// <summary> 获取所有数据错误信息 </summary>
        public List<string> GetErrorMessage()
        {
            List<string> results = new List<string>();

            var propertys = this.GetType().GetProperties();

            foreach (var item in propertys)
            {
                var collection = item.GetCustomAttributes(false)?.OfType<ValidationAttribute>();

                var display = item.GetCustomAttributes(false)?.OfType<DisplayAttribute>()?.FirstOrDefault();

                //  Do：检验数据有效性
                if (collection == null || collection.Count() == 0) continue;

                var value = item.GetValue(this);

                foreach (var r in collection)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? item.Name : display.Name));
                    }
                }
            }

            return results;
        }

        /// <summary> 检查数据是否都有效 </summary>
        public virtual bool IsValid()
        {
            var message = this.GetErrorMessage();

            this.Error = message.FirstOrDefault();

            return message.Count == 0;
        }

        #endregion
    }

    /// <summary> 带有单位数据基类 </summary>
    public abstract class UnitValidationBase : ValidationBase
    {
        /// <summary> 获取带有单位的转换结果值 </summary>
        public double GetUnitValue(string property)
        {
            var p = this.GetType().GetProperty(property);

            var find = p.GetCustomAttribute<UnitValidAttribute>();

            if (find != null)
            {
                return find.GetValue(p.GetValue(this)?.ToString());
            }
            else
            {
                return Convert.ToDouble(p.GetValue(this)?.ToString());
            }
        }

        /// <summary> 获取带有单位转换的显示值 </summary>
        public void SetUnitValue(string property, double? d)
        {
            var p = this.GetType().GetProperty(property);

            if (d == null)
            {
                p.SetValue(this, null);
                return;
            }

            var find = p.GetCustomAttribute<UnitValidAttribute>();

            if (find != null)
            {
                p.SetValue(this, find.GetView(d.Value));
            }
            else
            {
                p.SetValue(this, d.ToString());
            }
        }
    }

    public abstract class UnitValidationBase<T> : UnitValidationBase
    {
        public T Model { get; set; }

        public UnitValidationBase(T model)
        {
            this.Model = model;
        }

        protected virtual bool LoadValue(out string message)
        {
            message = string.Empty;

            var ps = this.GetType().GetProperties();

            if (ps == null) return true;

            foreach (var property in ps)
            {
                var find = typeof(T).GetProperty(property.Name);

                if (find == null) continue;

                var uv = property.GetCustomAttribute<UnitValidAttribute>();

                if (uv != null)
                {
                    this.SetUnitValue(property.Name, (double)find.GetValue(this.Model));
                }
                else
                {
                    property.SetValue(this, find.GetValue(this.Model));
                }
            }

            return true;
        }

        protected virtual bool SaveValue(out string message)
        {
            message = string.Empty;

            if (!this.IsValid())
            {
                message = this.Error;
                return false;
            }

            var ps = this.GetType().GetProperties();

            if (ps == null) return true;

            foreach (var property in ps)
            {
                var find = typeof(T).GetProperty(property.Name);

                if (find == null) continue;

                var uv = property.GetCustomAttribute<UnitValidAttribute>();

                if (uv != null)
                {
                    find.SetValue(this.Model, this.GetUnitValue(property.Name));
                }
                else
                {
                    find.SetValue(this, property.GetValue(this.Model));
                }
            }

            return true;
        }
    }
}
