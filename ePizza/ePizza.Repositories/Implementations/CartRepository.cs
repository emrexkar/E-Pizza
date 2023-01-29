using ePizza.Data.Concrete.EntityFramework.Context;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICardRepository
    {
        public ePizzaContext _ePizzaContext
        {
            // context nesnesini burada kapsule(encapsulation) ederek almış olduk.
            get
            {
                return _ePizzaContext as ePizzaContext;
            }
        }


        public CartRepository(DbContext context) : base(context)
        {
        }

        public int DeleteProduct(Guid cardId, int productId)
        {
            var item = _ePizzaContext.CartItems.FirstOrDefault(c => c.CartId == cardId && c.Id == productId);

            if (item != null)
            {
                _ePizzaContext.CartItems.Remove(item);
                return _ePizzaContext.SaveChanges();
            }
            else
            {
                return 0;
            }

        }

        public int DeleteProduct(Guid cardId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Cart GetCart(Guid cartId)
        {
            return _ePizzaContext.Carts.Include("Product").FirstOrDefault(x => x.Id == cartId && x.IsActive == true);
        }


        public CartModel GetCartDetails(Guid cartId)
        {
            var model = (from cart in _ePizzaContext.Carts
                         where cart.Id == cartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Products = (from CartItem in _ePizzaContext.CartItems
                                         join item in _ePizzaContext.Products on CartItem.ProductId
                                         equals item.Id
                                         where CartItem.CartId == cartId
                                         select new ProductModel()
                                         {
                                             Id = CartItem.Id,
                                             Name = item.Name,
                                             Description = item.Description,
                                             ImageUrl = item.ImageUrl,
                                             Quantity = CartItem.Quantity,
                                             ProductId = item.Id,
                                             UnitPrice = item.UnitPrice,

                                         }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateProduct(Guid cardId, int userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateQuantity(Guid cardId, int quantity, int productId)
        {
            bool flag = false;
            var cart = GetCart(cardId);
            if (cart != null)
            {
                for (int i = 0; i < cart.CartItems.Count; i++) //Sepetin içindeki nesneleri dön
                {
                    if (cart.CartItems[i].Id == productId)
                    {
                        flag = true;
                        if (quantity < 0 && cart.CartItems[i].Quantity > 1) // Miktar küçükse sıfırdan ve mevcut sepet büyükse 1 den
                            cart.CartItems[i].Quantity += quantity;

                        else if (quantity > 0) // Mevcut sepet büyükse 0 dan 
                            cart.CartItems[i].Quantity += quantity; // Gelen değeri mevcut sepete eklee

                        break; // İşi bitir.
                    }
                }
                if (flag)
                {
                    return _ePizzaContext.SaveChanges();
                }
            }
            return 0;
        }
    }
}
