using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CokluAdisyon.Models
{
    public class masalarcls
    {
        public List<Masalar>masalars{get;set;}
        public List<Siparis>siparislers{ get; set; }
        public List<Kafeler> kafelers { get; set; }
        public List<Kullanici> kullanicis { get; set; }

    }
}