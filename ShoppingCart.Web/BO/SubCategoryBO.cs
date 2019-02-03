using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class SubCategoryBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public SubCategory GetsubCategory(int subCategoryId)
        {
            return context.SubCategories.Where(s => s.PKSubCategoryId == subCategoryId).SingleOrDefault();
        }
        public List<SubCategory> GetSubCategories(int categoryId = 0, bool? isActive = null)
        {
            try
            {
                context.Configuration.LazyLoadingEnabled = false;
                IQueryable<SubCategory> qry = context.SubCategories;
                if (categoryId != 0)
                    qry = qry.Where(s => s.FKCategoryId == categoryId);
                if (isActive != null)
                    qry = qry.Where(s => s.IsActive == isActive);
                var q = (from s in qry
                         join u in context.UserProfiles on s.FKCreatedByUserId equals u.PKUserId
                         join c in context.Categories on s.FKCategoryId equals c.PKCategoryId
                         select new
                         {
                             PKSubCategoryId = s.PKSubCategoryId,
                             FKCategoryId = s.FKCategoryId,
                             CategoryName = c.CategoryName,
                             SubCategoryName = s.SubCategoryName,
                             CreatedDate = s.CreatedDate,
                             UpdatedDate = s.UpdatedDate,
                             FKCreatedByUserId = s.FKCreatedByUserId,
                             FKUpdatedByUserId = s.FKUpdatedByUserId,
                             CreatedByUser = u.UserName,
                             UpdatedByUser = u.UserName,
                             ImageName = s.ImageName,
                             ImagePath = s.ImagePath,
                             IsActive = s.IsActive
                         }).AsEnumerable().Select(x => new SubCategory
                         {
                             PKSubCategoryId = x.PKSubCategoryId,
                             CategoryName = x.CategoryName,
                             FKCategoryId = x.FKCategoryId,
                             SubCategoryName = x.SubCategoryName,
                             CreatedDate = x.CreatedDate,
                             UpdatedDate = x.UpdatedDate,
                             FKCreatedByUserId = x.FKCreatedByUserId,
                             FKUpdatedByUserId = x.FKUpdatedByUserId,
                             CreatedByUser = x.CreatedByUser,
                             UpdatedByUser = x.UpdatedByUser,
                             ImageName = x.ImageName,
                             ImagePath = x.ImagePath,
                             IsActive = x.IsActive
                         });
                return q.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertSubCategory(SubCategory objSubCategory)
        {
            try
            {
                objSubCategory.CreatedDate = DateTime.Now;
                objSubCategory.FKCreatedByUserId = Helper.UserId;
                context.SubCategories.Add(objSubCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateSubCategory(SubCategory objSubCategory)
        {
            try
            {
                objSubCategory.UpdatedDate = DateTime.Now;
                objSubCategory.FKUpdatedByUserId = Helper.UserId;
                context.Entry(objSubCategory).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteSubCategory(int subCategoryId)
        {
            try
            {
                SubCategory objSubCategory = context.SubCategories.Find(subCategoryId);
                context.SubCategories.Remove(objSubCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}