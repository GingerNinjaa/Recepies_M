using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Recepies_M.Models;
using Recepies_M.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
    public class AccountPageViewModel : INotifyPropertyChanged
    {

        bool isRefreshing;
        private int _UserId;
        private int itemCount;
        public string Name { get; set; }
        public string Email { get; set; }

        public ObservableCollection<RecepiesPartial> RecepiesPartials { get; private set; }

        public AccountPageViewModel()
        {
            RecepiesPartials = new ObservableCollection<RecepiesPartial>();
            _UserId = Preferences.Get("userId", 0);
            GetRecepies();
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await RefreshApi();
        }

        public async Task RefreshApi()
        {
            GetUserData();

            var recepieSearchList = await ApiService.GetAllRecepiesPartialById(this._UserId, 1, 5);

            foreach (var recepie in recepieSearchList)
            {
                RecepiesPartials.Add(recepie);
            }

            for (int i = 1; i <= recepieSearchList.Count; i++)
            {
                RecepiesPartials.RemoveAt(0);
            }

            IsRefreshing = false;
        }

        public void GetUserData()
        {
            this.Name = Preferences.Get("userName", String.Empty);
            this.Email = Preferences.Get("email", String.Empty);
        }

        public async Task GetRecepies()
        {
            GetUserData();

            var recepieSearchList = await ApiService.GetAllRecepiesPartialById(this._UserId, 1, 5);

            foreach (var recepie in recepieSearchList)
            {
                RecepiesPartials.Add(recepie);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
