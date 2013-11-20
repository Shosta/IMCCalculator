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
// File       : ViewModelBase
// Created    : 14/11/2013
// Maintainer : Rémi Lavedrine
// 
// Brief
// 
//

using System;
using System.ComponentModel;

namespace PanoramaIMCCalculator.ViewModels
{
    public enum CurrentState
    {
        Loading = 0,
        EndLoading,
        Error
    };

    public class StateChangedEventArgs : EventArgs
    {
        private CurrentState _state;

        public StateChangedEventArgs(CurrentState state)
        {
            this._state = state;
        }

        public CurrentState State
        {
            get { return _state; }
        }
    }

    public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);

    public class ViewModelBase : INotifyPropertyChanged
    {
        private string _refreshTime;
        public string RefreshTime
        {
            get { return _refreshTime; }
            set
            {
                if (value != _refreshTime)
                {
                    _refreshTime = value;
                    NotifyPropertyChanged("RefreshTime");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event StateChangedEventHandler StateChanged;
        protected virtual void NotifyStateChanged(CurrentState state)
        {
            StateChangedEventHandler handler = StateChanged;
            if (null != handler)
            {
                handler(this, new StateChangedEventArgs(state));
            }
        }
    }
}


