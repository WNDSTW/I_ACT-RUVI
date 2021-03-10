using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMUser : Default 
    {
        public string id { get; set; }
        public string appid { get; set; }
        public string username { get; set; }
        public string roleid { get; set; }
        public string role { get; set; }
        public string section { get; set; }
    }
}