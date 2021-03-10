using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Sumber : Default
    {
        public Nullable<int> idSumber { get; set; }
        public Nullable<int> idRekomendasi { get; set; }
        public string NamaSumber { get; set; }
        public string SingkatanSumber { get; set; }
        public bool isRAM { get; set; }


    }
}