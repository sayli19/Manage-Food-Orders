using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndischRasoi
{
    public class OrderLogList
    {
        public int orderId { get; set; }
        public int foodId { get; set; }
        public string orderDate { get; set; }
        public string foodItem { get; set; }
        public string mealType { get; set; }
        public int quantity { get; set; }
        public string price { get; set; }
        public string amountPaid { get; set; }
        public string status { get; set; }
    }
}
