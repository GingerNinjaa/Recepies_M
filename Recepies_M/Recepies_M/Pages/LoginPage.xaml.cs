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
            Navigation.PopModalAsync();
        }

        private async void ImgLogin_OnTapped(object sender, EventArgs e)
        {
            var responce = await ApiService.LoginUser(EntEmail.Text, EntPassword.Text);

            if (responce)
            {
                Preferences.Set("email",EntEmail.Text);
                Preferences.Set("password",EntPassword.Text);

                Application.Current.MainPage = new NavigationPage(new AppMainPage());
            }
            else
            {
                await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
            }
        }
    }
}