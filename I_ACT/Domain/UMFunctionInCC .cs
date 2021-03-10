using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class UMFunctionInCC : Default 
    {
        public Nullable<int> recid { get; set; }
        public string NoCostCenter { get; set; }
        public string NameCostCenter { get; set; }
        public string NameFunction { get; set; }
        public string NameSection { get; set; }

    }
}