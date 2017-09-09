using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace StarmileFx.Models.Base
{
    public class WebMenus : ModelContext
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }
        public bool Newest { get; set; }
        public bool Help { get; set; }
        public int Sort { get; set; }
        public int? PId { get; set; }
        public bool State { get; set; }
        public DateTime CreatTime { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<WebMenus> Children { get; set; }
    }
}
