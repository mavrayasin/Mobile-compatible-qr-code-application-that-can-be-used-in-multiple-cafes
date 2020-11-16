using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CokluAdisyon.Models;
namespace CokluAdisyon.Controllers
{

    public class AdisyonController : Controller
    {
        adisyondbEntities ent = new adisyondbEntities();
        [HttpGet]
        public ActionResult Anasayfa(string kafeSecilen)
        {
            if (Session["kullBilgi"] != null)
            {
                Kullanici kull = Session["kullBilgi"] as Kullanici;

                masalarcls mc = new masalarcls();

                mc.masalars = ent.Masalar.Where(i => i.Kafe_ID ==kull.Kafe_ID).ToList();
                mc.siparislers = ent.Siparis.Where(i => i.HesapOdendi == false).ToList();
                return View(mc);
            }
            else
            {
                return RedirectToAction("home","index");
            }
        }

        public ActionResult MasaDetay(int masa_id)
        {
           
                masadetaycls mdc = new masadetaycls();
                mdc.masa = ent.Masalar.Where(a => a.MasaID == masa_id).FirstOrDefault();
                Siparis yeni_siparis = ent.Siparis.Where(i => i.Masa_ID == masa_id && i.HesapOdendi == false).FirstOrDefault();

                if (yeni_siparis == null)
                {
                    yeni_siparis = new Siparis();
                    yeni_siparis.HesapOdendi = false;
                    yeni_siparis.Masa_ID = masa_id;
                    ent.Siparis.Add(yeni_siparis);
                    ent.SaveChanges();
                }
            if (Session["kullbilgi"] != null)
            {

                Kullanici kull = Session["kullBilgi"] as Kullanici;
                mdc.siparis = yeni_siparis;
                mdc.urunler = ent.Urun.Where(i=>i.Kafe_ID==kull.Kafe_ID).ToList();
                mdc.masa_siparisleri = ent.SiparisDetay.Where(i => i.Siparis_ID == yeni_siparis.SiparisID).ToList();
          

            return View(mdc);
        }
            else
            {
                return RedirectToAction("home", "MasaDetay");
            }
        }
        

        [HttpPost]
        public ActionResult siparisUrunEkle(int siparis_id, int urun_id, int urunAdet, int masa_id/*masa id redirect to action için*/)
        {
            SiparisDetay sd = new SiparisDetay();

            sd.Adet = urunAdet;
            sd.Siparis_ID = siparis_id;
            sd.Urun_ID = urun_id;

            ent.SiparisDetay.Add(sd);
            ent.SaveChanges();

            return RedirectToAction("MasaDetay", new { masa_id = masa_id });
        }
        public ActionResult HesapDefteri(int? OdemeTuru)
        {
            hesapdetay hd = new hesapdetay();

            hd.hesaplar = ent.Hesap.ToList();


            return View(hd);

        }
        [HttpPost]
        public ActionResult HesapIslem(int siparis_id, float tutar, int odeme_turu)
        {
            Hesap hsp = new Hesap();

            hsp.OdemeTuru = odeme_turu;
            hsp.Siparis_ID = siparis_id;
            hsp.Tarih = DateTime.Now;
            hsp.Tutar = tutar;

            ent.Hesap.Add(hsp);
            ent.SaveChanges();

            Siparis sp = ent.Siparis.Where(i => i.SiparisID == siparis_id).FirstOrDefault();

            sp.HesapOdendi = true;

            ent.SaveChanges();

            return RedirectToAction("AnaSayfa");
        }

        public ActionResult UrunIslemleri()
        {
            if (Session["kullbilgi"] != null)
            {

                Kullanici kull = Session["kullBilgi"] as Kullanici;
                urundetay udt = new urundetay();
                udt.urunler = ent.Urun.Where(i => i.Kafe_ID == kull.Kafe_ID).ToList();

                return View(udt);
            }
            else
            {
                return RedirectToAction("home", "UrunIslemleri");
            }
        }
        
    }
}