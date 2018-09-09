using System.Collections.Generic;

namespace DecisionTreeLab.Tests.Models
{
    public class Order
    {
        public string DisplayName { get; set; }

        public List<OrderItem> Items { get; set; }

        public void Void()
        {
        }
    }
}
