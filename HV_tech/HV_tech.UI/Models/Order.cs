using System.Collections.Generic;

namespace HV_tech.UI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order(string message)
        {
            Message = message;
            Items = new List<OrderItem>();
        }
    }
}