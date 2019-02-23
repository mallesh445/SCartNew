using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Utilities;
using ShoppingCart.Utilities.ExcelModel;
using ShoppingCart.Web;
using ShoppingCart.Web.BO;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        CategoryBO objCategoryBO = new CategoryBO();
        // GET: /Admin/Category/
        public ActionResult Index()
        {
            //TempData["Success"] = "Inserted Successfully.";
            if (TempData["Success"] != null)
            {
                TempData["Success"] = TempData["Success"];
            }
            if(TempData["Error"]!=null)
                TempData["Error"] = TempData["Error"];
            return View(objCategoryBO.GetCategories());
        }

        [HttpPost]
        public ActionResult ImportCategories(HttpPostedFileBase postedExcelFile)
        {
            if (ModelState.IsValid)
            {
                if (postedExcelFile == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                    TempData["Error"] = "Please Upload Your file.";
                    return RedirectToAction("Index");
                }
                else if (postedExcelFile.ContentLength > UtilityConstants.MaxContentLength)
                {
                    TempData["Error"] = "SizeExceed";
                    return RedirectToAction("Index");
                    //return Content("SizeExceed");
                }
                else if (postedExcelFile.ContentLength > 0)
                {
                    string path = ExcelHelper.SavePathForThePostedFile(postedExcelFile);

                    if (!(path.Contains("xlsx") || path.Contains("xls")))
                        return Content("FileFormatError");
                    
                    try
                    {
                        List<CategoryImportExcel> records = ExcelHelper.ReadSheet<CategoryImportExcel>(path, true, 0, null, true).ToList();
                        records = records.Where(r => !string.IsNullOrEmpty(r.CategoryName) && !string.IsNullOrEmpty(r.CreatedByUser)).ToList();
                        if (records.Count > 0)
                        {
                            objCategoryBO.InsertCategoryInBulk(records);
                            TempData["Success"] = $"\"NumberOfRecords Uploaded\" : {records.Count()}";
                            return RedirectToAction("Index");
                        }
                        TempData["Success"] = $"\"NumberOfRecords Uploaded\" : 0";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("InValidZipCode"))
                        {
                            return Content(ex.Message);
                        }
                    }
                }
            }
            return Content("Error");
            //if (Request.Files["postedExcelFile"].ContentLength > 0)
            //{
            //    string fileExtension = System.IO.Path.GetExtension(Request.Files["postedExcelFile"].FileName);
            //}
           // return View();
        }

        // GET: /Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                objCategoryBO.InsertCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: /Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult CreateNewCategory(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                Category _category = new Category() { CategoryName = categoryName };
                objCategoryBO.InsertCategory(_category);
            }
            return Json(categoryName, JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                objCategoryBO.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objCategoryBO.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddData(MyList dataList)
        {
            //return Content(ListID + " " + ItemName);
            return Json(dataList.ItemName, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult AddData(string ListID, string ItemName)
        //{
        //    return Content(ListID + " " + ItemName+" From Category admin controller");
        //}
    }

    public class MyList
    {
        public int ListId { get; set; }
        public string ItemName { get; set; }
    }
}
