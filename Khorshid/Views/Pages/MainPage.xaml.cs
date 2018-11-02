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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Areas_SectionCard_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new SearchTownPage());
        }

        private void Drivers_SectionCard_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new DriversManagement());
        }

        private void SectionCard_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new Customers());
        }

        private void SchoolService_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new MyService());
        }

       
    }
}
