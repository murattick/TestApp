using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApi.Models;
using PagedList.Mvc;
using PagedList;
using TestApi.DAL;

namespace TestApi.Controllers
{
    public class CategoriesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        // GET: Categories
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(unitOfWork.CategoryRepository.Get().ToPagedList(pageNumber, pageSize));
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.CategoryRepository.GetByIDAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Discription")] Category category)
        {
            using (MyContext db = new MyContext())
            {
                if (ModelState.IsValid)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            category.Id = Guid.NewGuid();
                            await unitOfWork.CategoryRepository.InsertAsync(category);
                            await unitOfWork.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }     
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.CategoryRepository.GetByIDAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Discription")] Category category)
        {
            using (MyContext db = new MyContext())
            {
                if (ModelState.IsValid)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            await unitOfWork.CategoryRepository.UpdateAsync(category);
                            await unitOfWork.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
           
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await unitOfWork.CategoryRepository.GetByIDAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (MyContext db = new MyContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Category category = await unitOfWork.CategoryRepository.GetByIDAsync(id);
                        await unitOfWork.CategoryRepository.DeleteAsync(category);
                        await unitOfWork.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
