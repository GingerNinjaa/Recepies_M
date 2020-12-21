using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Recepies_M.Models;
using Recepies_M.Pages;
using Recepies_M.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
   public  class LoginPageViewModel
    {
        public string EntEmail { get; set; }
        public string EntPassword { get; set; }
        public bool IsEnabled { get; set; }

        public ICommand LoginCommand => new Command(async () => await Login());

        private async Task Login()
        {
            try
            {
                this.IsEnabled = false;
                var responce = await ApiService.LoginUser(EntEmail, EntPassword);

                if (responce)
                {
                    await Application.Current.MainPage.DisplayAlert("Witamy", "Zalogowano do aplikacji", "Zaczynamy");
                    //this.BtnLogin.IsEnabled = true;
                    Preferences.Set("email", EntEmail);
                    Preferences.Set("password", EntPassword);

                    Application.Current.MainPage = new NavigationPage(new AppMainPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
                    this.IsEnabled = true;
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
            }
        }
    }
}
