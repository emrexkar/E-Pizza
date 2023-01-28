using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int userId);

        //TODO: OrderModel eklenecektir
        OrderModel getorderdetails(string id);

        //TODO: PagingListModel eklenecektir
         PagingListModel<OrderModel> GetOrderList(int page, int pageSize);

    }
}
