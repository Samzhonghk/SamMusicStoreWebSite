using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Configuration;

namespace SamMusicStoreWebSite.Models
{
    public class SamMusicStoreEntities: DbContext
    {
        
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<SamMusicStoreWebSite.Models.Artist> Artists { get; set; }
    }
}