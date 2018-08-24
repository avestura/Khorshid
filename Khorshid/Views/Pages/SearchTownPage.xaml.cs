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

        private ObservableCollection<TownPriceViewModel> Context { get; } = new ObservableCollection<TownPriceViewModel>();

        public string TextBoxPreviousText { get; set; } = "";

        public SearchTownPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.DataContext = Context;

            Context.Clear();
            KhorshidDataManager.GetTownPriceViewModels().ForEach(town => Context.Add(town));
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
                KhorshidDataManager.GetTownPriceViewModels().ForEach(town => Context.Add(town));
            }
            else
            {
                string term = SearchBox.Text;

                SearchEngine.ApplySearchOnCollection(term, Context);

            }
        }

        private void DataGrid_Main_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var row = e.Row.DataContext as TownPriceViewModel;
            var data = App.CurrentApp.Configuration.TownData;

            var editedItem = data.Find(item => item.TownId == row.TownId);

            editedItem.Price = row.Price.Replace(" تومان", "").Replace("تومان", "");
            editedItem.Tag = row.Tag;
            editedItem.Town = row.Town;

            App.CurrentApp.Configuration.SaveSettingsToFile();

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if (Town_TextBox.Text.Trim()?.Length == 0
               && Price_Textbox.Text.Trim()?.Length == 0)
            {
                return;
            }

            if (OkButton.Tag is bool isCreateMode)
            {
                if (isCreateMode)
                {
                    var townData = new TownData()
                    {
                        TownId = CurrentModifyId,
                        Price = Price_Textbox.Text,
                        Town = Town_TextBox.Text,
                        Tag = Tags_Textbox.Text
                    };

                    App.CurrentApp.Configuration.TownData.Add(townData);
                    App.CurrentApp.Configuration.SaveSettingsToFile();
                    SearchTextBox_ApplyModification();
                }
                else
                {
                    var townData = App.CurrentApp.Configuration.TownData.First(item => item.TownId == CurrentModifyId);
                    townData.Town = Town_TextBox.Text;
                    townData.Price = Price_Textbox.Text;
                    townData.Tag = Tags_Textbox.Text;

                    App.CurrentApp.Configuration.SaveSettingsToFile();

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
            CurrentModifyId = App.CurrentApp.Configuration.TownData.Max(item => item.TownId) + 1;
            Town_TextBox.Text = "";
            Price_Textbox.Text = "";
            Tags_Textbox.Text = "";
            OkButton.Content = "ساخت جدید";
            OkButton.Tag = true;

            UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
        }

        private void EditItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is TownPriceViewModel model)
            {
                CurrentModifyId = model.TownId;
                Town_TextBox.Text = model.Town;
                Price_Textbox.Text = model.Price.Replace(" تومان", "").Replace("تومان", "");
                Tags_Textbox.Text = model.Tag;
                OkButton.Content = "ویرایش";
                OkButton.Tag = false;

                UI_Overlay.ShowUsingLinearAnimation(milliSeconds: 250);
            }
        }

        private void DeleteItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Main.SelectedItem is TownPriceViewModel model)
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

            if (DataGrid_Main.SelectedItem is TownPriceViewModel model)
            {
                var desiredItem = App.CurrentApp.Configuration.TownData.First(item => item.TownId == model.TownId);

                App.CurrentApp.Configuration.TownData.Remove(desiredItem);

                App.CurrentApp.Configuration.SaveSettingsToFile();
            }

            Delete_Popup.IsOpen = false;

            SearchTextBox_ApplyModification();

        }

    }
}
