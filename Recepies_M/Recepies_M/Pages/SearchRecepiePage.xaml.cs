using System;
using System.Collections.Generic;
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

            
            CvRecepiesSearch.ItemsSource = recepieSearchList;
        }

  

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

       

        private void CvRecepiesSearch_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as FindRecepie;
            if (currentSelection == null)
            {
                return;
            }
            else
            {
                Navigation.PushModalAsync(new RecepiesDetail(currentSelection.id));
                ((CollectionView) sender).SelectedItem = null;
            }
        }
    }
}