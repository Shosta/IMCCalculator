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
// File       : BMIItemViewModel
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// 
//

using PanoramaIMCCalculator.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PanoramaIMCCalculator.Resources.AppResources;

namespace PanoramaIMCCalculator.ViewModels
{
    public class BMIItemViewModel
    {

        #region Object

        public BMIItemViewModel()
        {
            if (_bmiItems == null)
            {
                LoadDefaultData();
            }
        }

        /// <summary>
        /// Create the Observable Collection that contains all the information to bind on the Page.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public void LoadDefaultData()
        {
            _bmiItems = new ObservableCollection<BMIItem>() {
                // The Severe Obesity BMIItem.
                new BMIItem(){Title = AppResources.StackPanelObesiteSevereTitle, 
                    WeightZone = AppResources.StackPanelObesiteSevereZoneText, 
                    WeightToChangeToMatchNormalZone = AppResources.StackPanelObesiteSevereZoneInfoText,
                    IsUsed = false,
                    Congratulations = AppResources.ObesiteSevereHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.ObesiteSevereHubTileMessage,
                    Description = AppResources.ObesiteSevereDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.severeObesity},

                // The Obesity BMIItem.
                new BMIItem(){Title = AppResources.StackPanelObesiteTitle, 
                    WeightZone = AppResources.StackPanelObesiteZoneText, 
                    WeightToChangeToMatchNormalZone = AppResources.StackPanelObesiteZoneInfoText,
                    IsUsed = false,
                    Congratulations = AppResources.ObesiteHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.ObesiteHubTileMessage,
                    Description = AppResources.ObesiteDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.obesity},

                    // The Overweight BMIItem.
                new BMIItem(){Title = AppResources.StackPanelSurpoidsTitle, 
                    WeightZone = AppResources.StackPanelSurpoidsZoneText, 
                    WeightToChangeToMatchNormalZone = AppResources.StackPanelSurpoidsZoneInfoText,
                    IsUsed = false,
                    Congratulations = AppResources.SurpoidsHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.SurpoidsHubTileMessage,
                    Description = AppResources.SurpoidsDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.overweight},

                    // The Normal BMIItem.
                new BMIItem(){Title = AppResources.StackPanelNormalTitle, 
                    WeightZone = AppResources.StackPanelNormalZoneText, 
                    WeightToChangeToMatchNormalZone = String.Empty,
                    IsUsed = false,
                    Congratulations = AppResources.NormalHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.NormalHubTileMessage,
                    Description = AppResources.NormalDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.normal},

                    // The Thinness BMIItem.
                new BMIItem(){Title = AppResources.StackPanelMaigreurTitle, 
                    WeightZone = AppResources.StackPanelMaigreurZoneText, 
                    WeightToChangeToMatchNormalZone = AppResources.StackPanelMaigreurZoneInfoText,
                    IsUsed = false,
                    Congratulations = AppResources.MaigreurHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.MaigreurHubTileMessage,
                    Description = AppResources.MaigreurDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.thinness},

                    // The Undernutrition BMIItem.
                 new BMIItem(){Title = AppResources.StackPanelDenutritionTitle, 
                    WeightZone = AppResources.StackPanelDenutritionZoneText, 
                    WeightToChangeToMatchNormalZone = AppResources.StackPanelDenutritionZoneInfoText,
                    IsUsed = false,
                    Congratulations = AppResources.DenutritionHubTileMessage,
                    HubTileIcon = String.Empty,
                    HubTileMessage = AppResources.DenutritionHubTileMessage,
                    Description = AppResources.DenutritionDetailPageContent,
                    UrlToNavigate = String.Empty,
                    Type = App.bmiItemType.undernutrition},
                    };
        }

        #endregion

        #region MessageItems Business Logic

        private ObservableCollection<BMIItem> _bmiItems;
        public ObservableCollection<BMIItem> BMIItems
        {
            get
            {
                return _bmiItems;
            }
        }

        #endregion
    }
}
