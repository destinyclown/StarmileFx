using System;
using System.Reflection;

namespace StarmileFx.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PageData
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 是否升序
        /// </summary>
        public bool IsAsc { get; set; }

        public virtual bool Evaluate()
        {
            try
            {
                Type t = this.GetType();//类具体实例对象
                foreach (PropertyInfo prop in t.GetProperties())     //取得所有属性
                {
                    object value = prop.GetValue(this, null);
                    if (value == null)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(this, "", null); //设置属性
                        }
                        if (prop.PropertyType == typeof(int?))
                        {
                            prop.SetValue(this, 0, null); //设置属性
                        }
                        if (prop.PropertyType == typeof(DateTime?))
                        {
                            prop.SetValue(this, DateTime.Now, null); //设置属性
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 分页结果集封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> where T : new()
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
