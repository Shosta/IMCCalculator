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
// File       : StringToColorConverters
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// Change a string to a color.
// It is used to display the color of the listBoxItems according to their type.
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PanoramaIMCCalculator.Converters
{

    #region BMI Type to Color converter

    public class BmiTypeToSolidColorBrushConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.FromArgb(255, 79, 195, 187);

            switch ((App.bmiItemType)value)
            {
                case App.bmiItemType.severeObesity:
                    color = Color.FromArgb(255, 237, 109, 60); // { R = 0xED, G = 0x6D, B = 0x3C };
                    break;

                case App.bmiItemType.obesity:
                    color = Color.FromArgb(255, 248, 151, 29); // { R = 0xF8, G = 0x97, B = 0x1D };
                    break;

                case App.bmiItemType.overweight:
                    color = Color.FromArgb(255, 255, 201, 15); //{ R = 0xFF, G = 0xC9, B = 0x0F };
                    break;

                case App.bmiItemType.normal:
                    color = Color.FromArgb(255, 79, 195, 187); // { R = 0x4F, G = 0xC3, B = 0xBB };
                    break;

                case App.bmiItemType.thinness:
                    color = Color.FromArgb(255, 254, 214, 81); // { R = 0xFE, G = 0xD6, B = 0x51 };
                    break;

                case App.bmiItemType.undernutrition:
                    color = Color.FromArgb(255, 237, 109, 60); // { R = 0xED, G = 0x6D, B = 0x3C };
                    break;

                default:
                    break;
            }
            SolidColorBrush colorBrush = new SolidColorBrush(color);

            return colorBrush;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    #endregion
}
