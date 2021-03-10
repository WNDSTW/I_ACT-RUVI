using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Komentar : Default
    {
        public Nullable<int> idKomentar { get; set; }
        public string isiKomentar { get; set; }
        public string NoNotulenDetail { get; set; }

    }
}