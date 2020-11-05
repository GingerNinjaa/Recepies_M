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
    public partial class SearchRecepiePage : ContentPage
    {
        public SearchRecepiePage()
        {
            InitializeComponent();
        }

        private async void EntSearchMovie_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                return;
            }

            var recepieSearchList = await ApiService.FindRecepies(e.NewTextValue.ToLower());

            CvRecepies.ItemsSource = recepieSearchList;
        }

        private void CvRecepies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault();
            if (currentSelection == null)
            {
                return;
            }
            else
            {
                //TODO
                //dokonczyc jak bede miał recepie detail page
               // Navigation.PushModalAsync(new AppMainPage())
            }
        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}