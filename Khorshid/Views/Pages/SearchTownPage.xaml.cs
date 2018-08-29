using Khorshid.Data;
using Khorshid.DataAccessLayer;
using Khorshid.Engines;
using Khorshid.Models;
using Khorshid.ViewModels;
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
    /// Interaction logic for SearchTownPage.xaml
    /// </summary>
    public partial class SearchTownPage : Page
    {

        private int CurrentModifyId { get; set; }

        private ObservableCollection<TownData> Context { get; } = new ObservableCollection<TownData>();

        public string TextBoxPreviousText { get; set; } = "";

        public SearchTownPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.DataContext = Context;

            Context.Clear();
            KhorshidDataManager.GetModifiedTownData().ForEach(town => Context.Add(town));

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
                Context.Clear();
                KhorshidDataManager.GetModifiedTownData().ForEach(town => Context.Add(town));
            }
            else
            {
                string term = SearchBox.Text.Replace("آ", "ا");

                SearchEngine.ApplySearchOnCollection(term, Context);

            }
        }

        private void DataGrid_Main_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var row = e.Row.DataContext as TownData;

            var context = new KhorshidContext();

            var data = context.TownData;

            var editedItem = data.Find(row.Id);

            editedItem.Price = row.Price.Replace(" تومان", "").Replace("تومان", "");
            editedItem.Tag = row.Tag;
            editedItem.Town = row.Town;

            context.SaveChanges();

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if (Town_TextBox.Text.Trim()?.Length == 0
               && Price_Textbox.Text.Trim()?.Length == 0)
            {
                return;
            }

            var context = new KhorshidContext();

            if (OkButton.Tag is bool isCreateMode)
            {
                if (isCreateMode)
                {
                    var townData = new TownData()
                    {
                        Price = Price_Textbox.Text,
                        Town = Town_TextBox.Text,
                        Tag = Tags_Textbox.Text
                    };

                    context.TownData.Add(townData);
                    context.SaveChanges();
                    SearchTextBox_ApplyModification();
                }
                else
                {
                    var townData = context.TownData.First(item => item.Id == CurrentModifyId);
                    townData.Town = Town_TextBox.Text;
                    townData.Price = Price_Textbox.Text;
                    townData.Tag = Tags_Textbox.Text;

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

            CurrentModifyId = context.TownData.Max(item => item.Id) + 1;
            Town_TextBox.Text = "";
            Price_Textbox.Text = "";
            Tags_Textbox.Text = "";
            OkButton.Content = "ساخت جدید";
            OkButton.Tag = true;
            PopupTitle.Text = "ایجاد ناحیه جدید";
            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void EditItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is TownData model)
            {
                CurrentModifyId = model.Id;
                Town_TextBox.Text = model.Town;
                Price_Textbox.Text = model.Price.Replace(" تومان", "").Replace("تومان", "");
                Tags_Textbox.Text = model.Tag;
                OkButton.Content = "ویرایش";
                OkButton.Tag = false;
                PopupTitle.Text = "ویرایش ناحیه";
                UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
            }
        }

        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is TownData model)
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

            if (DataGrid_Main.SelectedItem is TownData model)
            {
                var context = new KhorshidContext();

                var desiredItem = context.TownData.First(item => item.Id == model.Id);

                context.TownData.Remove(desiredItem);

                context.SaveChanges();
            }

            Delete_Popup.IsOpen = false;

            SearchTextBox_ApplyModification();

        }

    }
}
