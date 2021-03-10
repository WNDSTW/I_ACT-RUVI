using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMActionRole : Default 
    {
        public int recId { get; set; }
        public int IdRole { get; set; }
        public int ActId { get; set; }
        public int MenuId { get; set; }
        public string RoleName { get; set; }
    }
}