using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Khorshid.ViewModels
{
    public class TownPriceViewModel : INotifyPropertyChanged
    {
        private int _townId;
        private string _town = string.Empty;
        private string _price = string.Empty;
        private string _tag = string.Empty;

        public int TownId
        {
            get => _townId;
            set
            {
                _townId = value;
                OnPropertyChanged();
            }
        }

        public string Town
        {
            get => _town;
            set
            {
                _town = value;
                OnPropertyChanged();
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;

                OnPropertyChanged();
            }
        }

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
