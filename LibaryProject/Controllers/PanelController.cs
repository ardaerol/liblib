﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LibaryProject.Models.Entity;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SelectListItem = System.Web.Mvc.SelectListItem;
namespace LibaryProject.Controllers
{
	[Authorize]
	public class PanelController : Controller
	{
		// GET: Panel
#pragma warning disable IDE0044 // Add readonly modifier
		DbLibaryEntities db = new DbLibaryEntities();
#pragma warning restore IDE0044 // Add readonly modifier
		// [HttpGet]
		public ActionResult Index()
		{
			var uyemail = (string)Session["Mail"];
			var d1 = db.TblUyeler.Where(x => x.Mail == uyemail).Select(y => y.Fotograf).FirstOrDefault();
			ViewBag.d1 = d1;
			var degerler = db.TblUyeler.FirstOrDefault(z => z.Mail == uyemail);
			return View(degerler);
		}
		[HttpPost]
		public ActionResult Index2(TblUyeler p)
		{
			var kullanici = (string)Session["Mail"];
			var uye = db.TblUyeler.FirstOrDefault(x => x.Mail == kullanici);
			uye.Sifre = p.Sifre;
			uye.Ad = p.Ad;
			uye.Fotograf = p.Fotograf;
			uye.Soyad = p.Soyad;
			uye.Okul = p.Okul;
			uye.KullanıcıAdi = p.KullanıcıAdi;
			uye.Telefon = p.Telefon;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult KitapHareket()
		{
			var kullanici = (string)Session["Mail"];
			var id = db.TblUyeler.Where(x => x.Mail == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
			var degerler = db.TblHareket.Where(x => x.Uye == id).ToList();
			return View(degerler);
		}
		public ActionResult Duyurular()
		{
			var duyurular = db.TblDuyurular.ToList();
			return View(duyurular);
		}
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("GirisYap", "Login");
		}
		public ActionResult BulunanKitaplar(string p)
		{
			var kitaplar = from k in db.TblKitap select k;
			kitaplar = kitaplar.Where(x => x.Durum == true);
			if (!string.IsNullOrEmpty(p))
			{
				kitaplar = kitaplar.Where(x => x.Ad.Contains(p));
			}
			//  var kitaplar=db.TblKitap.ToList();
			return View(kitaplar.ToList());
		}
	}
}