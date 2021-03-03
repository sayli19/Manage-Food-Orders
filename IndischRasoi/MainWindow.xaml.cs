using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndischRasoi
{

    public partial class MainWindow : Window
    {
        ObservableCollection<AddOrder> OrderList = new ObservableCollection<AddOrder>();
        List<FoodList> SeletedFoodlist = new List<FoodList>();
        public int sum = 0;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Customers.ItemsSource = App.customers;
            Lbx_FoodList.ItemsSource = App.foodlist;
   
            //Initialise the count of available list
            Lbx_FoodList.Items.Count.ToString();
            Txt_CustomerCount.Text = Lbx_Customers.Items.Count.ToString();

            DataContext = new FoodList();

            App.addOrder.Clear();           
        }

        // Side menu animation
        DoubleAnimation da = new DoubleAnimation(37, TimeSpan.FromSeconds(1.5));
        private void Grd_Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            da.To = 117;
            Grd_Menu.BeginAnimation(Grid.WidthProperty, da);
        }

        private void Grd_Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            da.To = 28;
            Grd_Menu.BeginAnimation(Grid.WidthProperty, da);
        }


        // Menu button click - Order log
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrderLog();
            win.Width = Width;
            win.Height = Height;
            win.Title = "Order Logs";
            win.Owner = this;
            win.Show();
            Visibility = Visibility.Collapsed;
        }


        // Customer Filter
        private void Tbx_Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.customers == null) return;
            var filter = (sender as TextBox).Text;
            var result = from s in App.customers where (s.firstName.ToLower().Contains(filter) || s.lastName.ToLower().Contains(filter) || s.firstName.ToUpper().Contains(filter) || s.lastName.ToUpper().Contains(filter)) select s;
            if(result != null)
            {
                Lbx_Customers.ItemsSource = result;
                Txt_CustomerCount.Text = Lbx_Customers.Items.Count.ToString();
            }
        
        }

        //Add new Customer
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var cus = new Customer { firstName = "Please edit ", lastName = "Please edit" };
            App.customers.Add(cus);
            Lbx_Customers.ScrollIntoView(cus);
            Lbx_Customers.SelectedItem = cus;
        }

        //Delete Customer
        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            var cus = Lbx_Customers.SelectedItem as Customer;
            if (cus == null)
            {
               
                MessageBox.Show("Please select student first to be deleted", "Hint");
                return;
            }
            App.customers.Remove(cus);
        }

        // customer Item click
        private void Lbx_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var custID = ((sender as ListBox).SelectedItem as Customer).customerID;
            var previousOrders = from p in App.custPreviousOrders where p.customerID == custID select p;
            Lbx_PreviousOrder.ItemsSource = previousOrders;
        }


        // Food filter
        private void Tbx_FilterFood_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.foodlist == null) return;
            var filter = (sender as TextBox).Text;
            var result = from f in App.foodlist where (f.foodName.ToLower().Contains(filter) || f.foodName.ToUpper().Contains(filter)) select f;
            Lbx_FoodList.ItemsSource = result;
            Tbx_FoodCount.Text = Lbx_FoodList.Items.Count.ToString();

        }



        // Add from foodlist to order list
        private void Lbx_FoodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lbx_FoodList.SelectedItem != null)
            {
                var fqty = (FoodList)Lbx_FoodList.SelectedItem;

                if (fqty.foodTotalQty > 0)
                {
                    Lbx_NewOrder.Items.Add(Lbx_FoodList.SelectedItem);                  
                    AddOrder addeditem = new AddOrder();
                    CustomerPreviousOrder prevOrd = new CustomerPreviousOrder();
                    var selecteditem = (sender as ListBox).SelectedItem as FoodList;            
                    var newfoodlist = from p in App.foodlist where p.foodID == selecteditem.foodID select p;

                    foreach (var item in newfoodlist)
                    {
                        if (item != null)
                        {
                            addeditem = new AddOrder()
                            {
                                customerID = 1,
                                foodId = item.foodID,
                                foodName = item.foodName,
                                qty = item.foodTotalQty,
                                foodPrice = item.foodPrice,

                            };
                           
                              App.foodlist.FirstOrDefault(c => c.foodID == selecteditem.foodID).foodTotalQty = selecteditem.foodTotalQty - 1;
                              var qty = selecteditem.foodTotalQty;                           
                              Lbx_FoodList.ItemsSource = null;
                              Lbx_FoodList.ItemsSource = App.foodlist;
                              App.addOrder.Add(addeditem);                           
                        }
                    }
                    AddTotalPrice(App.addOrder);
                }
                else
                {
                    MessageBox.Show("Sorry, This food item is currently not available", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
        }

        //Sum of added items
        private void AddTotalPrice(ObservableCollection<AddOrder> addOrder)
        {
            int sum = addOrder.Select(x => x.foodPrice).Sum();          
            Tbx_Sum.Text = sum.ToString();
        }

        //Clear button - to clear all the items
        private void Btn_ClearList_Click(object sender, RoutedEventArgs e)
        {
            var deleteMsg = MessageBox.Show("Are you sure you want to clear the order ? \n please click \n\n 'Yes' to delete order; \n 'No' to Cancel \n ",
                                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            switch (deleteMsg)
            {
                case MessageBoxResult.Yes:
                    foreach (var item in App.foodlist)
                    {
                        item.foodTotalQty = 5;
                    }
                    Lbx_FoodList.ItemsSource = null;
                    Lbx_FoodList.ItemsSource = App.foodlist;
                    App.addOrder.Clear();
                    Lbx_NewOrder.Items.Clear();
                    Tbx_Sum.Text = "0";
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
                    
        }


        //Delete button - to delete one order
        private void Btn_ClearItem_Click(object sender, RoutedEventArgs e)
        {
            if (Lbx_NewOrder.SelectedItem != null)
            {
                var selectedItem = (FoodList)Lbx_NewOrder.SelectedItem;

                if(selectedItem != null)
                {
                    App.foodlist.FirstOrDefault(c => c.foodID == selectedItem.foodID).foodTotalQty = selectedItem.foodTotalQty + 1;
                    var qty = selectedItem.foodTotalQty;
                    Lbx_FoodList.ItemsSource = null;
                    Lbx_FoodList.ItemsSource = App.foodlist;

                    //delete one item
                    int i = 0;
                    i = Lbx_NewOrder.SelectedIndex;
                    if (i >= 0)
                    {
                        Lbx_NewOrder.Items.RemoveAt(i);
                        App.addOrder.RemoveAt(i);
                    }

                    AddTotalPrice(App.addOrder);
                }
               
            }

        }

        //order tab -  fetch quantity from text box
        private void Txt_FoodQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            var filter = int.Parse((sender as TextBox).Text);           
            var selectedItem = (FoodList)Lbx_NewOrder.SelectedItem;
            if(selectedItem != null)
            {
                
           
            if (filter > selectedItem.foodTotalQty)
            {
                 MessageBox.Show("Cannot add " + filter + " quantity. Please enter " + selectedItem.foodTotalQty + " or less than " + selectedItem.foodTotalQty+ " quantity");
            }
            else
            {
                int finalQty = 0;
                foreach (var item in App.addOrder)
                {
                    var id = item.foodId;
                    if (selectedItem.foodID == id)
                    {

                        count = count++;

                        int totalQty = selectedItem.foodTotalQty;

                        finalQty = ((totalQty - filter) - count) + 1;
                    }
                };
                int sum = filter * selectedItem.foodPrice;
                App.addOrder.FirstOrDefault(c => c.foodId == selectedItem.foodID).foodPrice = sum;
                App.foodlist.FirstOrDefault(c => c.foodID == selectedItem.foodID).foodTotalQty = finalQty;
                AddTotalPrice(App.addOrder);
            }
            }
        }

        //Place order
        private void Btn_PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order has been placed");
            App.addOrder.Clear();
        }
    }
}
