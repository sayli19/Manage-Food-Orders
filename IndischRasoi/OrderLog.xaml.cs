using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndischRasoi
{
    public partial class OrderLog : Window
    {
        public OrderLog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Orderlogs.ItemsSource = App.orderLogs;
            
            Cbx_default.SelectedIndex = 0;
            Tbx_Count_all.Text = Lbx_Orderlogs.Items.Count.ToString();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ShowParent();
        }

        private void ShowParent()
        {
            Owner.Visibility = Visibility.Visible;
        }

        //Side menu animation
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

        private void Btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Close();
            ShowParent();
        }


        //filter to search orders
        private void Tbx_FilterOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.orderLogs == null) return;
            var filter = (sender as TextBox).Text;
            if(filter != null)
            {
                var result = from o in App.orderLogs where (o.foodItem.ToLower().Contains(filter) || o.orderDate.ToLower().Contains(filter) || o.mealType.ToLower().Contains(filter) || o.status.ToLower().Contains(filter) || o.foodItem.ToUpper().Contains(filter) || o.orderDate.ToUpper().Contains(filter) || o.mealType.ToUpper().Contains(filter) || o.status.ToUpper().Contains(filter)) select o;
                Lbx_Orderlogs.ItemsSource = result;
                Tbx_Count_all.Text = Lbx_Orderlogs.Items.Count.ToString();
            }
          
        }


        //Combo box selection
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.orderLogs == null) return;
            var filter = (sender as ComboBox).SelectedItem.ToString();
            if(filter != null)
            {
                if (filter == "All")
                {

                    Lbx_Orderlogs.ItemsSource = App.orderLogs;
                    Txt_Delivered.Text = "36";
                    Txt_Cancel.Text = "07";
                    Txt_Pop1.Text = "Chicken Biryani";
                    Txt_Pop2.Text = "Vegetable Pulav";
                    Txt_Pop3.Text = "Paneer Fried Rice";

                    Txt_Total.Text = "Total Sales Analysis";
                    Tbx_Count_all.Text = Lbx_Orderlogs.Items.Count.ToString();
                }
                if (filter == "Today")
                {

                    Txt_Delivered.Text = "06";
                    Txt_Cancel.Text = "02";
                    Txt_Pop1.Text = "Rassam Rice";
                    Txt_Pop2.Text = "Dal Tadka";
                    Txt_Pop3.Text = "Egg Biryani";

                    Txt_Total.Text = "Today's Sales Analysis";
                    var result = from o in App.orderLogs where (o.foodItem.ToLower().Contains(filter) || o.orderDate.Contains("11.02.2021") || o.mealType.ToLower().Contains(filter) || o.status.ToLower().Contains(filter)) select o;
                    Lbx_Orderlogs.ItemsSource = result;
                    Tbx_Count_all.Text = Lbx_Orderlogs.Items.Count.ToString();
                }
            }
           
        }
    }
}
