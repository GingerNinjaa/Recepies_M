using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recepies_M.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recepies_M.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecepiePage : ContentPage
    {
        public List<Ingredient> Iingredients;
        public List<PreparationStep> PreparationSteps;

        private Ingredient currentSelectionIngredient;
        private PreparationStep currentSelectionPreparationStep;

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

 
    }
}