using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Pekerja : Default
    {
        public Nullable<int> recid { get; set; }
        public string NoPek { get; set; }
        public string namapegawai { get; set; }
        public string kodeJabatan { get; set; }
        public Nullable<int> NoLevel { get; set; }
        public Nullable<int> NoStatus { get; set; }
        public Nullable<int> idJabatan { get; set; }
        public string desccostcenter { get; set; }
        public string direktur { get; set; }
        public string jabatan { get; set; }
        public string namaJabatan { get; set; }
        public string bagian { get; set; }
        public Nullable<int> nobagian { get; set; }
        public Nullable<int> nofungsi { get; set; }
        public string email { get; set; }
        public string PRL { get; set; }
        public DateTime tgllahir { get; set; }
        public DateTime tmt_dinas { get; set; }

        public Nullable<DateTime> vTglAwal { get; set; }
        public Nullable<DateTime> vTglAkhir { get; set; }
    }
}