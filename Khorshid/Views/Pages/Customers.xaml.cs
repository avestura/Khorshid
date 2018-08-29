using Khorshid.Data;
using Khorshid.DataAccessLayer;
using Khorshid.Engines;
using Khorshid.Models;
using Khorshid.Views.Animations;
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

namespace Khorshid.Views.Pages
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        private int CurrentModifyId { get; set; }

        private ObservableCollection<Customer> CustomerItems { get; } = new ObservableCollection<Customer>();

        public string TextBoxPreviousText { get; set; } = "";

        public Customers()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.DataContext = CustomerItems;

            var context = new KhorshidContext();
            CustomerItems.Clear();
            context.Customers.ToList().ForEach(c => CustomerItems.Add(c));

            SearchBox.Focus();
        }

        private void Page_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (UI_Overlay.Visibility != Visibility.Visible)
            {
                SearchBox.Focus();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxPreviousText != SearchBox.Text.Trim())
            {
                TextBoxPreviousText = SearchBox.Text;

                SearchTextBox_ApplyModification();

            }

        }

        private void SearchTextBox_ApplyModification()
        {
            if (SearchBox.Text.Trim()?.Length == 0)
            {
                var context = new KhorshidContext();
                CustomerItems.Clear();
                context.Customers.ToList().ForEach(c => CustomerItems.Add(c));
            }
            else
            {
                string term = SearchBox.Text.Replace("آ", "ا");

                SearchEngine.ApplySearchOnCollection(term, CustomerItems);

            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if (Name_Textbox.Text.Trim()?.Length == 0
               && SubscriptionId_Textbox.Text.Trim()?.Length == 0)
            {
                return;
            }

            if (!int.TryParse(SubscriptionId_Textbox.Text, out int intSubscriptionId))
            {
                MessageBox.Show(
                  messageBoxText: "مقداری که برای شماره اشتراک وارد شده باید عدد باشد. لطفا روی دکمه OK کلیک کرده و سپس مقدار شماره اشتراک را تصحیح کنید.",
                  caption: "خطا",
                  button: MessageBoxButton.OK,
                  icon: MessageBoxImage.Exclamation,
                  defaultResult: MessageBoxResult.OK,
                  options: MessageBoxOptions.RtlReading);
                return;
            }

            var context = new KhorshidContext();

            if (OkButton.Tag is bool isCreateMode)
            {
                if (isCreateMode)
                {
                    if (context.Customers.Any(c => c.SubscriptionId == intSubscriptionId))
                    {
                        MessageBox.Show(
                            messageBoxText: "این شماره اشتراک قبلا استفاده شده است. لطفا از شماره اشتراک دیگری استفاده کنید.",
                            caption: "خطا",
                            button: MessageBoxButton.OK,
                            icon: MessageBoxImage.Exclamation,
                            defaultResult: MessageBoxResult.OK,
                            options: MessageBoxOptions.RtlReading);
                        return;
                    }

                    context.Customers.Add(new Customer
                    {
                        Name = Name_Textbox.Text,
                        Address = Address_Textbox.Text,
                        MobileNumber = Mobile_Textbox.Text,
                        PhoneNumber = PhoneNumber_Textbox.Text,
                        SubscriptionId = intSubscriptionId
                    });

                    context.SaveChanges();
                    SearchTextBox_ApplyModification();
                }
                else
                {
                    var customer = context.Customers.First(item => item.Id == CurrentModifyId);

                    if (intSubscriptionId != customer.SubscriptionId
                        && context.Customers.Any(c => c.SubscriptionId == intSubscriptionId))
                    {
                        MessageBox.Show(
                            messageBoxText: "این شماره اشتراک قبلا استفاده شده است. لطفا از شماره اشتراک دیگری استفاده کنید.",
                            caption: "خطا",
                            button: MessageBoxButton.OK,
                            icon: MessageBoxImage.Exclamation,
                            defaultResult: MessageBoxResult.OK,
                            options: MessageBoxOptions.RtlReading);
                        return;
                    }

                    customer.Name = Name_Textbox.Text;
                    customer.Address = Address_Textbox.Text;
                    customer.MobileNumber = Mobile_Textbox.Text;
                    customer.PhoneNumber = PhoneNumber_Textbox.Text;
                    customer.SubscriptionId = intSubscriptionId;

                    context.SaveChanges();

                    SearchTextBox_ApplyModification();
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
            var context = new KhorshidContext();

            Name_Textbox.Text =
            PhoneNumber_Textbox.Text =
            Mobile_Textbox.Text =
            SubscriptionId_Textbox.Text =
            Address_Textbox.Text = "";

            OkButton.Content = "ساخت جدید";
            OkButton.Tag = true;
            PopupTitle.Text = "ایجاد مشترک جدید";
            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void EditItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is Customer model)
            {
                CurrentModifyId = model.Id;

                Name_Textbox.Text = model.Name ;
                PhoneNumber_Textbox.Text = model.PhoneNumber;
                Mobile_Textbox.Text = model.MobileNumber;
                SubscriptionId_Textbox.Text = model.SubscriptionId.ToString();
                Address_Textbox.Text = model.Address;

                OkButton.Content = "ویرایش";
                OkButton.Tag = false;
                PopupTitle.Text = "ویرایش مشترک";
                UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
            }
        }

        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is Customer model)
            {
                Delete_Popup.IsOpen = true;
            }
        }

        private void DeleteConfirm_No_Click(object sender, RoutedEventArgs e)
        {
            Delete_Popup.IsOpen = false;
            SearchTextBox_ApplyModification();

        }

        private void DeleteConfirm_Yes_Click(object sender, RoutedEventArgs e)
        {

            if (DataGrid_Main.SelectedItem is Customer model)
            {
                var context = new KhorshidContext();

                var desiredItem = context.Customers.First(item => item.Id == model.Id);

                context.Customers.Remove(desiredItem);

                context.SaveChanges();
            }

            Delete_Popup.IsOpen = false;

            SearchTextBox_ApplyModification();

        }

    }
}
