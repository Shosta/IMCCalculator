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
// File       : BMIItem
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// The Object that represents a BMI zone, such as Obesity or Thinness.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PanoramaIMCCalculator.Managers
{
    public class BMIItem : INotifyPropertyChanged
    {

        #region Fields

        private string _title;
        private string _weightZone;
        private string _weightToChangeToMatchNormalZone;
        private bool _isUsed;
        private string _congratulations;
        private string _hubTileIcon;
        private string _hubTileMessage;
        private string _description;
        private string _urlToNavigate;
        private App.bmiItemType _type;

        #endregion

        #region Accessors

        /// <summary>
        /// The BMI zone title, such as Obesity or Thinness.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        /// <summary>
        /// The weight zone description, such as "over 35" for the Obesity BMI zone.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string WeightZone
        {
            get
            {
                return this._weightZone;
            }
            set
            {
                this._weightZone = value;
            }
        }

        /// <summary>
        /// The number of kg the user has to gain or loose in order to reenter the normal zone.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string WeightToChangeToMatchNormalZone
        {
            get
            {
                return this._weightToChangeToMatchNormalZone;
            }
            set
            {
                this._weightToChangeToMatchNormalZone = value;
            }
        }

        /// <summary>
        /// The boolean that indicates if this BMI Item is the one's user. If the user is in the Obesity BMI zone for instance.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public bool IsUsed
        {
            get
            {
                return this._isUsed;
            }
            set
            {
                this._isUsed = value;
                RaisePropertyChanged("IsUsed");
            }
        }

        /// <summary>
        /// The congratulations text which is displayed behind the HubTile on the third PivotItem to tell the user that he is in this particular BMI zone.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string Congratulations
        {
            get
            {
                return this._congratulations;
            }

            set
            {
                this._congratulations = value;
            }
        }

        /// <summary>
        /// Represents the icon displayed in the HubTile.
        /// This string is changed to an image through a converter <see cref="MessageIconNameToImageConverter"/>.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        /// <remarks>It has 6 values which are : 
        /// . undernutrition
        /// . thinness
        /// . normal
        /// . overweight
        /// . obesity
        /// . severeobesity
        /// </remarks>
        public string HubTileIcon
        {
            get
            {
                return this._hubTileIcon;
            }

            set
            {
                this._hubTileIcon = value;
            }
        }

        /// <summary>
        /// The message that appears on the Back of the HubTile on the WrapPanel.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string HubTileMessage
        {
            get
            {
                return this._hubTileMessage;
            }

            set
            {
                this._hubTileMessage = value;
            }
        }

        /// <summary>
        /// The BMI zone description text which is displayed in the BMI detail Page.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string Description
        {
            get
            {
                return this._description;
            }

            set
            {
                this._description = value;
            }
        }

        /// <summary>
        /// The url to navigate to the detail Page for this BMI item.
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public string UrlToNavigate
        {
            get
            {
                return this._urlToNavigate;
            }

            set
            {
                this._urlToNavigate = value;
            }
        }

        /// <summary>
        /// The BMiItem type it is one type of the enum <see cref="bmiItemType"/>
        /// </summary>
        /// <author>Rémi Lavedrine</author>
        /// <date>14/11/2013</date>
        public App.bmiItemType Type
        {
            get
            {
                return this._type;
            }

            set
            {
                this._type = value;
            }
        }

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        #endregion

    }
}
