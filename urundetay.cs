using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CokluAdisyon.Models
{
    public class urundetay
    {
        public List<Urun> urunler { get; set; }
        public List<UrunGruplari> urungruplar { get; internal set; }
    }
}