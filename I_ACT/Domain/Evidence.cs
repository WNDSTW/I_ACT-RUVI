using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Evidence : Default
    {
        public Nullable<int> idEvidence { get; set; }
        public string noNotulenDetail { get; set; }
        public string Keterangan { get; set; }
        public string nopekSubmit { get; set; }
        public string fname { get; set; }

    }
}