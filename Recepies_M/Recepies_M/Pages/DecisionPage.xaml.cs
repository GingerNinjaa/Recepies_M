using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecisionPage : ContentPage
    {
        public DecisionPage()
        {
            InitializeComponent();
        }


        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        private async void AddRecepieBtn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddRecepiePage());
        }
    }
}