using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Models;
using Recepies_M.Services;
using Recepies_M.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMainPage : ContentPage
    {
       // private int pageNumber = 0;

        public AppMainPage()
        {
            InitializeComponent();
        }


        private async void TapMenu_OnTapped(object sender, EventArgs e)
        {
            //GridOverlay
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.SinIn);
        }

        private async void TapCloseMenu_OnTapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        private void CvRecepies_OnRemainingItemsThresholdReached(object sender, EventArgs e)
        {
            AppMainPageViewModel appMainPageVM = new AppMainPageViewModel();
            appMainPageVM.GetAll();
        }


        private void CvRecepies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curentSelection = e.CurrentSelection.FirstOrDefault() as RecepiesPartial;

            if (curentSelection == null)
            {
                return;
            }
            else
            {
                Navigation.PushModalAsync(new RecepiesDetail(curentSelection.id));
                ((CollectionView)sender).SelectedItem = null;
            }


        }

        private async void TapSearch_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchRecepiePage());
        }

        private async void TapContact_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ContactPage());
        }

        private async void TapLogout_OnTapped(object sender, EventArgs e)
        {
           Preferences.Set("accessToken", String.Empty);
           Preferences.Set("tokenExpirationTime", 0);

           Application.Current.MainPage = new LoginPage();
           await Navigation.PushModalAsync(new LoginPage());
        }

        private async void TapAcount_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AccountPage());
        }

        private async void TapAdd_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new DecisionPage());
        }
    }
}