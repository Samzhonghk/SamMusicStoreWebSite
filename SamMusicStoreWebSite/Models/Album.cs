using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SamMusicStoreWebSite.Models
{
    [Bind(Exclude="AlbumId")]
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }
       
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        [DisplayName("Artist")]
        public int ArtistId { get; set; }
        [Required(ErrorMessage="An Album Title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        [Required(ErrorMessage="Price is required")]
        [Range(0.001, 10000, ErrorMessage="Price must be between 0.001 and 10000")]
        public decimal Price { get; set; }
        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }
        public virtual  Genre Genre { get; set; }
        public virtual  Artist Artist { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}