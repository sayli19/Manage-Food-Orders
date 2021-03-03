using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IndischRasoi
{
    public partial class App : Application
    {
        public static ObservableCollection<Customer> customers;
        public static List<FoodList> foodlist;
        public static ObservableCollection<AddOrder> addOrder;
        public static List<OrderLogList> orderLogs;
        public static ObservableCollection<CustomerPreviousOrder> custPreviousOrders;
        public static ObservableCollection<PlacedOrders> placedOrders;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Customer 
            customers = MyStorage.ReadXml<ObservableCollection<Customer>>("Customers.xml");
            if (customers == null)
            {
               // customers = new ObservableCollection<Customer>();

                var toDo = MessageBox.Show("File not found \n\n Generate new data? \n please click \n\n 'Yes' to generate test data; \n 'No' to start from scratch \n 'Cancel' to exit the application", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                switch (toDo)
                {
                    case MessageBoxResult.Yes:
                    // var customer = new ObservableCollection<Customer> { new Customer { customerID = 22, firstName = "Kim", lastName = "Soekjin", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5"}, new Customer { customerID = 23, firstName = "Kim", lastName = "Taehyung", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5" }, new Customer { customerID = 24, firstName = "Kim", lastName = "Taehyung", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5" } };

                        break;
                    case MessageBoxResult.No:
                        customers = new ObservableCollection<Customer>();
                        break;
                    default:
                        App.Current.Shutdown();
                        break;
                }
            }

            //Food List
            foodlist = MyStorage.ReadXml<List<FoodList>>("FoodList.xml");
            if (foodlist == null)
            {
                foodlist = new List<FoodList>();
            }

            addOrder = MyStorage.ReadXml<ObservableCollection<AddOrder>>("Order.xml");
            if (addOrder == null)
            {
                addOrder = new ObservableCollection<AddOrder>();
            }

            //Order Logs
            orderLogs = MyStorage.ReadXml<List<OrderLogList>>("OrderLogs.xml");
            if (orderLogs == null)
            {
                orderLogs = new List<OrderLogList>();
            }

            //Previous orders
            custPreviousOrders = MyStorage.ReadXml<ObservableCollection<CustomerPreviousOrder>>("CustomerPreviousOrder.xml");
            if (custPreviousOrders == null)
            {
                custPreviousOrders = new ObservableCollection<CustomerPreviousOrder>();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

            //var custPrev = new List<CustomerPreviousOrder> { new CustomerPreviousOrder { customerID = 1, foodID = 1, orderDate="01.02.2020", foodItem = "Vegetable Pulav", amountDue = "5 €"}, 
            //new CustomerPreviousOrder {customerID = 1, foodID = 1, orderDate="01.02.2020", foodItem = "Vegetable Pulav", amountDue = "5 €" }, 
            //new CustomerPreviousOrder { customerID = 1, foodID = 1, orderDate="01.02.2020", foodItem = "Vegetable Pulav", amountDue = "5 €" } };
            // var newCustomer = new ObservableCollection<Customer> { new Customer { customerID = 22, firstName = "Kim", lastName = "Soekjin", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5"}, new Customer { customerID = 23, firstName = "Kim", lastName = "Taehyung", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5" }, new Customer { customerID = 24, firstName = "Kim", lastName = "Taehyung", streetName = "Elizabeth str 9", houseNumber = 5, pincode = 69123, city = "Heidelberg", phone = "+49 17543352511", amountDue = "5" } };
          // var newOrder = new List<OrderLogList> { new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1, price = "0", amountPaid = "0", status = "Cancelled" }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1, price = "0", amountPaid = "0", status = "Cancelled" }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1, price = "0", amountPaid = "0", status = "Cancelled" }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 }, new OrderLogList { orderId = 22, foodId = 1, orderDate = "22.01.2021", foodItem = "Vegetable Pulav", mealType = "Lunch", quantity = 1 } };
            MyStorage.WriteXml(customers, "Customers.xml");
         // MyStorage.WriteXml(newOrder, "OrderLogs.xml");
          // MyStorage.WriteXml(foodlist, "FoodList.xml");
        }
    }

}
