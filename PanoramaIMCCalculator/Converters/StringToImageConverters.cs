// -------------------------------  Copyright ---------------------------------
//            This software has been developped by France Telecom, FT/OLNC/RD/MAPS/DVC/MSF
//
//            Copyright (c) France Telecom 2013
//
// COPYRIGHT    : This file is the property of FRANCE TELECOM.
//                It cannot be copied, used, or modified without obtaining
//                an authorization from the authors or a mandated
//                member of FRANCE TELECOM.
//                If such an authorization is provided, any modified version or
//                copy of the software has to contain this header.
//
// WARRANTIES   : This software is made available by the authors in the  hope
//                that it will be useful, but without any warranty.
//                France Telecom is not liable for any consequence related to the
//                use of the provided software.
//
// AUTHORS      : France Telecom  / OLNC / RD / MAPS / DVC / MSF
// ----------------------------------------------------------------------------
// Project    : MyMobistar
// File       : StringToImageConverters
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// 
//

using PanoramaIMCCalculator.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PanoramaIMCCalculator.Converters
{
    #region Messages Images Styles Converter

    public class BmiItemToImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BMIItem b = value as BMIItem;

            string selected = "";
            if (b.IsUsed)
            {
                selected = "Selected";
            }

            string gender = App.isMale ? "Homme": "Femme";

            BitmapImage img = new BitmapImage();
            img.CreateOptions = BitmapCreateOptions.BackgroundCreation;
            String icon = "Normal";
            switch (b.Type)
            {
                case App.bmiItemType.severeObesity:
                    icon = "Obese";
                    break;

                case App.bmiItemType.obesity:
                    icon = "Obese";
                    break;

                case App.bmiItemType.overweight:
                    icon = "Surpoids";
                    break;

                case App.bmiItemType.normal:
                    icon = "NormalSelected";
                    break;

                case App.bmiItemType.thinness:
                    icon = "Maigreur";
                    break;

                case App.bmiItemType.undernutrition:
                    icon = "Denutrition";
                    break;

                default:
                    break;
            }

            string imageUrl = "../Resources/Images/HubTileImages/" + gender + icon + selected  + ".png";
            Uri uri = new Uri(imageUrl, UriKind.Relative);
            img = new BitmapImage(uri);
            return img;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    #endregion
}
