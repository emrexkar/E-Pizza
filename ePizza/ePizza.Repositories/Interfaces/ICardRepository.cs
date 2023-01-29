using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Interfaces
{
    public interface ICardRepository : IRepository<Cart>
    {
        Cart GetCart(Guid cartId);

        CartModel GetCartDetails(Guid cartId);

        int DeleteProduct(Guid cardId, int productId);
        int DeleteProduct(Guid cardId, int productId, int quantity);
        int UpdateProduct(Guid cardId, int userId);
        int UpdateQuantity(Guid cardId, int quantity,int productId);

    }
}
