using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SamMusicStoreWebSite.Models
{
    public class Artist
    {
        
        public int ArtistId { get; set; } 
        public string Name { get; set; }
    }
}