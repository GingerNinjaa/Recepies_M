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