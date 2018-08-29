using Khorshid.Data;
using Khorshid.Models;
using Khorshid.ViewModels;
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
    /// Interaction logic for EditDriverPage.xaml
    /// </summary>
    public partial class EditDriverPage : Page
    {
        public EditDriverPage(int driverId)
        {
            InitializeComponent();
            DriverId = driverId;

            var driver = new KhorshidContext().Drivers.First(d => d.Id == driverId);
            DataContext = new EditDriverViewModel
            {
                DriverDescription = driver.Description,
                DriverName = driver.Name
            };
        }

        public int DriverId { get; set; }

        private void DeleteDriver_Button_Click(object sender, RoutedEventArgs e)
        {
            Delete_Popup.IsOpen = true;
        }

        private void DeleteConfirm_No_Click(object sender, RoutedEventArgs e)
        {
            Delete_Popup.IsOpen = false;
        }

        private void DeleteConfirm_Yes_Click(object sender, RoutedEventArgs e)
        {
            var context = new KhorshidContext();
            if (context.Drivers.FirstOrDefault(d => d.Id == DriverId) is Driver driver)
            {
                foreach (var workpage in context.WorkPages.Where(wp => wp.DriverId == driver.Id))
                {
                    var workItems = context.DriverWorks.Where(dw => dw.WorkPage.Id == workpage.Id);
                    context.DriverWorks.RemoveRange(workItems);
                    context.WorkPages.Remove(workpage);
                }

                context.Drivers.Remove(driver);
                context.SaveChanges();
            }

            Delete_Popup.IsOpen = false;

            App.Navigator.Navigate(new DriversManagement());

        }

        private void EditDriverButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new KhorshidContext();
            if (context.Drivers.FirstOrDefault(d => d.Id == DriverId) is Driver driver)
            {
                driver.Name = DriverName_Textbox.Text;
                driver.Description = Description_Textbox.Text;

                context.SaveChanges();
            }

            App.Navigator.Navigate(new DriverPage(DriverId));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new DriverPage(DriverId));

        }
    }
}
