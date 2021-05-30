using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    /// <summary>
    /// Model class for orders.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id for the order.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Id of user who is getting the order.
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Date of when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }
        
        /// <summary>
        /// List of item ids and amounts corresponding.
        /// </summary>
        public IList<BasketItem> Items { get; set; }
        
        /// <summary>
        /// Address where the order gets invoiced to (Street, Number, etc.).
        /// </summary>
        [Required]
        public string InvoiceAddress { get; set; }
        
        /// <summary>
        /// Address where the order gets delivered to (Street, Number, etc.).
        /// </summary>
        [Required]
        public string DeliveryAddress { get; set; }
        
        /// <summary>
        /// Name of the city to deliver.
        /// </summary>
        [Required]
        public string City { get; set; }
        
        /// <summary>
        /// Zip code of the city.
        /// </summary>
        [Required]
        public int PostalCode { get; set; }
        
        /// <summary>
        /// Total price of all the items times amounts in the order.
        /// </summary>
        [Required]
        public double TotalPrice { get; set; }
        
        /// <summary>
        /// Not used.
        /// </summary>
        [Required]
        public double ItemPrice { get; set; }
        
        /// <summary>
        /// Price of delivery that adds to the total price.
        /// </summary>
        [Required]
        public double DeliveryPrice { get; set; }
        
        /// <summary>
        /// Not used.
        /// </summary>
        [Required]
        public string Currency { get; set; }
        
        /// <summary>
        /// Boolean value to see if an order is delivered yet.
        /// </summary>
        [JsonPropertyName("delivered")]
        public bool IsDelivered { get; set; }
        
        /// <summary>
        /// Status of the order after placing the order.
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public string Status { get; set; }
    }
}