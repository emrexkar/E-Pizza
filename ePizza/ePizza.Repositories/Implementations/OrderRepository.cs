using ePizza.Data.Concrete.EntityFramework.Context;
using ePizza.Data.Concrete.EntityFramework.Mappings;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ePizza.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public ePizzaContext _ePizzaContext
        {
            // context nesnesini burada kapsule(encapsulation) ederek almış olduk.
            get
            {
                return _ePizzaContext as ePizzaContext;
            }
        }

        public OrderRepository(DbContext context) : base(context)
        {
        }

        public OrderModel getorderdetails(string id)
        {
            throw new NotImplementedException();
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in _ePizzaContext.Orders
                        join payment in _ePizzaContext.PaymentDetails on order.PaymentId equals
                        payment.Id
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = order.PaymentId,
                            CreatedDate = order.CreatedDate,
                            GrandTotal = payment.GrandTotal,
                            Locality = order.Locality,
                        });
           
            int itemCount = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);
            var pagedListData = new StaticPagedList<OrderModel>(orders, page, pageSize, itemCount);
            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = pageSize;
            pagingModel.TotalRows = itemCount;
            return pagingModel;
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            // Satısa göre kullanıcıları list ettik.
            return _ePizzaContext.Orders.Include(o => o.OrderItem).Where(o => o.UserId == userId).ToList();
        }
    }
}
