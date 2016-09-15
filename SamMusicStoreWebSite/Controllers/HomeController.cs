using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SamMusicStoreWebSite.Models;

namespace SamMusicStoreWebSite.Controllers
{
    public class HomeController : Controller
    {

        public List<Album> GetTopSellingAlbums(int count)
        {
            using (var db = new SamMusicStoreEntities())
            {
                //Group the order details by album and return
                //the albums with the highest count
                return db.Albums
                    .OrderByDescending(m => m.OrderDetails.Count())
                    .Take(count)
                    .ToList();
            }
            
            
        }
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}