using Akavache;
using ToossyApp.Models;
using ToossyApp.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ToossyApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                OnIsBusyChanged();
            }
        }

        bool _isLocalizing;
        public bool IsLocalizing
        {
            get { return _isLocalizing; }
            set
            {
                _isLocalizing = value;
                OnPropertyChanged();
                OnIsLocalizingChanged();
            }
        }

        GeoCoords _myPosition;
        public GeoCoords MyPosition
        {
            get { return _myPosition; }
            set
            {
                _myPosition = value;
                OnPropertyChanged();
            }
        }

        protected INavService NavService { get; private set; }

        protected BaseViewModel(INavService navService)
        {
            NavService = navService;
        }

        public abstract Task Init();       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnIsLocalizingChanged()
        {
        }

        protected virtual void OnIsBusyChanged()
        {
        }

        public double distanceBetweenTwoLocations(double lat1, double lon1, double lat2, double lon2)
        {
            //calcule la distance en kilomètre
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;

            dist = dist * 1.609344;

            return dist;
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        private INavService navService;


        public BaseViewModel(INavService navService, IBlobCache cache) : base(navService)
        {
        }

        public override async Task Init()
        {
            await Init(default(TParameter));
        }

        public abstract Task Init(TParameter parameter);
    }
}
