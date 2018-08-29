using Khorshid.Data;
using Khorshid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Khorshid.ViewModels
{
    public class DriverDetailsViewModel : INotifyPropertyChanged
    {

        public DriverDetailsViewModel(int id, KhorshidContext context)
        {
            DriverId = id;
            Context = context;

            Context.WorkPages.Load();

            CurrentDriver = Context.Drivers.FirstOrDefault(d => d.Id == DriverId);

            RequestLastPage();

            context.TownData.Load();
            Locations = context.TownData.Local;

            UpdateVm();
        }

        public void UpdateVm()
        {
            if (CurrentDriver != null)
            {
                DriverName = CurrentDriver.Name;

                var last = LastPageIndex();
                if (CurrentPageNo > last) CurrentPageNo = last;
                OnPropertyChanged(nameof(CurrentPageNoString));

                CurrentWorkPage = Context.WorkPages.Where(wp => wp.DriverId == DriverId).ToList()[_currentPageNo];

                DriverWorks = Context.DriverWorks.Where(dw => dw.WorkPageId == CurrentWorkPage.Id).OrderByDescending(c => c.Id).ToArray();
                DriverTotalPrice = DriverWorks.Sum(dw => dw.Price);

                ClosedPageVisibility = (CurrentWorkPage.IsClosed) ? Visibility.Visible : Visibility.Collapsed;
                CloseButtonVisibility = (CurrentWorkPage.IsClosed) ? Visibility.Collapsed : Visibility.Visible;
                WorkPagePrecentage = (CurrentWorkPage.IsClosed) ? CurrentWorkPage.CommissionPercentage + "%" : "-";
                WorkPageCommission = (CurrentWorkPage.IsClosed) ? (CurrentWorkPage.CommissionPercentage * DriverTotalPrice / 100f).ToString() + " تومان" : "-";

            }
        }

        public void RequestLastPage()
        {
            CurrentPageNo = LastPageIndex();
        }

        public int LastPageIndex()
        {
            var driverPages = Context.WorkPages.Where(wp => wp.DriverId == DriverId);
            var lastPage = driverPages.First(wp => !wp.IsClosed);
            var lastIndex = driverPages.ToList().IndexOf(lastPage);

            return lastIndex;
        }

        private KhorshidContext Context { get; }
        private readonly Driver CurrentDriver;

        private int _driverId;
        private long _driverTotalPrice;
        private string _driverName;
        private int _currentPageNo;
        private string _workPagePrecentage;
        private string _workPageCommission;
        private IEnumerable<DriverWork> _driverWorks;
        private WorkPage _currentWorkPage;
        private Visibility _closedPageVisibility;
        private Visibility _closeButtonVisibility;

        public ObservableCollection<TownData> Locations { get; set; }

        public int DriverId
        {
            get => _driverId;
            private set
            {
                _driverId = value;
                OnPropertyChanged();
            }
        }

        public long DriverTotalPrice
        {
            get => _driverTotalPrice;
            private set
            {
                _driverTotalPrice = value;
                OnPropertyChanged();
            }
        }

        public string DriverName
        {
            get => _driverName;
            private set
            {
                _driverName = value;
                OnPropertyChanged();
            }
        }

        public int CurrentPageNo
        {
            get => _currentPageNo;
            set
            {
                _currentPageNo = (value >= 0) ? value : _currentPageNo ;
                OnPropertyChanged();
            }
        }

        public string CurrentPageNoString
        {
            get => (_currentPageNo + 1).ToString();
        }

      public string WorkPageCommission
        {
            get => _workPageCommission;
            private set
            {
                _workPageCommission = value;
                OnPropertyChanged();
            }
        }

        public string WorkPagePrecentage
        {
            get => _workPagePrecentage;
            private set
            {
                _workPagePrecentage = value;
                OnPropertyChanged();
            }
        }

        public WorkPage CurrentWorkPage
        {
            get => _currentWorkPage;
            set
            {
                _currentWorkPage = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<DriverWork> DriverWorks
        {
            get => _driverWorks;
            private set
            {
                _driverWorks = value;
                OnPropertyChanged();
            }
        }

        public Visibility ClosedPageVisibility
        {
            get => _closedPageVisibility;
            private set
            {
                _closedPageVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility CloseButtonVisibility
        {
            get => _closeButtonVisibility;
            private set
            {
                _closeButtonVisibility = value;
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
