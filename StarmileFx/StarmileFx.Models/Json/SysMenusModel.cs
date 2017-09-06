using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Json
{
    public class SysMenusModel
    {
        public List<MainMenu> MainMenuList { get; set; }

        public class MainMenu
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Icon { get; set; }
            public string Code { get; set; }
            public bool State { get; set; }
            public List<BaseMenu> MainMenuBase { get; set; }
        }

        public class BaseMenu
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Code { get; set; }
            public bool State { get; set; }
            public List<Menu> MenuList { get; set; }
        }

        public class Menu
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Icon { get; set; }
            public string Code { get; set; }
            public bool State { get; set; }
        }
    }
}
