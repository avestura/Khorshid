using Khorshid.Data;
using Khorshid.Models;
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
    /// Interaction logic for AddDriverPage.xaml
    /// </summary>
    public partial class AddDriverPage : Page
    {
        public AddDriverPage()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new DriversManagement());
        }

        private void CreateDriver_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new KhorshidContext())
            {
                var driver = new Driver
                {
                    Name = (DriverName_Textbox.Text.Trim().Length == 0) ? "(بدون عنوان)" : DriverName_Textbox.Text,
                    Description = Description_Textbox.Text,

                };
                var workPage = new WorkPage
                {
                    Driver = driver
                };

                context.Drivers.Add(driver);
                context.WorkPages.Add(workPage);

                context.SaveChanges();
            }
            App.Navigator.Navigate(new DriversManagement());
        }
    }
}
