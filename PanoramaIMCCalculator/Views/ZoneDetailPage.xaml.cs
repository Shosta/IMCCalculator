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
using PanoramaIMCCalculator.Resources.AppResources;
using System.Collections.ObjectModel;
using PanoramaIMCCalculator.Managers;

namespace PanoramaIMCCalculator
{
    public partial class ZoneDetailPage : PhoneApplicationPage
    {

        #region Fields

        private const string typeParameter = "type";

        private const string _severeObesityUriParameter = "SevereObesity";
        private const string _obesityUriParameter = "Obesity";
        private const string _overweightUriParameter = "Overweight";
        private const string _normalUriParameter = "Normal";
        private const string _thinnessUriParameter = "Thinness";
        private const string _undernutritionUriParameter = "Undernutrition";

        #endregion

        #region Object

        public ZoneDetailPage()
        {
            InitializeComponent();

            this.DataContext = App.ViewModel.BMIItemViewModel;
        }

        #endregion

        #region Get Uri

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriParameter"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        /// <returns></returns>
        private static App.bmiItemType AppBmiItemTypeFromUriBmiParameter(string uriParameter)
        {
            App.bmiItemType bmiZoneType = App.bmiItemType.normal;
            switch (uriParameter)
            {
                case _severeObesityUriParameter:
                    bmiZoneType = App.bmiItemType.severeObesity;
                    break;

                case _obesityUriParameter:
                    bmiZoneType = App.bmiItemType.obesity;
                    break;

                case _overweightUriParameter:
                    bmiZoneType = App.bmiItemType.overweight;
                    break;

                case _normalUriParameter:
                    bmiZoneType = App.bmiItemType.normal;
                    break;

                case _thinnessUriParameter:
                    bmiZoneType = App.bmiItemType.thinness;
                    break;

                case _undernutritionUriParameter:
                    bmiZoneType = App.bmiItemType.undernutrition;
                    break;

                default:
                    break;
            }

            return bmiZoneType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        /// <returns></returns>
        private static string UriBmiParameterFromAppBmiItemType(App.bmiItemType type)
        {
            string bmiZoneTypeParameter = _normalUriParameter;
            switch (type)
            {
                case App.bmiItemType.severeObesity:
                    bmiZoneTypeParameter = _severeObesityUriParameter;
                    break;

                case App.bmiItemType.obesity:
                    bmiZoneTypeParameter = _obesityUriParameter;
                    break;

                case App.bmiItemType.overweight:
                    bmiZoneTypeParameter = _overweightUriParameter;
                    break;

                case App.bmiItemType.normal:
                    bmiZoneTypeParameter = _normalUriParameter;
                    break;

                case App.bmiItemType.thinness:
                    bmiZoneTypeParameter = _thinnessUriParameter;
                    break;

                case App.bmiItemType.undernutrition:
                    bmiZoneTypeParameter = _undernutritionUriParameter;
                    break;

                default:
                    break;
            }

            return bmiZoneTypeParameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        /// <returns></returns>
        public static Uri GetUri(App.bmiItemType type)
        {
            string bmiZoneTypeParameter = ZoneDetailPage.UriBmiParameterFromAppBmiItemType(type);

            string uri = "/Views/ZoneDetailPage.xaml";
            if (String.IsNullOrEmpty(bmiZoneTypeParameter) == false)
            {
                uri = uri + "?" + typeParameter + "=" + bmiZoneTypeParameter;
            }

            return new Uri(uri, UriKind.Relative);
        }

        #endregion

        #region Navigation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        /// <returns></returns>
        protected bool TryGetQueryString(string key, out string value)
        {
            string valueString;
            if (NavigationContext.QueryString.TryGetValue(key, out valueString))
            {
                value = valueString;
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            string type = null;
            if (TryGetQueryString(typeParameter, out type))
            {
                Console.WriteLine(type);
                var bmiZoneType = ZoneDetailPage.AppBmiItemTypeFromUriBmiParameter(type);

                ObservableCollection<BMIItem> observableBmiItemsCollection = App.ViewModel.BMIItemViewModel.BMIItems;
                var bmiItem = observableBmiItemsCollection.Where(i => i.Type == bmiZoneType).FirstOrDefault();
                if (bmiItem != null)
                {
                    this.DataContext = bmiItem;
                }
            }
        }

        #endregion

    }
}