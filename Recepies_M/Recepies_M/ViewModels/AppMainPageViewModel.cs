using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class AppMainPageViewModel : INotifyPropertyChanged
    {
        bool isRefreshing;
        static int pageNumber = 0;
        public ObservableCollection<RecepiesPartial> RecepiesesColection { get; private set; }
        public string userName { get; set; }

        public AppMainPageViewModel()
        {
            RecepiesesColection = new ObservableCollection<RecepiesPartial>();
            //username
            GetAll();
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
            //czysci kolekcje
            RecepiesesColection.Clear();
            pageNumber = 1;

            var recepies = await ApiService.GetAllRecepiesPartial(1, 5);
            foreach (var recepie in recepies)
            {
                RecepiesesColection.Add(recepie);
            }

            //refresh user name
             this.userName = Preferences.Get("userName", String.Empty);


        IsRefreshing = false;
        }

        public async Task GetAll()
        {

            //refresh user name
            this.userName = Preferences.Get("userName", String.Empty);
            pageNumber++;

            var recepies = await ApiService.GetAllRecepiesPartial(pageNumber, 5);
            if (recepies == null)
            {
                pageNumber--;
            }
            foreach (var recepie in recepies)
            {
                RecepiesesColection.Add(recepie);
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
