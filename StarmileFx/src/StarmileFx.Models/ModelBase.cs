using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StarmileFx.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class ModelBase
    {
        public ModelBase()
        {
            CreatTime = DateTime.Now;
        }

        [Key]
        public virtual int ID { get; set; }
        public virtual DateTime CreatTime { get; set; }
        //public virtual DateTime UpdateTime { get; set; }
        public virtual bool State { get; set; }

        /// <summary>
        /// 清除对象属性数据;给对象重新赋值前，最好先清除旧数据。
        /// </summary>
        /// <returns>true or false</returns>
        public virtual bool Clear()
        {
            try
            {
                Type t = this.GetType();//类具体实例对象
                foreach (PropertyInfo prop in t.GetProperties())     //取得所有属性
                {
                    object value = prop.GetValue(this, null);
                    prop.SetValue(this, null, null); //设置属性
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 复制对象
        /// 由子类根据需要继承实现
        /// </summary>
        /// <returns>ModelBase子类</returns>
        public virtual ModelBase Clone()
        {
            return null;
        }

        /// <summary>
        /// 复制对象
        /// 由子类根据需要继承实现
        /// </summary>
        /// <returns>ModelBase子类</returns>
        public virtual void CopyTo(ModelBase model)
        {
        }
    }
}
