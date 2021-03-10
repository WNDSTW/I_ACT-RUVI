using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMRole : Default 
    {
        public int IdRole { get; set; }
        public int NoLevel { get; set; }
        public string RoleName { get; set; }
        public string Jabatan { get; set; }
    }
}