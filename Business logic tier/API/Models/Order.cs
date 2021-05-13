using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Order
    {
        public int order_id { get; set; }
        public DateTime order_date { get; set; }
        public IList<Recipe> Recipes { get; set; }
        [Required]
        public string InvoiceAddress { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public double ItemPrice { get; set; }
        [Required]
        public double DeliveryPrice { get; set; }
        [Required]
        public string Currency { get; set; }

        public bool? IsDelivered { get; set; }
    }
}