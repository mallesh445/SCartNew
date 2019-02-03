using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class ErrorHistoryBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public ErrorHistory GetErrorHistory(int errorHistoryId)
        {
            return context.ErrorHistories.Where(e => e.PKErrorId == errorHistoryId).SingleOrDefault();
        }
        public List<ErrorHistory> GetErrorHistories()
        {
            try
            {
                return context.ErrorHistories.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void InsertErrorHistory(ErrorHistory objErrorHistory)
        {
            try
            {
                objErrorHistory.ErrorLogTime = DateTime.Now;
                if (!string.IsNullOrEmpty(Helper.UserData))
                    objErrorHistory.FKUserId = Helper.UserId;
                context.ErrorHistories.Add(objErrorHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateErrorHistory(ErrorHistory objErrorHistory)
        {
            try
            {
                context.Entry(objErrorHistory).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteErrorHistory(int errorHistoryId)
        {
            try
            {
                ErrorHistory objErrorHistory = context.ErrorHistories.Find(errorHistoryId);
                context.ErrorHistories.Remove(objErrorHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}