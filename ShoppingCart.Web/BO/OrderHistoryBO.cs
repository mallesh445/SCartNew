using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class OrderHistoryBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public OrderHistory GetOrderHistory(int orderId)
        {
            return context.OrderHistories.Where(o => o.FKOrderId == orderId).SingleOrDefault();
        }
        public List<OrderHistory> GetOrderHistories(int userId = 0)
        {
            try
            {
                IQueryable<OrderHistory> qry = context.OrderHistories;
                var q = (from oh in qry
                         join o in context.Orders on oh.FKOrderId equals o.PKOrderId
                         where o.FKCustomerUserId == (userId == 0 ? o.FKCustomerUserId : userId)
                         select new
                         {
                             FKOrderId = oh.FKOrderId,
                             Description = oh.Description,
                             Status = oh.Status,
                             DateChanged = oh.DateChanged,
                             CustomerUserId = o.FKCustomerUserId
                         }).AsEnumerable().Select(x => new OrderHistory
                         {
                             FKOrderId = x.FKOrderId,
                             Description = x.Description,
                             Status = x.Status,
                             DateChanged = x.DateChanged,
                             CustomerUserId = x.CustomerUserId
                         });
                return q.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertOrderHistory(OrderHistory objOrderHistory)
        {
            try
            {
                objOrderHistory.DateChanged = DateTime.Now;
                context.OrderHistories.Add(objOrderHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateOrderHistory(OrderHistory objOrderHistory)
        {
            try
            {
                context.Entry(objOrderHistory).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOrderHistory(int orderHistoryId)
        {
            try
            {
                OrderHistory objOrderHistory = context.OrderHistories.Find(orderHistoryId);
                context.OrderHistories.Remove(objOrderHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}