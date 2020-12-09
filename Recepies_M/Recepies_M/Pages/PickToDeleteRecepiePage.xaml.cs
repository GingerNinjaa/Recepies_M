using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Models;
using Recepies_M.Services;
using Recepies_M.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickToDeleteRecepiePage : ContentPage
    {
        private DeleteRecepiePageViewModel DeleteRecepiePageViewModel;

        public PickToDeleteRecepiePage()
        {
            InitializeComponent();
        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void CvRecepiesDelete_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curentSelection = e.CurrentSelection.FirstOrDefault() as RecepiesPartial;

            if (curentSelection == null)
            {
                return;
            }
            else
            {
                //kasowanie
                var decision = await Application.Current.MainPage.DisplayAlert("Decyzja", $"Czy chcesz usunąś przepis: {curentSelection.title}", "Tak","Nie");
                if (decision)
                {
                   bool responce = await ApiService.DeleteRecepie(curentSelection.id);

                   if (responce)
                   {
                       await DisplayAlert("Sukces!", "Pomyślnie usunieto przepis", "OK");
                       Navigation.PopModalAsync();
                   }
                   else
                   {
                       await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
                   }
                }
                
                
            }
        }

        
    }
}