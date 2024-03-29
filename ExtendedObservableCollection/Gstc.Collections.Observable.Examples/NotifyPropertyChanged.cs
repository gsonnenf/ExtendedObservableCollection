﻿using Gstc.Collections.Observable.Extended;
using System.ComponentModel;

namespace Gstc.Collection.Observable.Examples {
    public abstract class NotifyPropertySyncChanged : INotifyPropertySyncChanged, INotifyPropertyChanged {

        // Notify Property Changed Fields
        public event PropertyChangedEventHandler PropertyChanged;

        //Sync Fields 
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);

        //Convience Fields
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
