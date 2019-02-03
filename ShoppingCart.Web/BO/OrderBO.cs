using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class OrderBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public Order GetOrder(int orderId)
        {
            return context.Orders.Where(o => o.PKOrderId == orderId).SingleOrDefault();
        }
        public List<Order> GetOrders(DateTime? fromDate = null, DateTime? toDate = null, string userName = "", string status = "")
        {
            try
            {
                IQueryable<Order> qry = context.Orders;
                if (status != "" && status != "All")
                    qry = qry.Where(order => order.Status == status);
                if (fromDate.HasValue && toDate.HasValue)
                    qry = qry.Where(order => order.OrderDate > fromDate.Value && order.OrderDate < toDate.Value);

                var q = (from o in qry
                         join u in context.UserProfiles on o.FKCustomerUserId equals u.PKUserId
                         join od in context.OrderDetails on o.PKOrderId equals od.FKOrderId
                         where u.UserName.Contains(userName == "" ? u.UserName : userName)
                         select new
                         {
                             PKOrderId = o.PKOrderId,
                             FKCustomerUserId = o.FKCustomerUserId,
                             FKBillingAddressId = o.FKBillingAddressId,
                             OrderDate = o.OrderDate,
                             DeliveredDate = o.DeliveredDate,
                             FKProcessByUserId = o.FKProcessByUserId,
                             CustomerComment = o.CustomerComment,
                             AdminComment = o.AdminComment,
                             Status = o.Status,
                             UserName = u.UserName,
                             Cost = od.Cost,
                             Quantity = od.Quantity,
                             FKProductId = od.FKProductId

                         }).AsEnumerable().Select(x => new Order
                         {
                             PKOrderId = x.PKOrderId,
                             FKCustomerUserId = x.FKCustomerUserId,
                             FKBillingAddressId = x.FKBillingAddressId,
                             OrderDate = x.OrderDate,
                             DeliveredDate = x.DeliveredDate,
                             FKProcessByUserId = x.FKProcessByUserId,
                             CustomerComment = x.CustomerComment,
                             AdminComment = x.AdminComment,
                             Status = x.Status,
                             UserName = x.UserName,
                             Cost = x.Cost,
                             Quantity = x.Quantity,
                             FKProductId = x.FKProductId
                         });
                return q.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertOrder(Order objOrder)
        {
            try
            {
                objOrder.FKCustomerUserId = Helper.UserId;
                objOrder.FKProcessByUserId = Helper.UserId;
                objOrder.OrderDate = DateTime.Now;
                context.Orders.Add(objOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOrder(Order objOrder)
        {
            try
            {
                context.Entry(objOrder).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOrder(int orderId)
        {
            try
            {
                Order objOrder = context.Orders.Find(orderId);
                context.Orders.Remove(objOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}