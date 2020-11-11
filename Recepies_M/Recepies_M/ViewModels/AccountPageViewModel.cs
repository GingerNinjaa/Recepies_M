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
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
    public class AccountPageViewModel : INotifyPropertyChanged
    {

        const int RefreshDuration = 2;
        bool isRefreshing;
        public ObservableCollection<RecepiesPartial> RecepiesPartials { get; private set; }

        public AccountPageViewModel()
        {
            RecepiesPartials = new ObservableCollection<RecepiesPartial>();
            RefreshApi();
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
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            
            RefreshApi();

            IsRefreshing = false;
        }

       public  async Task RefreshApi()
        {
            var recepieSearchList = await ApiService.GetAllRecepiesPartialById(1, 1, 5);

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
