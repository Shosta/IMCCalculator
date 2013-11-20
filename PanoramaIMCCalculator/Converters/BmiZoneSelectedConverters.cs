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
// File       : BmiZoneSelectedConverters.cs
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// This class converts the value of the given parameter to a style to hide or display some values according to the Bmi zone in which the user is courrently in.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PanoramaIMCCalculator.Converters
{
    #region Unread/Read Messages TextBlock Styles Converter

    public class IsUserInBmiZoneStyleConverter : IValueConverter
    {
        public Style IsInBmiZone { get; set; }
        public Style IsNotInBmiZone { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isUsed = (bool)value;

            return isUsed ? IsInBmiZone : IsNotInBmiZone;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
