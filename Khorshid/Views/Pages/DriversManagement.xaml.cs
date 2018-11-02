using Khorshid.Data;
using Khorshid.Models;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for DriversManagement.xaml
    /// </summary>
    public partial class DriversManagement : Page
    {

        public DriversManagement()
        {
            InitializeComponent();

            KhorshidContext.Drivers.Load();
            Drivers = KhorshidContext.Drivers.Local;
        }

        private readonly KhorshidContext KhorshidContext = new KhorshidContext();
        public ObservableCollection<Driver> Drivers { get; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DriverItems.ItemsSource = Drivers;
        }

        private void AddDriver_Button_Click(object sender, RoutedEventArgs e)
        {

            App.Navigator.Navigate(new AddDriverPage());
        }

        private void SectionCard_Click(object sender, RoutedEventArgs e)
        {
            var driverId = (int)(sender as Control)?.Tag;
            App.Navigator.Navigate(new DriverPage(driverId));
        }
    }
   
}
