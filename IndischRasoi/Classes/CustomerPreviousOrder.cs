using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndischRasoi
{
    public class CustomerPreviousOrder
    {
        public int customerID { get; set; }
        public int foodID { get; set; }
        public string orderDate { get; set; }
        public string foodItem { get; set; }
      //  public string amountPaid { get; set; }
        public string amountDue { get; set; }
    }
}
