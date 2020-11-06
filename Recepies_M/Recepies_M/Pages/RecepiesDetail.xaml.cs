using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Recepies_M.Models;
using Recepies_M.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecepiesDetail : ContentPage
    {

        public ObservableCollection<Ingredient> IngredientsColection;
        public ObservableCollection<PreparationStep> PreparationStepsColection;
        public Recepies Recepieses;

        public RecepiesDetail(int recepieId)
        {
            InitializeComponent();
            IngredientsColection = new ObservableCollection<Ingredient>();
            PreparationStepsColection = new ObservableCollection<PreparationStep>();
            Recepieses = new Recepies();
            GetRecepieDetail(recepieId);
            
        }

        private async  void GetRecepieDetail(int movieId)
        {
            
            var recepie = await ApiService.GetRecepieDetail(movieId);

            Recepieses = (Recepies)recepie.First();



            this.ImgMovie.Source = Recepieses.imageUrl;
            this.LblCategory.Text = Recepieses.category;
            this.LblPeople.Text = Recepieses.people.ToString();
            this.LblTitle.Text = Recepieses.title;
            this.LblDifficulty.Text = Recepieses.difficulty;
            this.LblPeople.Text = $"Dla {Recepieses.people.ToString()} osób";
            this.LblPreparationTime.Text = $"Czas przygotowania: {Recepieses.preparationTime.ToString()} min";
            this.LblDescription.Text = Recepieses.description;
            



            var test = recepie.SelectMany(x => x.ingredients).ToList();

            foreach (var lala in test)
            {
                IngredientsColection.Add(lala);
            }

            CvIngredients.ItemsSource = IngredientsColection;

            var test2 = recepie.SelectMany(x => x.preparationSteps).ToList();

            foreach (var lala in test2)
            {
                PreparationStepsColection.Add(lala);
            }

            LvPreparationStep.ItemsSource = PreparationStepsColection;

        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}