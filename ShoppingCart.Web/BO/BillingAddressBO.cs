using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class BillingAddressBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public BillingAddress GetBillingAddress(int addressId)
        {
            return context.BillingAddresses.Where(b => b.PKAddressId == addressId).SingleOrDefault();
        }
        public List<BillingAddress> GetBillingAddresses()
        {
            try
            {
                return context.BillingAddresses.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertBillingAddress(BillingAddress objBillngAddress)
        {
            try
            {
                objBillngAddress.FKUserId = Helper.UserId;
                context.BillingAddresses.Add(objBillngAddress);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateBillingAddress(BillingAddress objBillngAddress)
        {
            try
            {
                context.Entry(objBillngAddress).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBillingAddress(int addressId)
        {
            try
            {
                BillingAddress objBillngAddress = context.BillingAddresses.Find(addressId);
                context.BillingAddresses.Remove(objBillngAddress);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}