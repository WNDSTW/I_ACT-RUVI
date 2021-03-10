using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_ACT.Domain
{
    public class Notulen :Default
    {
        public Nullable<int> idRekomendasi { get; set; }
        public Nullable<int> idSumber { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> prioritas { get; set; }
        public Nullable<int> idSeverity { get; set; }
        public Nullable<int> idProbability { get; set; }
        public Nullable<int> idPotential { get; set; }
        public string idfungsi { get; set; }
        public string noNotulen { get; set; }
        public string noNotulenDetail { get; set; }
        public string nopekConceptor { get; set; }
        public string judulNotulen { get; set; }
        public string noDokumen { get; set; }
        public string namaRekomendasi { get; set; }
        public string namaSumber { get; set; }
        public string namaFungsi { get; set; }
        public string namaStatus { get; set; }
        public string namaPrioritas { get; set; }
        public string fname { get; set; }
        public string subjek { get; set; }
        public string isi { get; set; }
        public string nopekPIC { get; set; }
        public string namaPIC { get; set; }
        public string nopekDelegasi { get; set; }
        public string namaDelegasi { get; set; }
        public string noteDelegasi { get; set; }
        public string nopekReviewer { get; set; }
        public string namaReviewer { get; set; }
        public string namaPotential { get; set; }
        public bool isDraft { get; set; }
        public Nullable<DateTime> tglJatuhTempo { get; set; }
        public Nullable<DateTime> tglDelegasi { get; set; }
        public Nullable<DateTime> tglClosed { get; set; }

        public Nullable<DateTime> tglNotulen { get; set; }

        public Nullable<DateTime> ModifiedOn { get; set; }
    
    }
}