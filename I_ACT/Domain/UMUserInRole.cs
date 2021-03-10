using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMUserInRole : Default 
    {
        public Nullable<int> idRole { get; set; }
        public string username { get; set; }
        public string RoleName { get; set; }
        public string section { get; set; }
        public bool isActive { get; set; }
        public bool isBanned { get; set; }
        public bool requested { get; set; }
    }
}