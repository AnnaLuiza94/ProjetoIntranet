using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProjetoIntranet.Models;
using ProjetoIntranet.ViewModel;

namespace ProjetoIntranet.Controllers
{
    public class NoticiasController : Controller
    {
        private IntranetContext db = new IntranetContext();

        // GET: Noticias
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.User);
            if(news != null)
                return View(news.ToList());

            return View();
        }

        // GET: Noticias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Noticias/Create
        public ActionResult Create()
        {
            var Departament = db.Departament;
            var viewModel = new List<DepartamentSelectViewModel>();
            foreach (var item in Departament)
            {
                viewModel.Add(new DepartamentSelectViewModel
                {
                    DepartamentId = item.ID,
                    Name = item.Name,
                    IsSelected = false
                });
            }
            ViewBag.DepartamentPopulate = viewModel;
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID, Title,Content,CreationDate")] News news, List<int> selectedDepartaments)
        {
            if (ModelState.IsValid)
            {
                //news.CreationDate = DateTime.Now;
                news.User = db.Users.Find(news.UserID);

                if (selectedDepartaments != null)
                {
                    foreach (var selectedDepartamentId in selectedDepartaments)
                    {
                        var departamento = db.Departament.Find(selectedDepartamentId);
                        news.Departaments.Add(departamento);

                    }
                }
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var departamentos = new List<Departament>();
            foreach (var departamento in db.Departament.ToList())
            {
                departamentos.Add(departamento);
            }
            ViewBag.DepartamentID = new SelectList(db.Departament, "ID", "Name", news.Departaments);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", news.UserID);
            return View(news);
        }

        // GET: Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            News news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();

            var allDepartaments = db.Departament;
            var newsDepartaments = new HashSet<int>(news.Departaments.Select(c => c.ID));
            var viewModel = new List<DepartamentSelectViewModel>();
            foreach (var departamento in allDepartaments)
            {
                viewModel.Add(new DepartamentSelectViewModel
                {
                    DepartamentId = departamento.ID,
                    Name = departamento.Name,
                    IsSelected = newsDepartaments.Contains(departamento.ID)
                });
            }
            ViewBag.DepartamentPopulate = viewModel;
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", news.UserID);
            return View(news);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Title,Content,CreationDate")] News news, List<int> selectedDepartaments)
        {
            News newsToUpdate = db.News.Find(news.ID);
            if (ModelState.IsValid)
            {

                if (selectedDepartaments != null)
                    foreach (var selectedDepartamentId in selectedDepartaments)
                    {
                        var departamento = db.Departament.Find(selectedDepartamentId);
                        newsToUpdate.Departaments.Add(departamento);
                    }

                if (selectedDepartaments == null)
                    newsToUpdate.Departaments = new List<Departament>();

                db.Entry(newsToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", news.UserID);
            return View(newsToUpdate);
        }

        // GET: Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void UpdateDepartamentNews(List<int> selectedDepartaments, News newsToUpdate)
        {
            if (selectedDepartaments == null)
            {
                newsToUpdate.Departaments = new List<Departament>();
                return;
            }

            var selectedDepartamentsHs = new HashSet<int>(selectedDepartaments);
            var newsDepartament = new HashSet<int>
                (newsToUpdate.Departaments.Select(c => c.ID));

            if (selectedDepartaments != null)
            {
                foreach (var selectedDepartamentId in selectedDepartaments)
                {
                    var departamento = db.Departament.Find(selectedDepartamentId);
                    newsToUpdate.Departaments.Add(departamento);

                }
            }

            //foreach (var departament in db.Departament)
            //{
            //    if (selectedDepartamentsHs.Contains(departament.ID))
            //    {
            //        if (!newsDepartament.Contains(departament.ID))
            //        {
            //            newsToUpdate.Departaments.Add(departament);
            //        }
            //    }
            //    else
            //    {
            //        if (newsDepartament.Contains(departament.ID))
            //        {
            //            newsToUpdate.Departaments.Remove(departament);
            //        }
            //    }
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
