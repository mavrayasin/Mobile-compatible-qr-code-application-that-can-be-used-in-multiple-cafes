using CokluAdisyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CokluAdisyon.Controllers
{

    public class HomeController : Controller
    {
        adisyondbEntities ent = new adisyondbEntities();
        public ActionResult Index()
        {
            kafelercls kc = new kafelercls();
            kc.kafelers = ent.Kafeler.ToList();
            return View(kc);
        }
        [HttpPost]
        public ActionResult Index(string kulladi, string pass, string kafeSecilen)
        {
            Kafeler kafeler = new Kafeler();
            kafeler.KafeAdi = kafeSecilen;
        
            if (kulladi != "" && pass != "")
            {
                var control = ent.Kullanici.Where(i => i.KullaniciAdi == kulladi && i.Sifre == pass).FirstOrDefault();

                if (control != null)
                {
                    Session["kullBilgi"] = control;
                    return RedirectToAction("Anasayfa", "Adisyon");
                }
                else
                {
                   //Kullanici bilgileri yanlış
                }
            }
            
               
            return View();
        }
       
    }
}