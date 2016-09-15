using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SamMusicStoreWebSite.Models;
using SamMusicStoreWebSite.ViewModel;

namespace SamMusicStoreWebSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            using (var db = new SamMusicStoreEntities())
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);

                //Set up our ViewModel
                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = cart.GetCartItems(),
                    CartTotal = cart.GetTotal()
                };

                //Return the view
                return View(viewModel);
            }
           
        }


        public ActionResult AddToCart(int id)
        {
            //Retrieve the album from the database 
            using(var db = new SamMusicStoreEntities())
	{
            var addedAlbum = db.Albums.Single(m => m.AlbumId == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedAlbum);

            //Go back to main page
            return RedirectToAction("Index");  
	}
            
        }

        //[HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            using(var db = new SamMusicStoreEntities())
	{
		 //// Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string albumName = db.Carts.Single(item => item.Record == id).Album.Title;

                //Remove from cart
            int itemCount = cart.RemoveFromCart(id);

                //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) + "has been removed from your shopping cart",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
	}
            
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}