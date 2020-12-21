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
using Recepies_M.Settings;
using Xamarin.Essentials;
using Recepies_M.Pages;
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
    public class AppMainPageViewModel : INotifyPropertyChanged
    {
        bool isRefreshing;
        private int pageNumber = 0;
        public ObservableCollection<RecepiesPartial> RecepiesesColection { get; private set; }
        public string userName { get; set; }

        public AppMainPageViewModel()
        {
            RecepiesesColection = new ObservableCollection<RecepiesPartial>();
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
        public ICommand ItemTresholdReachedCommand => new Command(async () => await GetAll());


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
            try
            {

       
            //refresh user name
            this.userName = Preferences.Get("userName", String.Empty);
            pageNumber++;

            var recepies = await ApiService.GetAllRecepiesPartial(pageNumber, 5);
            if (recepies.Count == 0)
            {
                pageNumber--;
            }
            foreach (var recepie in recepies)
            {
                RecepiesesColection.Add(recepie);
            }
            }
            catch (Exception e)
            {
                
            }
        }

        public async Task OnSelection()
        {
            //try
            //{
            //    var curentSelection = e.CurrentSelection.FirstOrDefault() as RecepiesPartial;

            //    if (curentSelection == null)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        Navigation.PushModalAsync(new RecepiesDetail(curentSelection.id));
            //        ((CollectionView)sender).SelectedItem = null;
            //    }
            //}
            //catch (Exception e)
            //{
            //}
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
