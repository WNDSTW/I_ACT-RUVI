using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMActionMenu : Default 
    {
        public int ActId { get; set; }
        public int MenuId { get; set; }
        public string Actions { get; set; }
        public string MenuName { get; set; }
    }
}