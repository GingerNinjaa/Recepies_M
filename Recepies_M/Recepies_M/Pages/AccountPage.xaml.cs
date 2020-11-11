using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Services;
using Recepies_M.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {

        private AccountPageViewModel AccountPageViewModel;
        public AccountPage()
        {
            InitializeComponent();
            //Filldata();
            //FillCaruse();
        }


        public void Filldata()
        {
            // Preferences.Get("userName",String.Empty);
            this.LblUserNameValue.Text = Preferences.Get("userName", String.Empty);
            this.LblEmailValue.Text = Preferences.Get("email", String.Empty);
        }

        private async void FillCaruse()
        {
            
          // var userId = Int32.Parse(Preferences.Get("userId", string.Empty));

            var recepieSearchList = await ApiService.GetAllRecepiesPartialById(1,1,5);

            CVMyRecepies.ItemsSource = recepieSearchList;
        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.FrEdit.HeightRequest = 420;
            this.SlEdit.IsVisible = false;
            this.SlClose.IsVisible = true;
            this.SVEdit.IsVisible = true;
            this.SlOption.IsVisible = true;
        }

        private void BtClose_OnClicked(object sender, EventArgs e)
        {
            this.FrEdit.HeightRequest = 150;
            this.SlEdit.IsVisible = true;
            this.SlClose.IsVisible = false;
            this.SVEdit.IsVisible = false;
            this.SlOption.IsVisible = false;
        }
    }
}