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

namespace PanoramaIMCCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {

        public static Boolean isMale = true;

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

        private void gotFocusOnTextBox(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            deleteTextBoxContent((TextBox)sender);
            mainViewScrollViewer.Height = 300;
        }
    
        private void lostFocusOnTextBox(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            TextBox tb = (TextBox)sender;
            if ( tb.Text == "")
            {
                if ( tb == metricHeightTextBox )
                    metricHeightTextBox.Text = "Taille";

                if ( tb == metricWeightTextBox )
                    metricWeightTextBox.Text = "Poids";
            }

            mainViewScrollViewer.Height = 498;
        }


        // Calculate Button action management
        private void defineFeedbackHeightandWeightTextBlocks(string height, string weight)
        {
            feedbackHeightTextBlock.Text = height;
            feedbackWeightTextBlock.Text = weight;
        }

        private string getGender()
        {
            string gender = "Homme";
            if (!isMale)
            {
                gender = "Femme";
            }

            return gender;
        }

        private void changeAllHubTilesToStartState()
        {
            ObesiteSevereHubTile.Title = "Obésité sévère";
            ObesiteHubTile.Title = "Obésité";
            SurpoidsHubTile.Title = "Surpoids";
            NormalHubTile.Title = "Normal";
            MaigreurHubTile.Title = "Maigreur";
            DenutritionHubTile.Title = "Dénutrition";

            ObesiteSevereHubTile.Message = "";
            ObesiteHubTile.Message = "";
            SurpoidsHubTile.Message = "";
            NormalHubTile.Message = "";
            MaigreurHubTile.Message = "";
            DenutritionHubTile.Message = "";

            string gender = getGender();

            ObesiteSevereHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "Obese.png", UriKind.RelativeOrAbsolute));
            ObesiteHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "Obese.png", UriKind.RelativeOrAbsolute));
            SurpoidsHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "Surpoids.png", UriKind.RelativeOrAbsolute));
            NormalHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "NormalSelected.png", UriKind.RelativeOrAbsolute));
            MaigreurHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "Maigreur.png", UriKind.RelativeOrAbsolute));
            DenutritionHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "Denutrition.png", UriKind.RelativeOrAbsolute));
            
        }

        private void changeBackHubTile(double imc)
        {
            changeAllHubTilesToStartState();
            string gender = getGender();

            if (imc > 35)
            {
                ObesiteSevereHubTile.Title = "";
                ObesiteSevereHubTile.Message = "Vous êtes dans la zone d'obésité sévère.";
                ObesiteSevereHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "ObeseSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (30 < imc && imc < 35)
            {
                ObesiteHubTile.Title = "";
                ObesiteHubTile.Message = "Vous êtes dans la zone d'obésité modérée.";
                ObesiteHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "ObeseSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (25 < imc && imc < 30)
            {
                SurpoidsHubTile.Title = "";
                SurpoidsHubTile.Message = "Vous êtes dans la zone de surpoids.";
                SurpoidsHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "SurpoidsSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if ( 18.5 < imc && imc < 25)
            {
                NormalHubTile.Title = "";
                NormalHubTile.Message = "Vous etes dans la zone normale. Bravo!!";
                NormalHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "NormalSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (16.5 < imc && imc < 18.5)
            {
                MaigreurHubTile.Title = "";
                MaigreurHubTile.Message = "Vous êtes dans la zone de maigreur.";
                MaigreurHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "MaigreurSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (imc < 16.5)
            {
                DenutritionHubTile.Title = "";
                DenutritionHubTile.Message = "Vous êtes dans la zone de dénutrition.";
                DenutritionHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "DenutritionSelected.png", UriKind.RelativeOrAbsolute));
            }
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
                changeBackHubTile(imc);
            }
            catch
            {
                metricIMCResultTextBlock.Text = "Un problème est survenu lors du calcul de votre IMC.\nVeuillez entrer à nouveau votre taille et votre poids.";
            }
        }


        // Gender management

        private void changeToWomen(object sender, System.Windows.RoutedEventArgs e)
        {
            isMale = false;
            menImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Resources/Images/men.png", UriKind.RelativeOrAbsolute));
            womenImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Resources/Images/womenSelected.png", UriKind.RelativeOrAbsolute));

            changeAllHubTilesToStartState();
        }

        private void changeToMen(object sender, System.Windows.RoutedEventArgs e)
        {
            isMale = true;
            menImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Resources/Images/menSelected.png", UriKind.RelativeOrAbsolute));
            womenImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Resources/Images/women.png", UriKind.RelativeOrAbsolute));

            changeAllHubTilesToStartState();
        }


        // Navigation logic

        private void displayDenutritionView(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            Uri uri = new Uri("/DenutritionPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayMaigreurView(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            Uri uri = new Uri("/MaigreurPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayNormalView(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            Uri uri = new Uri("/NormalPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displaySurpoidsView(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            Uri uri = new Uri("/SurpoidsPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayObesiteView(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            Uri uri = new Uri("/ObesitePage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
        
    }
}