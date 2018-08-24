using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Khorshid.Configurations;

namespace Khorshid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Returnes current app as an <see cref="App"/> instead of an <see cref="Application"/>
        /// </summary>
        public static App CurrentApp => (App)Current;

        public static Frame Navigator => (CurrentApp.MainWindow as MainWindow)?.MainFrame;

        /// <summary>
        /// Returens current <see cref="Configuration"/> of application
        /// </summary>
        public Configuration Configuration { get; set; }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
