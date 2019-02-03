using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.BO
{
    public class OrderDetailBO
    {
        ShoppingStoreEntities context = new ShoppingStoreEntities();
        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            return context.OrderDetails.Where(o => o.PKOrderDetailId == orderDetailId).SingleOrDefault();
        }
        public List<OrderDetail> GetOrderDetails(int orderId = 0)
        {
            try
            {
                context.Configuration.LazyLoadingEnabled = false;
                IQueryable<OrderDetail> qry = context.OrderDetails;
                if (orderId > 0)
                {
                    qry = qry.Where(ordDetails => ordDetails.FKOrderId == orderId);
                }
                return qry.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertOrderDetail(OrderDetail objOrderDetail)
        {
            try
            {
                context.OrderDetails.Add(objOrderDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOrderDetail(OrderDetail objOrder)
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
        public void DeleteOrderDetail(int orderId)
        {
            try
            {
                OrderDetail objOrder = context.OrderDetails.Find(orderId);
                context.OrderDetails.Remove(objOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}