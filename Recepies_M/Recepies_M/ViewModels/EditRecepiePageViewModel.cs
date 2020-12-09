using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Models;
using Recepies_M.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
   public class EditRecepiePageViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private int _UserId;

        public ObservableCollection<RecepiesPartial> RecepiesPartials { get; private set; }

        public EditRecepiePageViewModel()
        {
            RecepiesPartials = new ObservableCollection<RecepiesPartial>();
            _UserId = Preferences.Get("userId", 0);
            GetRecepies();
        }

        public void GetUserData()
        {
            this.Name = Preferences.Get("userName", String.Empty);
            this.Email = Preferences.Get("email", String.Empty);
        }

        public async Task GetRecepies()
        {
            GetUserData();

            var recepieSearchList = await ApiService.GetAllRecepiesPartialById(this._UserId, 1, 99);

            if (recepieSearchList.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Nie masz własnych przepisów do edycji", "Ok");
            }
            else
            {
                foreach (var recepie in recepieSearchList)
                {
                    RecepiesPartials.Add(recepie);
                }
            }
           

        }
    }
}
