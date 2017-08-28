using SqlSugar;
using System;
using System.Collections.Generic;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysMenus")]
    public partial class SysMenus : ModelContext
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Id")]
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }
        public int? PId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        public DateTime CreatTime { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<SysMenus> SysMenusList
        {
            get
            {
                return CreateMapping<SysMenus>().Where(it => it.PId == Id).ToList();
            }
        }
    }
}
