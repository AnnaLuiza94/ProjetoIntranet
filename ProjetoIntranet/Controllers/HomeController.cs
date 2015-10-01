using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoIntranet.Models;

namespace ProjetoIntranet.Controllers
{
    public class HomeController : Controller
    {
        private IntranetContext db = new IntranetContext();

        public ActionResult Index()
        {
            var news = db.News.Include(n => n.User);
            return View(news.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}