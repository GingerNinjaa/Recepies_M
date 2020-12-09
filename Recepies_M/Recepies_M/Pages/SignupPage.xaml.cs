using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void LblLogin_OnTapped(object sender, EventArgs e)
        {
            this.lblLogin.IsEnabled = false; 
            await Navigation.PushModalAsync(new LoginPage());
            this.lblLogin.IsEnabled = true;
        }


        private async void BtnRegister_OnClicked(object sender, EventArgs e)
        {
            this.BtnRegister.IsEnabled = false;
            var responce = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            //Jeśli wystąpo poprawna rejestracja 
            if (responce)
            {
                await DisplayAlert("Witaj", "Twoje konto zostało utworzone", "Ok");
                this.BtnRegister.IsEnabled = true;
                await Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
                this.BtnRegister.IsEnabled = true;
            }
        }
    }
}