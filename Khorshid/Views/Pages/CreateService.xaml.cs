using Khorshid.Data;
using Khorshid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateService.xaml
    /// </summary>
    public partial class CreateService : Page
    {
        public CreateService()
        {
            InitializeComponent();
            KhorshidContext.Drivers.Load();
            Drivers = KhorshidContext.Drivers.Local;

        }

        private readonly KhorshidContext KhorshidContext = new KhorshidContext();
        public ObservableCollection<Driver> Drivers
        {
            get;
        }
        public ObservableCollection<Service> Services
        {
            get;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PersianCalendar p = new PersianCalendar();

            var x = Drivers;
            foreach (var item in x)
                CMB_DRIVER.Items.Add(item.Name);

            CMB_DRIVER.SelectedIndex = 0;
            var m = "فروردین اردیبهشت خرداد تیر مرداد شهریور مهر آبان آذر دی بهمن اسفند".Split(' ');
            int counter = 0;
            for (int i = 1397; i < 1440; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    CMB_START.Items.Add(i.ToString() + " " + m[j]);
                    if (p.GetYear(DateTime.Today) == i && p.GetMonth(DateTime.Today) == j + 1)
                        CMB_START.SelectedIndex = counter;
                    counter++;
                }


            }

            for (int i = 0; i < 12; i++)
                CMB_END.Items.Add((i + 1) + " ماهه");

            CMB_END.SelectedIndex = 5;



        }


        private void CANCEL_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new MyService());
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_PRICE.Text))
                TXT_PRICE.Text = "0";
            using (var context = new KhorshidContext())
            {
                var res = GetMArray(CMB_START.Text, CMB_END.SelectedIndex);
                foreach (var item in res)
                {
                    var service = new Service
                    {
                        DriveName = CMB_DRIVER.Text,
                        Name = TXT_STU_NAME.Text,
                        Phone = TXT_STU_PH.Text,
                        Adreess = TXT_STU_ADD.Text,
                        SchoolName = TXT_SC_NAME.Text,
                        SchoolPhone = TXT_SC_PH.Text,
                        SchoolAdreess = TXT_SC_ADR.Text,
                        date = item,
                        Price = int.Parse(TXT_PRICE.Text.Replace(",", "")),
                        PricePay = 0,
                    };
                    

                    context.Services.Add(service);
                    context.SaveChanges();

                }


            }
        }
        private string[] GetMArray(string date,int forward)
        {

            var tmp = date.Split(' ');
            var m = "فروردین اردیبهشت خرداد تیر مرداد شهریور مهر آبان آذر دی بهمن اسفند".Split(' ');
            var index = Array.IndexOf(m, tmp[1]);
            int year = int.Parse(tmp[0]);
            List<string> res = new List<string>();
            for (int i = 0; i < forward+1; i++)
            {

                if(i + index >= m.Length)
                {
                   
                    forward -= i;
                    index = 0;
                    i = 0;
                    year += 1;
                }

                res.Add(year + " " + m[i + index]);
            }

            return res.ToArray();
        }
      
    }
    class TextBoxEx : TextBox
    {




        void TextBoxEx_OnTextChanged(object sender, EventArgs e)
        {
            ConvertText();
            var ex = (TextBoxEx)sender;
            ex.CaretIndex = ex.Text.Length;
        }
        public TextBoxEx()
        {
            GotFocus += TextBoxEx_GotFocus;
            LostFocus += TextBoxEx_LostFocus;
            PreviewTextInput += TextBoxEx_PreviewTextInput;
            TextChanged += new TextChangedEventHandler(TextBoxEx_OnTextChanged);

        }

        void TextBoxEx_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            decimal d;
            if (!decimal.TryParse(e.Text, out d))
            {
                e.Handled = true;
            }
        }

        void TextBoxEx_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            ConvertText();
        }

        void TextBoxEx_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            ConvertText();
        }

        private void ConvertText()
        {

            string str = Text;
            if (str == "")
                return;
            string format = str.Replace(",", "");
            double dbl = Convert.ToDouble(format);
            str = string.Format("{0:n0}", dbl);
            Text = str;
        }
    }
}