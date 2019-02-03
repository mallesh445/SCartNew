using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class ProductBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public Product GetProduct(int productId)
        {
            return context.Products.Where(p => p.PKProductId == productId).SingleOrDefault();
        }
        public List<Product> GetProducts(int categoryId = 0, int subCategoryId = 0, bool? isActive = null)
        {
            try
            {
                IQueryable<Product> qry = context.Products;
                if (categoryId != 0)
                {
                    qry = context.Products.Where(p => p.FKCategoryId == categoryId);
                }
                if (subCategoryId != 0)
                {
                    qry = context.Products.Where(p => p.FKSubCategoryId == subCategoryId);
                }
                if (isActive != null)
                {
                    qry = context.Products.Where(p => p.IsActive == isActive);
                }
                var q = (from p in qry
                         join u in context.UserProfiles on p.FKCreatedByUserId equals u.PKUserId
                         join c in context.Categories on p.FKCategoryId equals c.PKCategoryId
                         join s in context.SubCategories on p.FKSubCategoryId equals s.PKSubCategoryId
                         join pi in context.ProductImages on p.PKProductId equals pi.FKProductId
                         select new
                         {
                             PKProductId = p.PKProductId,
                             FKCategoryId = p.FKCategoryId,
                             FKSubCategoryId = p.FKSubCategoryId,
                             CategoryName = c.CategoryName,
                             SubCategoryName = s.SubCategoryName,
                             ProductName = p.ProductName,
                             ImageName = pi.ImageName,
                             ImagePath = pi.ImagePath,
                             Description = p.Description,
                             Quantity = p.Quantity,
                             Price = p.Price,
                             CreatedDate = p.CreatedDate,
                             UpdatedDate = p.UpdatedDate,
                             CreatedByUser = u.UserName,
                             UpdatedByUser = u.UserName,
                             IsActive = p.IsActive
                         }).AsEnumerable().Select(x => new Product
                         {
                             PKProductId = x.PKProductId,
                             FKCategoryId = x.FKCategoryId,
                             FKSubCategoryId = x.FKSubCategoryId,
                             CategoryName = x.CategoryName,
                             SubCategoryName = x.SubCategoryName,
                             ProductName = x.ProductName,
                             ImageName = x.ImageName,
                             ImagePath = x.ImagePath,
                             Description = x.Description,
                             Quantity = x.Quantity,
                             Price = x.Price,
                             CreatedDate = x.CreatedDate,
                             UpdatedDate = x.UpdatedDate,
                             CreatedByUser = x.CreatedByUser,
                             UpdatedByUser = x.UpdatedByUser,
                             IsActive = x.IsActive
                         });
                //var q = (from p in qry
                //         join u in context.UserProfiles on p.FKCreatedByUserId equals u.PKUserId
                //         join c in context.Categories on p.FKCategoryId equals c.PKCategoryId
                //         join s in context.SubCategories on p.FKSubCategoryId equals s.PKSubCategoryId
                //         join pi in context.ProductImages on p.PKProductId equals pi.FKProductId
                //         select new
                //         {

                //         }

                return q.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void InsertProduct(Product objProduct)
        {
            try
            {
                objProduct.CreatedDate = DateTime.Now;
                objProduct.FKCreatedByUserId = Helper.UserId;
                context.Products.Add(objProduct);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProduct(Product objProduct)
        {
            try
            {
                objProduct.UpdatedDate = DateTime.Now;
                objProduct.FKUpdatedByUserId = Helper.UserId;
                context.Entry(objProduct).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteProduct(int productId)
        {
            try
            {
                Product objProduct = context.Products.Find(productId);
                context.Products.Remove(objProduct);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}