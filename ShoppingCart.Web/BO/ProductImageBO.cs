using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class ProductImageBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public ProductImage GetImage(int imageId)
        {
            return context.ProductImages.Where(i => i.PKImageId == imageId).SingleOrDefault();
        }
        public List<ProductImage> GetImages(int productId = 0)
        {
            try
            {
                IQueryable<ProductImage> qry = context.ProductImages;
                if (productId != 0)
                    qry = qry.Where(x => x.FKProductId == productId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertImage(ProductImage objImage)
        {
            try
            {
                context.ProductImages.Add(objImage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateImage(ProductImage objImage)
        {
            try
            {
                context.Entry(objImage).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteImage(int imageId)
        {
            try
            {
                ProductImage objImage = context.ProductImages.Find(imageId);
                context.ProductImages.Remove(objImage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}