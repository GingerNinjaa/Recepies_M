using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Recepies_M.Pages;
using Recepies_M.Services;
using Xamarin.Forms;

namespace Recepies_M.ViewModels
{
   public class SignupPageViewModel
    {
        public string EntName { get; set; }
        public string EntEmail { get; set; }
        public string EntPassword { get; set; }
        public bool IsEnabled { get; set; }

        public INavigation Navigation { get; set; }

        public ICommand RegisterCommand { get => new Command(async () => await Register()); }
        public ICommand LoginCommand { get => new Command(async () => await Login()); }

        private async Task Register()
        {
            try
            {
                this.IsEnabled = false;
                var responce = await ApiService.RegisterUser(EntName, EntEmail, EntPassword);
                //Jeśli wystąpo poprawna rejestracja 
                if (responce)
                {
                    await Application.Current.MainPage.DisplayAlert("Witaj", "Twoje konto zostało utworzone", "Ok");
                    this.IsEnabled = true;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
                    this.IsEnabled = true;
                }
            }
            catch (Exception e)
            {
           
            }
        }

        private async Task Login()
        {
            this.IsEnabled = false;
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            this.IsEnabled = true;
        }
    }
}
