using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndischRasoi
{
    public class PlacedOrders
    {
        public int customerID { get; set; }
        public int foodId { get; set; }
        public string foodName { get; set; }

        public string amountPaid { get; set; }
        public int qty { get; set; }
        public int foodPrice { get; set; }
    }
}
