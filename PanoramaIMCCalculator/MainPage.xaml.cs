using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PanoramaIMCCalculator.Managers;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Windows.Media;
using PanoramaIMCCalculator.Resources.AppResources;
using System.Collections.ObjectModel;
using System.Linq;

namespace PanoramaIMCCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {

        #region Object

        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            // Affecter l'exemple de données au contexte de données du contrôle ListBox
            DataContext = App.ViewModel.BMIItemViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            CompletePageInitialization();
        }

        /// <summary>
        /// Overrides PhoneApplicationPage.OnNavigatedTo().
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.CompletePageInitialization();
        }

        /// <summary>
        /// Rebuild the AppplicationBar when the user navigate to this page.
        /// </summary>
        private void CompletePageInitialization()
        {
            RebuildAppBar();
        }

        #endregion

        #region MainPage Model

        // Charger les données pour les éléments ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        #endregion

        #region Placeholder on Height and Weight TextBox

        /// <summary>
        /// Fired when the Height or Weight textBo got the Focus. It removes the text to simulate a placeholder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        private void textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Text = "";
        }

        /// <summary>
        /// Fired when the Height or Weight textBo got the Focus. It replace the text to the default one if it's empty to simulate a placeholder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        private void textBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                if (tb == metricHeightTextBox)
                    metricHeightTextBox.Text = AppResources.metricHeightTextBoxText;

                if (tb == metricWeightTextBox)
                    metricWeightTextBox.Text = AppResources.metricWeightTextBoxText;
            }
        }

        #endregion

        #region AppBar Building

         /// <summary>
         /// 
         /// </summary>
        private void RebuildAppBar()
        {
            this.ApplicationBar.Buttons.Clear();
            this.ApplicationBar.MenuItems.Clear();

            switch (MainPivot.SelectedIndex)
            {
                case -1:
                case 0:
                    ApplicationBar.Buttons.Add(CalculateBMIAppBarButton);
                    ApplicationBar.Mode = ApplicationBarMode.Default;
                    break;

                default:
                    ApplicationBar.Mode = ApplicationBarMode.Minimized;
                    break;
            }

            // Create a new menu item with the localized string from AppResources.
            ApplicationBar.MenuItems.Add(AboutAppBarMenuItem);
        }

        /// <summary>
        /// 
        /// </summary>
        private IApplicationBarIconButton _calculateBMIAppBarButton = null;
        /// <summary>
        /// 
        /// </summary>
        private IApplicationBarIconButton CalculateBMIAppBarButton
        {
            get
            {
                if (this._calculateBMIAppBarButton != null)
                    return this._calculateBMIAppBarButton;

                this._calculateBMIAppBarButton = new ApplicationBarIconButton
                {
                    IconUri = new Uri("/Resources/Images/AppBarIcons/scale.png", UriKind.Relative),
                    Text = AppResources.CalculateIMCButtonText,
                };
                this._calculateBMIAppBarButton.Click += this.OnCalculateBMIAppBarButtonOnClick;
                return this._calculateBMIAppBarButton;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IApplicationBarMenuItem _aboutAppBarMenuItem = null;

        /// <summary>
        /// 
        /// </summary>
        private IApplicationBarMenuItem AboutAppBarMenuItem
        {
            get
            {
                if (this._aboutAppBarMenuItem != null)
                    return this._aboutAppBarMenuItem;

                this._aboutAppBarMenuItem = new ApplicationBarMenuItem
                {
                    Text = AppResources.AProposText
                };
                this._aboutAppBarMenuItem.Click += this.displayAboutView;
                return this._aboutAppBarMenuItem;
            }
        }

        #endregion

        #region AppBar Actions

        /// <summary>
        /// Navigate to the About Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        private void displayAboutView(object sender, EventArgs e)
        {
            Uri uri = new Uri("/PanoramaIMCCalculator;component/Views/AboutPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void OnCalculateBMIAppBarButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.calculateImcAndUpdateUI();

            this.Focus();
        }

        /// <summary>
        /// Calculate the BMI and Update the MainPageUI.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        private void calculateImcAndUpdateUI()
        {
            try
            {
                string s_height = this.metricHeightTextBox.Text;
                if (s_height.Contains("."))
                    s_height = s_height.Substring(0, s_height.IndexOf("."));

                string s_weight = metricWeightTextBox.Text;
                if (s_weight.Contains("."))
                    s_weight = s_weight.Substring(0, s_weight.IndexOf("."));

                Double height = Double.Parse(s_height);
                Double weight = Double.Parse(s_weight);
                Double imc = calculateMetricIMC(height, weight);

                displayIMCinfo(imc);

                //defineFeedbackHeightandWeightTextBlocks(metricHeightTextBox.Text + " cm", metricWeightTextBox.Text + " kg");

                // Manage the Tile according to the imc result.
                TileManager tileManager = new TileManager();
                IMCCalculatorManager imcManager = new IMCCalculatorManager();
                tileManager.changeBackTile(imc, height, weight, imcManager.infoFromIMC(imc));
                //changeBackHubTile(imc);
            }
            catch
            {
                metricIMCResultTextBlock.Text = AppResources.IMCCalculationError;
            }
        }

        #endregion

        #region IMC Formula

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

            String s_imc = Math.Round(imc, 2).ToString("R"); // On prend toutes les valeurs de décimal. Si on ne les veut pas, il suffit de mettre F0 à la place de R.
            int nbDecimal = s_imc.Length - s_imc.IndexOf(",");

            s_imc = AppResources.IMCResultTextFirstPart + " " + s_imc + AppResources.IMCResultTextSecondPart + "\n" + manager.infoFromIMC(imc);

            if (imc < 18.5)
            {
                //displayGainWeightTextBlockFromIMC(imc, height, weight);
                s_imc = s_imc + "\n" + manager.weightToGainFromIMC(imc, height, weight);
            }
            else if (imc > 25)
            {
                //displayLoseWeightTextBlockFromIMC(imc, height, weight);
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

        #endregion

        #region Summary Page display info

        /// <summary>
        /// Display the correct info according to the current imc on the stack panel.
        /// </summary>
        /// <param name="imc">The current IMC.</param>
        private void displayIMCinfo(double imc)
        {
            var type = App.bmiItemType.normal;

            if (imc > 35)
            {
                type = App.bmiItemType.severeObesity;
            }
            else if (30 < imc && imc <= 35)
            {
                type = App.bmiItemType.obesity;
            }
            else if (25 < imc && imc <= 30)
            {
                type = App.bmiItemType.overweight;
            }
            else if (16.5 <= imc && imc < 18.5)
            {
                type = App.bmiItemType.thinness;
            }
            else if (imc < 16.5)
            {
                type = App.bmiItemType.undernutrition;
            }

            ObservableCollection<BMIItem> observableBmiItemsCollection = App.ViewModel.BMIItemViewModel.BMIItems;
            var bmiItem = observableBmiItemsCollection.Where(i => i.Type == type).FirstOrDefault();
            if (bmiItem != null)
            {
                bmiItem.IsUsed = true;
            }
            var bmiItems = observableBmiItemsCollection.Where(i => i.Type != type).ToList();
            foreach (var item in bmiItems)
            {
                item.IsUsed = false;
            } 
        }

        #endregion

        #region TextBox & Keyboard Management

        /// <summary>
        /// Fired everytime the user press the keyboard.
        /// If the user press the "Enter" key for the metricHeightTextBox then focus the weight text box to allow the user to enter his weight.
        /// If the user press the "Enter" key for the metricWeightTextBox then focus the page to hide the keyboard and let the user press the "Calculate" button.
        /// </summary>
        /// <param name="sender">The object responsible for firing this event</param>
        /// <param name="e">The event fired.</param>
        /// <author>Rémi Lavedrine</author>
        /// <date>13/11/2013</date>
        private void metricHeightTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if (sender == this.metricWeightTextBox)
                    {
                        // Focus the page to hide the keyboard and let the user press the "Activate" button.
                        this.Focus();
                    }
                    else if (sender == this.metricHeightTextBox)
                    {
                        // Focus the confirmation text box to allow the user to enter his email address once more.
                        this.metricWeightTextBox.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Detail Page Navigation

        /// <summary>
        /// Navigate to the Detail Page. It passes only the bmi type to display the correct information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        private void OnClick_NavigateToDetailPage(object sender, RoutedEventArgs e)
        {
            // TODO 
            DependencyObject tappedElement = e.OriginalSource as UIElement;
            var tappedItem = this.FindParentOfType<ListBoxItem>(tappedElement);

            if (tappedItem != null)
            {
                // DataContext contains reference to data item
                BMIItem selectedBmiZone = tappedItem.DataContext as BMIItem;
                string detailPageUri = selectedBmiZone.UrlToNavigate;

                NavigationService.Navigate(ZoneDetailPage.GetUri(selectedBmiZone.Type));
            }

        }

        private T FindParentOfType<T>(DependencyObject element) where T : DependencyObject
        {
            T result = null;
            DependencyObject currentElement = element;
            while (currentElement != null)
            {
                result = currentElement as T;
                if (result != null)
                {
                    break;
                }
                currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            return result;
        }

        #endregion

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RebuildAppBar();
        }


        /*
        #region Summary Page display info

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
            else if (30 < imc && imc <= 35)
            {
                ModerateObesityZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (25 < imc && imc <= 30)
            {
                SurpoidsZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (16.5 <= imc && imc < 18.5)
            {
                MaigreurZoneTextBlock.Visibility = Visibility.Visible;
            }
            else if (imc < 16.5)
            {
                DenutritionZoneTextBlock.Visibility = Visibility.Visible;
            }

        }

        #endregion

        #region IMC Formula

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

            s_imc = AppResources.IMCResultTextFirstPart + s_imc + AppResources.IMCResultTextSecondPart + "\n" + manager.infoFromIMC(imc);

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

        #endregion

        #region User Interface management

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
                if (tb == metricHeightTextBox)
                    metricHeightTextBox.Text = AppResources.metricHeightTextBoxText;

                if (tb == metricWeightTextBox)
                    metricWeightTextBox.Text = AppResources.metricWeightTextBoxText;
            }

            mainViewScrollViewer.Height = 498;
        }

        const int kCalculatePanoramaItem = 0;
        const int kSummaryPanoramaItem = 1;
        const int kInfoPanoramaItem = 2;
        private void PanoramaItemGotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            Console.WriteLine("Got Focus"); 
            //ApplicationBar = ((ApplicationBar)Application.Current.Resources["CalculateBMIAppBar"]);    
        }

        private void PanoramaItem_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            //ApplicationBar = ((ApplicationBar)Application.Current.Resources["HiddenApplicationBar"]);
        }

        #endregion

        #region Calculate Button Action Management

        // Calculate Button action management
        private void defineFeedbackHeightandWeightTextBlocks(string height, string weight)
        {
            feedbackHeightTextBlock.Text = height;
            feedbackWeightTextBlock.Text = weight;
        }

        #endregion

        #region Hub Tiles

        private void changeAllHubTilesToStartState()
        {
            ObesiteSevereHubTile.Title = AppResources.ObesiteSevereHubTileTitle;
            ObesiteHubTile.Title = AppResources.ObesiteHubTileTitle;
            SurpoidsHubTile.Title = AppResources.SurpoidsHubTileTitle;
            NormalHubTile.Title = AppResources.NormalHubTileTitle;
            MaigreurHubTile.Title = AppResources.MaigreurHubTileTitle;
            DenutritionHubTile.Title = AppResources.DenutritionHubTileTitle;

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
                ObesiteSevereHubTile.Message = AppResources.ObesiteSevereHubTileMessage;
                ObesiteSevereHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "ObeseSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (30 < imc && imc <= 35)
            {
                ObesiteHubTile.Title = "";
                ObesiteHubTile.Message = AppResources.ObesiteHubTileMessage;
                ObesiteHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "ObeseSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (25 < imc && imc <= 30)
            {
                SurpoidsHubTile.Title = "";
                SurpoidsHubTile.Message = AppResources.SurpoidsHubTileMessage;
                SurpoidsHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "SurpoidsSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if ( 18.5 <= imc && imc <= 25)
            {
                NormalHubTile.Title = "";
                NormalHubTile.Message = AppResources.NormalHubTileMessage;
                NormalHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "NormalSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (16.5 <= imc && imc < 18.5)
            {
                MaigreurHubTile.Title = "";
                MaigreurHubTile.Message = AppResources.MaigreurHubTileMessage;
                MaigreurHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "MaigreurSelected.png", UriKind.RelativeOrAbsolute));
            }
            else if (imc < 16.5)
            {
                DenutritionHubTile.Title = "";
                DenutritionHubTile.Message = AppResources.DenutritionHubTileMessage;
                DenutritionHubTile.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/HubTileImages/" + gender + "DenutritionSelected.png", UriKind.RelativeOrAbsolute));
            }
        }

        #endregion

        #region Gender Management

        // Gender management

        private string getGender()
        {
            string gender = "Homme";
            if (!isMale)
            {
                gender = "Femme";
            }

            return gender;
        }

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

        #endregion

        #region Button Action Management

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
                Double imc = calculateMetricIMC(height, weight);

                defineFeedbackHeightandWeightTextBlocks(metricHeightTextBox.Text + " cm", metricWeightTextBox.Text + " kg");

                // Manage the Tile according to the imc result.
                TileManager tileManager = new TileManager();
                IMCCalculatorManager imcManager = new IMCCalculatorManager();
                tileManager.changeBackTile(imc, height, weight, imcManager.infoFromIMC(imc));
                changeBackHubTile(imc);
            }
            catch
            {
                metricIMCResultTextBlock.Text = AppResources.IMCCalculationError;
            }
        }

#endregion

        #region AppBar Action Management

        private void OnCalculateBMIAppBarButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.calculateImcAndUpdateUI();
        }

        /// <summary>
        /// Calculate the BMI and Update the MainPageUI.
        /// </summary>
        private void calculateImcAndUpdateUI()
        {
            // TODO: Add event handler implementation here.
            try
            {
                string s_height = this.metricHeightTextBox.Text;
                if (s_height.Contains("."))
                    s_height = s_height.Substring(0, s_height.IndexOf("."));

                string s_weight = metricWeightTextBox.Text;
                if (s_weight.Contains("."))
                    s_weight = s_weight.Substring(0, s_weight.IndexOf("."));

                Double height = Double.Parse(s_height);
                Double weight = Double.Parse(s_weight);
                Double imc = calculateMetricIMC(height, weight);


                defineFeedbackHeightandWeightTextBlocks(metricHeightTextBox.Text + " cm", metricWeightTextBox.Text + " kg");

                // Manage the Tile according to the imc result.
                TileManager tileManager = new TileManager();
                IMCCalculatorManager imcManager = new IMCCalculatorManager();
                tileManager.changeBackTile(imc, height, weight, imcManager.infoFromIMC(imc));
                changeBackHubTile(imc);
            }
            catch
            {
                metricIMCResultTextBlock.Text = AppResources.IMCCalculationError;
            }
        }

        #endregion

        #region Navigation Logic

        // Navigation logic

        private void displayDenutritionView(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO : ajoutez ici l’implémentation du gestionnaire d’événements.
            string gender = "Homme";
            if (!isMale)
                gender = "Femme";

            Uri uri = new Uri("/ZoneDetailPage.xaml?type=Denutrition&gender=" + gender, UriKind.Relative);
            // Uri uri = new Uri("/DenutritionPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayMaigreurView(object sender, System.Windows.RoutedEventArgs e)
        {
        	string gender = "Homme";
            if (!isMale)
                gender = "Femme";

            Uri uri = new Uri("/ZoneDetailPage.xaml?type=Maigreur&gender=" + gender, UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayNormalView(object sender, System.Windows.RoutedEventArgs e)
        {
            string gender = "Homme";
            if (!isMale)
                gender = "Femme";

            Uri uri = new Uri("/ZoneDetailPage.xaml?type=Normal&gender=" + gender, UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displaySurpoidsView(object sender, System.Windows.RoutedEventArgs e)
        {
            string gender = "Homme";
            if (!isMale)
                gender = "Femme";

            Uri uri = new Uri("/ZoneDetailPage.xaml?type=Surpoids&gender=" + gender, UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayObesiteView(object sender, System.Windows.RoutedEventArgs e)
        {
            string gender = "Homme";
            if (!isMale)
                gender = "Femme";

            Uri uri = new Uri("/ZoneDetailPage.xaml?type=Obese&gender=" + gender, UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void displayAboutView(object sender, EventArgs e)
        {
            Uri uri = new Uri("/PanoramaIMCCalculator;component/AboutPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void MainPanorama_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	this.RebuildAppBar();
        }
        
        #endregion
*/

    }
}