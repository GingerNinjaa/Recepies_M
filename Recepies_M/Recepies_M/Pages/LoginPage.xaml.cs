using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            this.ImgBack.IsEnabled = false;
            Navigation.PopModalAsync();
            this.ImgBack.IsEnabled = true;
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            this.BtnLogin.IsEnabled = false;
            var responce = await ApiService.LoginUser(EntEmail.Text, EntPassword.Text);

            if (responce)
            {
                await DisplayAlert("Witamy", "Zalogowano do aplikacji", "Zaczynamy");
                this.BtnLogin.IsEnabled = true;
                Preferences.Set("email", EntEmail.Text);
                Preferences.Set("password", EntPassword.Text);

                Application.Current.MainPage = new NavigationPage(new AppMainPage());
            }
            else
            {
                await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
                this.BtnLogin.IsEnabled = true;
            }
        }
    }
}