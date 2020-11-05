using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Models;
using Recepies_M.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMainPage : ContentPage
    {
        public ObservableCollection<RecepiesPartial> RecepiesesColection;


        private int pageNumber = 0;

        public AppMainPage()
        {
            InitializeComponent();
            RecepiesesColection = new ObservableCollection<RecepiesPartial>();
            GetAllRecepies();
        }

        private async void GetAllRecepies()
        {

            pageNumber++;

            var recepies = await ApiService.GetAllRecepiesPartial(this.pageNumber, 5);
            foreach (var recepie in recepies)
            {
                RecepiesesColection.Add(recepie);
            }

            CvRecepies.ItemsSource = RecepiesesColection;
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
            GetAllRecepies();
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
    }
}