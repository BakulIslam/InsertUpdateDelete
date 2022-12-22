using Insert_Update_Delete_In_Modal_PopUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insert_Update_Delete_In_Modal_PopUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeCRUDaspEntities db;

        public HomeController() 
        {
            db = new EmployeCRUDaspEntities();
        }
        public ActionResult Index()
        {
           
            var data = db.Students.ToList();
            ViewBag.Data = data;

            return View();
        }
        [HttpPost]
        public JsonResult SaveStudent(Student std)
        {
            bool result = false;
            if(std != null)
            {
                db.Students.Add(std);
                db.SaveChanges();
              
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllData()
        {
            var data = db.Students.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult Edit(int id)
        {
            bool result = false;
            var data = db.Students.Find(id);
           // var d = db.Students.Where(m => m.Id == id).ToList();
            if(data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(Student std)
        {
            bool result = false;
            if(std != null)
            {
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult  Delete(int id)
        {
               var data= db.Students.Find(id);
                db.Entry(data).State = EntityState.Deleted;
                db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}