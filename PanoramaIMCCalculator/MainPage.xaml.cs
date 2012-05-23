using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PanoramaIMCCalculator.Model;
// using Microsoft.Phone.Shell;

namespace PanoramaIMCCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            // Affecter l'exemple de données au contexte de données du contrôle ListBox
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Charger les données pour les éléments ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }


        /// <summary>
        /// Set the visibility of all imc info textBlocks to Collapsed.
        /// </summary>
        private void collapseAllIMCInfo()
        {
            HardObesityZoneTextBlock.Visibility = Visibility.Collapsed;
            ModerateObesityZoneTextBlock.Visibility = Visibility.Collapsed;
            SurpoidsZoneTextBlock.Visibility = Visibility.Collapsed;
            MaigreurZoneTextBlock.Visibility = Visibility.Collapsed;
            DenutritionZoneTextBlock.Visibility = Visibility.Collapsed;

            GainKilosToNormalWeightTextBlock.Visibility = Visibility.Collapsed;
            LoseKilosToNormalWeightTextBlock.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Display info on how many kilo to lose in order to be in normal zone.
        /// </summary>
        /// <param name="currentImc">The current imc</param>
        /// <param name="currentHeight">The current height in cm</param>
        /// <param name="currentWeight">The current weight in kg</param>
        private void displayLoseWeightTextBlockFromIMC(double currentImc, double currentHeight, double currentWeight)
        {
            IMCCalculatorManager manager = new IMCCalculatorManager();
            LoseKilosToNormalWeightTextBlock.Text = manager.weightToLoseFromIMC(currentImc, currentHeight, currentWeight);
            LoseKilosToNormalWeightTextBlock.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Display info on how many kilo to gain in order to be in normal zone.
        /// </summary>
        /// <param name="currentImc">The current imc</param>
        /// <param name="currentHeight">The current height in cm</param>
        /// <param name="currentWeight">The current weight in kg</param>
        private void displayGainWeightTextBlockFromIMC(double currentImc, double currentHeight, double currentWeight)
        {
            IMCCalculatorManager manager = new IMCCalculatorManager();
            GainKilosToNormalWeightTextBlock.Text = manager.weightToGainFromIMC(currentImc, currentHeight, currentWeight);
            GainKilosToNormalWeightTextBlock.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Display the correct info according to the current imc on the stack panel.
        /// </summary>
        /// <param name="imc">The current IMC.</param>
        private void displayIMCinfo(double imc)
        {
            if (imc > 35)
            {
                HardObesityZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (30 < imc && imc < 35)
            {
                ModerateObesityZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (25 < imc && imc < 30)
            {
                SurpoidsZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (16.5 < imc && imc < 18.5)
            {
                MaigreurZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (imc < 16.5)
            {
                DenutritionZoneTextBlock.Visibility = Visibility.Visible;
            }

        }

        // IMC calculation.
        /// <summary>
        /// Calculate an IMC for metric height and weight value
        /// </summary>
        /// <param name="height">the user height in cm</param>
        /// <param name="weight"> the user weight in kg</param>
        /// <returns></returns>
        private double calculateMetricIMC(double height, double weight)
        {
            IMCCalculatorManager manager = new IMCCalculatorManager();
            double imc = manager.calculateMetricIMC(height, weight);
            
            displayIMCinfo(imc);

            String s_imc = Math.Round(imc, 2).ToString("R"); // On prend toutes les valeurs de décimal. Si on ne les veut pas, il suffit de mettre F0 à la place de R.
            int nbDecimal = s_imc.Length - s_imc.IndexOf(",");

            s_imc = "Votre IMC est de " + s_imc + "\n" + manager.infoFromIMC(imc);

            if (imc < 18.5)
            {
                displayGainWeightTextBlockFromIMC(imc, height, weight);
                s_imc = s_imc + "\n" + manager.weightToGainFromIMC(imc, height, weight);
            }
            else if (imc > 25)
            {
                displayLoseWeightTextBlockFromIMC(imc, height, weight);
                s_imc = s_imc + "\n" + manager.weightToLoseFromIMC(imc, height, weight);
            }
            metricIMCResultTextBlock.Text = s_imc;

            return imc;
        }

        /// <summary>
        /// Calculate an IMC for english height and weight value
        /// </summary>
        /// <param name="height">the user height in po</param>
        /// <param name="weight"> the user weight in lb</param>
        /// <returns></returns>
        private double calculateEnglishIMC(int height, double weight)
        {
            double imc = 0.0;

            imc = (weight * 703) / (height * height); // Multiply by 703 is in the formula.

            return imc;
        }


        // User interface components management

        private void deleteTextBoxContent(TextBox a_textBox)
        {
            collapseAllIMCInfo();
            a_textBox.Text = "";
        }

        // User interface interaction management

        private void clickMetricHeightTextBox(object sender, System.Windows.Input.GestureEventArgs e)
        {
            deleteTextBoxContent(metricHeightTextBox);
        }

        private void clickMetricWeightTextBox(object sender, System.Windows.Input.GestureEventArgs e)
        {
            deleteTextBoxContent(metricWeightTextBox);
        }



        // Calculate Button action management
        private void defineFeedbackHeightandWeightTextBlocks(string height, string weight)
        {
            feedbackHeightTextBlock.Text = height;
            feedbackWeightTextBlock.Text = weight;
        }

        private void clickCalculateMetricIMC(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                string s_height = metricHeightTextBox.Text;
                if (s_height.Contains("."))
                    s_height = s_height.Substring(0, s_height.IndexOf("."));

                string s_weight = metricWeightTextBox.Text;
                if (s_weight.Contains("."))
                    s_weight = s_weight.Substring(0, s_weight.IndexOf("."));

                Double height = Double.Parse(s_height);
                Double weight = Double.Parse(s_weight);
                Double imc = Math.Floor(calculateMetricIMC(height, weight));


                defineFeedbackHeightandWeightTextBlocks(metricHeightTextBox.Text + " cm", metricWeightTextBox.Text + " kg");
                
                // Manage the Tile according to the imc result.
                TileManager tileManager = new TileManager();
                IMCCalculatorManager imcManager = new IMCCalculatorManager();
                tileManager.changeBackTile(imc, height, weight, imcManager.infoFromIMC(imc));
            }
            catch
            {
                metricIMCResultTextBlock.Text = "Un problème est survenu lors du calcul de votre IMC.\nVeuillez entrer à nouveau votre taille et votre poids.";
            }
        }

    }
}