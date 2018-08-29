using Khorshid.Data;
using Khorshid.Models;
using Khorshid.ViewModels;
using Khorshid.Views.Animations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for DriverPage.xaml
    /// </summary>
    public partial class DriverPage : Page
    {
        public DriverPage(int driverId)
        {
            InitializeComponent();

            DriverId = driverId;

            DataContext = new DriverDetailsViewModel(DriverId, DriverPageDbContext);
        }

        private int CurrentModifyId { get; set; }

        private readonly KhorshidContext DriverPageDbContext = new KhorshidContext();

        public int DriverId
        {
            get { return (int)GetValue(DriverIdProperty); }
            set { SetValue(DriverIdProperty, value); }
        }

        public DriverDetailsViewModel GetVm() => DataContext as DriverDetailsViewModel;

        public static readonly DependencyProperty DriverIdProperty =
            DependencyProperty.Register("DriverId", typeof(int), typeof(DriverPage));

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            App.Navigator.Navigate(new EditDriverPage(DriverId));
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if(!int.TryParse(Price_Textbox.Text.Trim(), out int intPrice))
            {
                MessageBox.Show(
                    messageBoxText: "مقداری که برای قیمت وارد شده باید عدد باشد. لطفا روی دکمه OK کلیک کرده و سپس مقدار قیمت را تصحیح کنید.",
                    caption: "خطا",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Exclamation,
                    defaultResult: MessageBoxResult.OK,
                    options: MessageBoxOptions.RtlReading);

                return;
            }

            var lastWorkPage = DriverPageDbContext.WorkPages.ToList().Last(wp => wp.DriverId == DriverId);

            if (OkButton.Tag is bool isCreateMode)
            {
                if (isCreateMode)
                {
                    DriverPageDbContext.DriverWorks.Add(new DriverWork
                    {
                        WorkPageId = lastWorkPage.Id,
                        Date = DatePicker.SelectedDate.Value,
                        FromLocation = FromLocation_ComboBox.Text,
                        ToLocation = ToLocation_ComboBox.Text,
                        Price = intPrice
                    });

                    DriverPageDbContext.SaveChanges();

                    GetVm().UpdateVm();

                }
                else
                {
                    var work = DriverPageDbContext.DriverWorks.First(w => w.Id == CurrentModifyId);

                    work.Date = DatePicker.SelectedDate.Value;
                    work.FromLocation = FromLocation_ComboBox.Text;
                    work.ToLocation = ToLocation_ComboBox.Text;
                    work.Price = intPrice;

                    DriverPageDbContext.SaveChanges();

                    GetVm().UpdateVm();

                }
            }
            else
            {
                return;
            }

            UI_Overlay.HideUsingLinearAnimation(milliSeconds: 250);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UI_Overlay.HideUsingLinearAnimation(milliSeconds: 250);
        }

        private void AddItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!EligibleToModifyCollection()) return;

            var context = new KhorshidContext();
            PopupTitle.Text = "افزودن کارکرد جدید";

            DatePicker.SelectedDate = DateTime.Now;
            FromLocation_ComboBox.Text = "";
            ToLocation_ComboBox.Text = "";
            Price_Textbox.Text = "";

            OkButton.Content = "ساخت جدید";
            OkButton.Tag = true;

            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void EditItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!EligibleToModifyCollection()) return;

            if (DataGrid_Main.SelectedItem is DriverWork model)
            {
                CurrentModifyId = model.Id;
                PopupTitle.Text = "ویرایش کارکرد";

                DatePicker.SelectedDate = model.Date;
                FromLocation_ComboBox.Text = model.FromLocation;
                ToLocation_ComboBox.Text = model.ToLocation;
                Price_Textbox.Text = model.Price.ToString();

                OkButton.Content = "ویرایش";
                OkButton.Tag = false;

                UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
            }
        }

        private void DeleteConfirm_No_Click(object sender, RoutedEventArgs e)
        {
            Delete_Popup.IsOpen = false;
        }

        /// <summary>
        /// Returns true if eligible for add, edit or remove
        /// </summary>
        /// <returns></returns>
        private bool EligibleToModifyCollection()
        {
            if (GetVm().CurrentWorkPage.IsClosed)
            {
                MessageBox.Show(
                  messageBoxText: "این صفحه تسویه حساب شده است. لطفا به آخرین صفحه حساب بروید.",
                  caption: "خطا",
                  button: MessageBoxButton.OK,
                  icon: MessageBoxImage.Exclamation,
                  defaultResult: MessageBoxResult.OK,
                  options: MessageBoxOptions.RtlReading);

                return false;
            }
            return true;
        }

        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is DriverWork model)
            {
                if (!EligibleToModifyCollection()) return;

                Delete_Popup.IsOpen = true;
            }
        }

        private void DeleteConfirm_Yes_Click(object sender, RoutedEventArgs e)
        {
            if (!EligibleToModifyCollection()) return;

            if (DataGrid_Main.SelectedItem is DriverWork model)
            {
                var context = new KhorshidContext();

                var desiredItem = context.DriverWorks.First(item => item.Id == model.Id);

                context.DriverWorks.Remove(desiredItem);

                context.SaveChanges();

                GetVm().UpdateVm();
            }

            Delete_Popup.IsOpen = false;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ToLocation_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ToLocation_ComboBox.SelectedItem is TownData t)
            {
                Price_Textbox.Text = t.Price;
            }
        }

        private void PreviousWorkPage_Button_Click(object sender, RoutedEventArgs e)
        {
            GetVm().CurrentPageNo--;
            GetVm().UpdateVm();
        }

        private void NextWorkPage_Button_Click(object sender, RoutedEventArgs e)
        {
            GetVm().CurrentPageNo++;
            GetVm().UpdateVm();
        }

        private void GoToLastPage_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            GetVm().RequestLastPage();
            GetVm().UpdateVm();
        }

        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CommisionPrecentage_Textbox.Text, out int cp) && cp >= 0 && cp <= 100)
                return;

            var wp = GetVm().CurrentWorkPage;
            wp.IsClosed = true;
            wp.DateClosed = DateTime.Now;
            wp.CommissionPercentage = (byte)cp;
            DriverPageDbContext.WorkPages.Add(new WorkPage
            {
                DriverId = DriverId,
            });

            DriverPageDbContext.SaveChanges();

            GetVm().UpdateVm();

            ClosePagePopup.IsOpen = false;

        }

        private void ClosePage_Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            ClosePagePopup.IsOpen = false;
        }

        private void ClosePagePopupButton_Click(object sender, RoutedEventArgs e)
        {
            if(GetVm().DriverWorks.Any())
            {
                ClosePagePopup.IsOpen = true;
            }
        }
    }
}
