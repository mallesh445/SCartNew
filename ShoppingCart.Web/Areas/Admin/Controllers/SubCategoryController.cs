using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Web;
using ShoppingCart.Web.BO;
using System.Web.UI.WebControls;
using ShoppingCart.Utilities;
using ShoppingCart.Utilities.ExcelModel;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        SubCategoryBO objSubCategoryBO = new SubCategoryBO();
        CategoryBO objCartegoryBO = new CategoryBO();

        // GET: /Admin/SubCategory/
        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            return View(objSubCategoryBO.GetSubCategories());
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            if (fc["btnSearch"] == "Search" && fc["Category"]!="")
            {
                int categoryId = Convert.ToInt32(fc["Category"]);
                ViewBag.CategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName", categoryId);
                return View(objSubCategoryBO.GetSubCategories(categoryId, true));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
                return View(objSubCategoryBO.GetSubCategories());
            }
        }
        // GET: /Admin/SubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = objSubCategoryBO.GetsubCategory(id.Value);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: /Admin/SubCategory/Create
        public ActionResult Create()
        {
            ViewBag.FKCategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            return View();
        }

        /// <summary>
        /// ImportSubCategories
        /// </summary>
        /// <param name="postedExcelFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportSubCategories(HttpPostedFileBase postedExcelFile)
        {
            if (ModelState.IsValid)
            {
                if (postedExcelFile == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (postedExcelFile.ContentLength > UtilityConstants.MaxContentLength)
                {
                    return Content("SizeExceed");
                }
                else if (postedExcelFile.ContentLength > 0)
                {
                    string path = ExcelHelper.SavePathForThePostedFile(postedExcelFile);

                    if (!(path.Contains("xlsx") || path.Contains("xls")))
                        return Content("FileFormatError");

                    try
                    {
                        List<SubCategoryImportExcel> records = ExcelHelper.ReadSheet<SubCategoryImportExcel>(path, true, 0, null, true).ToList();
                        records = records.Where(r => !string.IsNullOrEmpty(r.SubCategoryName)).ToList();
                        if (records.Count > 0)
                        {
                            objSubCategoryBO.InsertSubCategoriesInBulk(records);
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

        // POST: /Admin/SubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subcategory, HttpPostedFileBase Images, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                subcategory.FKCategoryId = Convert.ToInt32(fc["CategoryName"]);
                string imageName = System.IO.Path.GetFileName(Images.FileName);
                Images.SaveAs(Server.MapPath("~/Images/" + imageName));
                subcategory.ImageName = imageName;
                subcategory.ImagePath = "/Images/" + imageName;
                objSubCategoryBO.InsertSubCategory(subcategory);
                return RedirectToAction("Index");
            }

            ViewBag.FKCategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName", subcategory.FKCategoryId);
            return View(subcategory);
        }

        // GET: /Admin/SubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = objSubCategoryBO.GetsubCategory(id.Value);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName", subcategory.FKCategoryId);
            return View(subcategory);
        }

        // POST: /Admin/SubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategory subcategory, HttpPostedFileBase txtFile)
        {
            if (ModelState.IsValid)
            {
                if (txtFile != null)
                {
                    string imageName = System.IO.Path.GetFileName(txtFile.FileName);
                    txtFile.SaveAs(Server.MapPath("~/Images/" + imageName));
                    subcategory.ImageName = imageName;
                    subcategory.ImagePath = "/Images/" + imageName;
                }
                objSubCategoryBO.UpdateSubCategory(subcategory);
                return RedirectToAction("Index");
            }
            ViewBag.FKCategoryId = new SelectList(objCartegoryBO.GetCategories(true), "PKCategoryId", "CategoryName", subcategory.FKCategoryId);
            return View(subcategory);
        }

        // GET: /Admin/SubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = objSubCategoryBO.GetsubCategory(id.Value);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: /Admin/SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objSubCategoryBO.DeleteSubCategory(id);
            return RedirectToAction("Index");
        }
    }
}
