using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SamMusicStoreWebSite.Models;

namespace SamMusicStoreWebSite.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            using (var db = new SamMusicStoreWebSite.Models.SamMusicStoreEntities())
            {
                var genre = db.Genres.ToList();
                return View(genre);
            }
            //return View();
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            using (var db = new SamMusicStoreEntities())
            {
                var genres = db.Genres.ToList();
                return PartialView(genres);
            }
        }

        public ActionResult Browse(string genre)
        {
            using (var db = new SamMusicStoreEntities())
            {
                // Retrieve Genre and its Associated Albums from database 
                var genreModel = db.Genres.Include("Album").Single(g => g.Name == genre);
                return View(genreModel);
            }
 
        }

        public ActionResult Details(int id)
        {
            using (var db = new SamMusicStoreEntities())
            {
                var album = db.Albums.Find(id);
                return View(album);
            }
        }
    }
}