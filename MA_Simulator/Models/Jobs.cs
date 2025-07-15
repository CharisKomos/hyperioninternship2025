using System;

namespace MA_Simulator.Models
{
    public class Jobs : EntityId
    {
        public string Customer { get; set; }
        public Heat Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DueDate { get; set; }

        public Jobs() { }

        public Jobs(int id, string customer, Heat product, int quantity, DateTime dueDate)
        {
            Id = id;
            Customer = customer;
            Product = product;
            Quantity = quantity;
            DueDate = dueDate;
        }
    }
}