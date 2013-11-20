using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using PanoramaIMCCalculator.ViewModels;


namespace PanoramaIMCCalculator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.BMIItemViewModel = new BMIItemViewModel();
        }

        /// <summary>
        /// BMI Item Property that describes all the BMI zones.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public BMIItemViewModel BMIItemViewModel { get; set; }

        /// <summary>
        /// Collection pour les objets ItemViewModel.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

       public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Crée et ajoute quelques objets ItemViewModel dans la collection Items.
        /// </summary>
        public void LoadData()
        {
            this.BMIItemViewModel.LoadDefaultData();

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}