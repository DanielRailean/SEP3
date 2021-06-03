using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApp.Models;

namespace API.Models
{
    /// <summary>
    /// Model for orders in the data access tier.
    /// </summary>
    public class DataOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public string RecipesIdList { get; set; }
        public string RecipesQuantityList { get; set; }
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

        [JsonPropertyName("delivered")]
        public bool IsDelivered { get; set; }
        [JsonPropertyName("orderStatus")]
        public string Status { get; set; }
    }
}