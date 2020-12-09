using System;
using System.Collections.Generic;
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
    public partial class PickToEditRecepiePage : ContentPage
    {
        private EditRecepiePageViewModel EditRecepiePageViewModel;
        public PickToEditRecepiePage()
        {
            InitializeComponent();
        }


        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }



        private void CvRecepiesPick_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as FindRecepie;
            if (currentSelection == null)
            {
                return;
            }
            else
            {
                Navigation.PushModalAsync(new RecepiesDetail(currentSelection.id));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private void CvRecepiesEdit_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curentSelection = e.CurrentSelection.FirstOrDefault() as RecepiesPartial;

            if (curentSelection == null)
            {
                return;
            }
            else
            {
                Navigation.PushModalAsync(new EditRecepiePage(curentSelection.id));
                ((CollectionView)sender).SelectedItem = null;
            }

        }
    }
}