using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace SamMusicStoreWebSite.Models
{
    [Bind(Exclude="OrderId")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        [Required(ErrorMessage="First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage="Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State	{ get; set; }
        [Required(ErrorMessage = "Postal Code is required")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country	{ get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone	{ get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Email is is not valid.")] 
        public string Email	{ get; set; }
        [ScaffoldColumn(false)]
        public decimal Total	{ get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}