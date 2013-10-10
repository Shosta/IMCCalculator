using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PanoramaIMCCalculator
{
    public class LocalizedStrings
    {
        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        public LocalizedStrings()
        { }

        private static PanoramaIMCCalculator.AppResources localizedResources = new PanoramaIMCCalculator.AppResources();

        public PanoramaIMCCalculator.AppResources LocalizedResources { get { return localizedResources; } }
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PanoramaIMCCalculator.UI.LocalizedStrings", typeof(LocalizedStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }


        /// <summary>
        ///   Looks up a localized string similar to "Calculer".
        /// </summary>
        public static string MainPage_CalculateIMC
        {
            get
            {
                return ResourceManager.GetString("CalculateIMCButtonText", resourceCulture);
            }
        }
    }

}
