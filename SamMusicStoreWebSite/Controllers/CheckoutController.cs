using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SamMusicStoreWebSite.Models;
using SamMusicStoreWebSite.ViewModel;

namespace SamMusicStoreWebSite.Controllers
{
    public class CheckoutController : Controller
    {
        

        private SamMusicStoreWebSite.Models.SamMusicStoreEntities db = new Models.SamMusicStoreEntities();
        const string PromoCode = "FREE";
        [Authorize]
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
               

                if (string.Equals(values["Promocode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save order
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch (Exception)
            {
                
                return View(order);
            }
            
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order 
            bool isValid = db.Orders.Any(m => m.OrderId == id && m.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }

        }
    }
}