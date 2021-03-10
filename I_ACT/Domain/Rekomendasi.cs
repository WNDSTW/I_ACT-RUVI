using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Rekomendasi : Default
    {
        public Nullable<int> idRekomendasi { get; set; }
        public string NamaRekomendasi { get; set; }
        public string SingkatanRekomendasi { get; set; }

    }
}