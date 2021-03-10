using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
namespace I_ACT.Domain
{
    public class Log : Default 
    {
        public Nullable<int> recid { get; set; }
        public string menu { get; set; }
        public string aktifitas { get; set; }
        public string deskripsi { get; set; }
    }
}