using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Recepies_M.Models;
using Recepies_M.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecepiePage : ContentPage
    {
        public List<Ingredient> Iingredients;
        public List<PreparationStep> PreparationSteps;

        private Ingredient currentSelectionIngredient;
        private PreparationStep currentSelectionPreparationStep;

        private MediaFile file;

        public AddRecepiePage()
        {
            InitializeComponent();
            Iingredients = new List<Ingredient>();
            PreparationSteps = new List<PreparationStep>();
        }

        private void ImgBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CvIngredients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (Ingredient)e.CurrentSelection.FirstOrDefault();
            this.currentSelectionIngredient = test;
            SelectedIngredientsItem(test);
        }
        private void CvPreparationStep_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (PreparationStep)e.CurrentSelection.FirstOrDefault();
            this.currentSelectionPreparationStep = test;
            SelectedPreparationStepsItem(test);
        }

        private void SelectedIngredientsItem(Ingredient value)
        {
            this.EntryName.Text = value.name;
            this.EntryAmount.Text = value.amount.ToString();
            this.EntryAmountDesc.Text = value.amountDesc;
        }
        private void SelectedPreparationStepsItem(PreparationStep value)
        {
            this.EntryStepNumber.Text = value.stepNumber.ToString();
            this.EntryText.Text = value.text;
        }

        private void UpdateIngredient()
        {
            this.CvIngredients.ItemsSource = null;
            this.CvIngredients.ItemsSource = Iingredients;

        }
        private void UpdatePreparationSteps()
        {
            this.CvPreparationStep.ItemsSource = null;
            this.CvPreparationStep.ItemsSource = PreparationSteps;

        }

        //Iingredients
        private void DeleteIngBtn_OnClicked(object sender, EventArgs e)
        {
            var Element = this.Iingredients.Find(x =>
                x.name == this.EntryName.Text && x.amount.ToString() == this.EntryAmount.Text &&
                x.amountDesc == this.EntryAmountDesc.Text);

            this.Iingredients.Remove(Element);

            UpdateIngredient();
        }
        private void EditIngBtn_OnClicked(object sender, EventArgs e)
        {
            //Search
            var Element = this.Iingredients.FindIndex(x =>
                x.name == this.currentSelectionIngredient.name && x.amount.ToString() == this.currentSelectionIngredient.amount.ToString() &&
                x.amountDesc == this.currentSelectionIngredient.amountDesc);

            //Edit
            this.Iingredients[Element].name = this.EntryName.Text;
            this.Iingredients[Element].amount = Int32.Parse(this.EntryAmount.Text);
            this.Iingredients[Element].amountDesc = this.EntryAmountDesc.Text;

            //Update
            UpdateIngredient();
        }
        private void AddIngBtn_OnClicked(object sender, EventArgs e)
        {
            this.Iingredients.Add(new Ingredient { name = this.EntryName.Text, amount = Int32.Parse(this.EntryAmount.Text), amountDesc = this.EntryAmountDesc.Text });

            UpdateIngredient();
        }

        //PreparationSteps
        private void AddPrepBtn_OnClicked(object sender, EventArgs e)
        {
            this.PreparationSteps.Add(new PreparationStep { stepNumber = Int32.Parse(this.EntryStepNumber.Text), text = this.EntryText.Text });

            UpdatePreparationSteps();
        }
        private void EditPrepBtn_OnClicked(object sender, EventArgs e)
        {
            //Search
            var Element = this.PreparationSteps.FindIndex(x =>
                x.stepNumber == this.currentSelectionPreparationStep.stepNumber && x.text == this.currentSelectionPreparationStep.text);

            //Edit
            this.PreparationSteps[Element].stepNumber = Int32.Parse(this.EntryStepNumber.Text);
            this.PreparationSteps[Element].text = this.EntryText.Text;

            //Update
            UpdatePreparationSteps();
        }
        private void DeletePrepBtn_OnClicked(object sender, EventArgs e)
        {
            var Element = this.PreparationSteps.Find(x =>
                x.stepNumber == this.currentSelectionPreparationStep.stepNumber && x.text == this.currentSelectionPreparationStep.text);

            this.PreparationSteps.Remove(Element);

            UpdatePreparationSteps();
        }


        private async void ImgAdd_OnTapped(object sender, EventArgs e)
        {
        }

        private async void AddimageCameraBtn_OnClicked(object sender, EventArgs e)
        {
            //var result = await MediaPicker.CapturePhotoAsync();

            //if (result != null)
            //{
            //    var stream = await result.OpenReadAsync();

            //    ImgAddFromFile.Source = ImageSource.FromStream(() => stream);
            //}
            //ImgAddFromFile.HeightRequest = 200;

            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    DisplayAlert("No Camera", ":( No camera available.", "OK");
            //    return;
            //}

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "Sample",
            //    Name = "test.jpg"
            //});

            //if (file == null)
            //    return;

            //ImgAddFromFile.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});

            //ImgAddFromFile.HeightRequest = 200;
        }

        private async void AddimageGalleryaBtn_OnClicked(object sender, EventArgs e)
        {

            ImgAddFromFile.HeightRequest = 300;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            try
            {
                this.file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    CompressionQuality = 50

                });
            }
            catch (Exception exception)
            {
                DisplayAlert("Hmmm", "Wystąpił problem z twoim plikiem.", "OK");
            }
            if (file == null)
                return;

            // image.Source = ImageSource.FromStream(() =>
            ImgAddFromFile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

          
        }

        private async void BtAddRecepie_OnClicked(object sender, EventArgs e)
        {
            Recepies  recepies = new Recepies();

          //var ImageArray = FromFile.ToArray(file.GetStream());

            recepies.title = this.EntryTitle.Text;
            recepies.description = this.EntryDescription.Text;
            recepies.preparationTime = Int32.Parse(this.EntryPreparationTime.Text);
            recepies.cookingTime = Int32.Parse(this.EntryCookingTime.Text);
            recepies.people = Int32.Parse(this.EntryPeople.Text);
            recepies.difficulty = this.EntryDifficulty.Text;
            recepies.category = this.EntryCategory.Text;
            recepies.UserId = Preferences.Get("userId", 0);

            recepies.ingredients = this.Iingredients;
            recepies.preparationSteps = this.PreparationSteps;

           await ApiService.AddRecepie(recepies);
           var responce = await ApiService.AddRecepieIMG( recepies.title, this.file);

           if (responce)
           {
               await DisplayAlert("Sukces!", "Pomyślnie dodano nowy przepis", "OK");
               Application.Current.MainPage = new NavigationPage(new AppMainPage());
           }
           else
           {
               await DisplayAlert("Ooops", "Coś poszło nie tak", "Zamknij");
           }
        }
    }
}