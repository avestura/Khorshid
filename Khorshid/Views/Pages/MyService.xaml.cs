using Khorshid.Data;
using Khorshid.DataAccessLayer;
using Khorshid.Engines;
using Khorshid.Models;
using Khorshid.Views.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for MyService.xaml
    /// </summary>
    public partial class MyService : Page
    {

        string driver = "";
        public MyService(string _driver)
        {
            InitializeComponent();
            KhorshidContext.Services.Load();
            DataContext = Service = new ObservableCollection<Service>(KhorshidContext.Services.Where(c => c.DriveName == _driver).ToList()); ;
            driver = _driver;
           
        }

        private  KhorshidContext KhorshidContext = new KhorshidContext();
        private  ObservableCollection<Service> Service;

        private ObservableCollection<Service> Context { get; } = new ObservableCollection<Service>();

        private void AddItem_Button_Click(object sender, RoutedEventArgs e)
        {
            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CreateService service = new CreateService(driver);
            frm.Navigate(service);
           
           
        }

        private void DataGrid_Main_Loaded(object sender, RoutedEventArgs e)
        {
            SrvName.Text += " " + driver;
          
        }

        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem.ShowUsingLinearAnimation(milliSeconds: 250);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem.HideUsingLinearAnimation(milliSeconds: 250);

        }

        private void CreateDlt_Click(object sender, RoutedEventArgs e)
        {
            foreach (var res in DataGrid_Main.SelectedItems)
            {
                if (res is Service model)
                {
                    var context = new KhorshidContext();

                    var desiredItem = context.Services.First(item => item.Id == model.Id);

                    context.Services.Remove(desiredItem);

                    context.SaveChanges();
                }
            }
          
            App.Navigator.Navigate(new MyService(driver));

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KhorshidContext.Services.Load();
            DataContext = Service = new ObservableCollection<Service>(KhorshidContext.Services.Where(c => c.DriveName == driver).ToList()); ;

            var ctr = (TextBox)sender;
            if (ctr.Text == "")
                return;
            SearchEngine.ApplySearchOnCollection(ctr.Text,Service);
                
        }

        private void BTN_PayMent_Click(object sender, RoutedEventArgs e)
        {
            var res = (Service)DataGrid_Main.SelectedItem;

            if (KhorshidContext.Services.FirstOrDefault(d => d.Id == res.Id) is Service service)
            {
                service.PricePay =int.Parse( txt_main.Text.Replace(",",""));

                KhorshidContext.SaveChanges();
            }
            FinalPayment.HideUsingLinearAnimation(milliSeconds: 250);

            App.Navigator.Navigate(new MyService(driver));

        }

        private void BTN_CAN_Click(object sender, RoutedEventArgs e)
        {
            FinalPayment.HideUsingLinearAnimation(milliSeconds: 250);
            App.Navigator.Navigate(new MyService(driver));

        }

        private void EditItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItems.Count > 0)
            {
                var res = (Service)DataGrid_Main.SelectedItem;
                lbl_Serv_mnt.Text = "ماه سرویس:" + res.date;
                lbl_Serv_name.Text = "نام دانش آموز:" + res.Name;
                lbl_Money.Text = "مبلغ قرارداد:" + res.Price;

                FinalPayment.ShowUsingLinearAnimation(milliSeconds: 250);
            }
            else
            {
                MessageBox.Show("هیچ سرویسی انخاب نشده است");
            }
        }
    }
}
