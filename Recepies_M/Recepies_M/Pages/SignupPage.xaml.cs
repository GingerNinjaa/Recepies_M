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

        private async void ImgSignup_OnTapped(object sender, EventArgs e)
        {
           var responce= await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            //Jeśli wystąpo poprawna rejestracja 
           if (responce)
           {
               await DisplayAlert("Witaj", "Twoje konto zostało utworzone", "Ok");
               await Navigation.PushModalAsync(new LoginPage());
           }
           else
           {
               await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
            }
        }

        private async void LblLogin_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}