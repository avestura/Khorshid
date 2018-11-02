using Khorshid.Views.Animations;
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
    /// Interaction logic for MyService.xaml
    /// </summary>
    public partial class MyService : Page
    {
        public MyService()
        {
            InitializeComponent();
        }

        private void AddItem_Button_Click(object sender, RoutedEventArgs e)
        {
            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           frm.Source = new Uri("CreateService.xaml", UriKind.Relative);

        }
    }
}
