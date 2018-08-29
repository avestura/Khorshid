using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Khorshid.Configurations;
using Khorshid.Data;

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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            using (var context = new KhorshidContext())
            {
                context.Database.Initialize(false);
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fa-IR");
        }
    }
}
