using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CokluAdisyon.Models
{
    public class masadetaycls
    {
        public Masalar masa { get; set; }
        public List<Urun> urunler { get; set; }
        public Siparis siparis { get; set; }
        public List<SiparisDetay> masa_siparisleri { get; set; }
    }
}