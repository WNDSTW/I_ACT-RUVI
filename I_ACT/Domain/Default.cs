using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Default
    {
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Search { get; set; }
        public bool isActive { get; set; }
    }
}