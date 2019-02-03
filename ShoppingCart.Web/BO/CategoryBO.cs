using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class CategoryBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public Category GetCategory(int categoryId)
        {
            return context.Categories.Where(c => c.PKCategoryId == categoryId).SingleOrDefault();
        }
        public List<Category> GetCategories(bool? isActive = null, string categoryName = null)
        {
            try
            {
                IQueryable<Category> qry = context.Categories;
                if (isActive != null)
                    qry = qry.Where(c => c.IsActive == isActive);
                var q = (from c in qry
                         join u in context.UserProfiles on c.FKCreatedByUserId equals u.PKUserId
                         select new
                         {
                             PKCategoryId = c.PKCategoryId,
                             CategoryName = c.CategoryName,
                             CreatedDate = c.CreatedDate,
                             UpdatedDate = c.UpdatedDate,
                             FKCreatedByUserId = c.FKCreatedByUserId,
                             FKUpdatedByUserId = c.FKUpdatedByUserId,
                             CreatedByUser = u.UserName,
                             UpdatedByUser = u.UserName,
                             IsActive = c.IsActive
                         }).AsEnumerable().Select(x => new Category
                         {
                             PKCategoryId = x.PKCategoryId,
                             CategoryName = x.CategoryName,
                             CreatedDate = x.CreatedDate,
                             UpdatedDate = x.UpdatedDate,
                             FKCreatedByUserId = x.FKCreatedByUserId,
                             FKUpdatedByUserId = x.FKUpdatedByUserId,
                             CreatedByUser = x.CreatedByUser,
                             UpdatedByUser = x.UpdatedByUser,
                             IsActive = x.IsActive
                         });
                if (!string.IsNullOrEmpty(categoryName))
                    qry = qry.Where(cat => cat.CategoryName == categoryName);
                return q.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void InsertCategory(Category objCategory)
        {
            try
            {
                objCategory.CreatedDate = DateTime.Now;
                objCategory.FKCreatedByUserId = Helper.UserId;
                context.Categories.Add(objCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateCategory(Category objCategory)
        {
            try
            {
                objCategory.UpdatedDate = DateTime.Now;
                objCategory.FKUpdatedByUserId = Helper.UserId;
                context.Entry(objCategory).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCategory(int categoryId)
        {
            try
            {
                Category objCategory = context.Categories.Find(categoryId);
                context.Categories.Remove(objCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}