using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamMusicStoreWebSite.Models
{
    public partial class ShoppingCart
    {
        SamMusicStoreEntities db = new SamMusicStoreEntities();

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album)
        {
            // Get the matching cart and album instances 
            var cartItem = db.Carts.SingleOrDefault(m => m.CartId == ShoppingCartId && m.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = db.Carts.Single(
                m => m.CartId == ShoppingCartId && m.Record == id);

            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }

                //save changes
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(m => m.CartId == ShoppingCartId);

            foreach (var item in cartItems)
            {
                db.Carts.Remove(item);
            }

            //save changes
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(m => m.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {

            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart 
                orderTotal += (item.Count * item.Album.Price);

                db.OrderDetails.Add(orderDetail);
            }

           // Set the order's total to the orderTotal count 
            order.Total = orderTotal;

            //Save the order
            db.SaveChanges();

            //Empty the shopping cart
            EmptyCart();

            //Empty the shopping cart
            EmptyCart();

            //Return the OrderId as the confirmation number
            return order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                
            
            else
            {
                // Generate a new random GUID
                Guid tempCartId = Guid.NewGuid();

                // Send tempCartId back to client as a cookie 
                context.Session[CartSessionKey] = tempCartId.ToString();
            }
           }
            return context.Session[CartSessionKey].ToString();
        }

        //When a user has logged in, migrate their shopping cart to //be associated with their username
        public void MigrateCart(string userName)
        {

            var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;

            }
            db.SaveChanges();

        }

        
    }
}