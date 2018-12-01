using Khorshid.Data;
using Khorshid.Models;
using Khorshid.Views.Controls;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for DriverForService.xaml
    /// </summary>
    public partial class DriverForService : Page
    {
        public DriverForService()
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

        private void SectionCard_Click(object sender, RoutedEventArgs e)
        {
            var driverId = (sender as SectionCard)?.Title;
            App.Navigator.Navigate(new MyService(driverId));
        }
    }
}
