using Khorshid.DataAccessLayer;
using Khorshid.Models;
using Khorshid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Khorshid.Extensions;
using System.Data.Common;
using System.Runtime.InteropServices.ComTypes;
using System.Data.Entity.Infrastructure;
using Khorshid.Views.Animations;
using Khorshid.Engines;
using Khorshid.Views.Pages;

namespace Khorshid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Configurations.Configuration.InitializeLocalFolder();
            Configurations.Configuration.LoadSettingsFromFile();

            if (App.CurrentApp.Configuration.IsFirstTime)
            {
                KhorshidDataManager.ResetToDefaultValues();

                App.CurrentApp.Configuration.IsFirstTime = false;
                App.CurrentApp.Configuration.SaveSettingsToFile();
            }

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            try
            {
                MainFrame.MarginFadeInAnimation(new Thickness(20,0,0,0), new Thickness(0), TimeSpan.FromMilliseconds(250));
            }
            catch { }
        }

        private void Home_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new MainPage());
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}