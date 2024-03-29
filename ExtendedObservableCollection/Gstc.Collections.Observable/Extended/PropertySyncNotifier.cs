﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Gstc.Collections.Observable.Extended {
    public class PropertySyncNotifier {

        public List<PropertyChangedEventArgs> LastArgs = new List<PropertyChangedEventArgs>();
        public INotifyPropertySyncChanged SourceSync { get; set; }
        public INotifyPropertySyncChanged DestSync { get; set; }

        public INotifyPropertyChanged SourceNotify { get; set; }
        public INotifyPropertyChanged DestNotify { get; set; }
        public PropertySyncNotifier(INotifyPropertyChanged sourceItem, INotifyPropertyChanged destItem) {
            SourceNotify = sourceItem;
            DestNotify = destItem;

            SourceSync = sourceItem as INotifyPropertySyncChanged;
            DestSync = destItem as INotifyPropertySyncChanged;

            if (sourceItem == null && destItem == null) throw new System.ArgumentException("One of the objects must implement INotifyPropertySyncChanged."); ;

            if (DestSync != null) SourceNotify.PropertyChanged += DestTrigger;
            if (SourceSync != null) DestNotify.PropertyChanged += SourceTrigger;
        }

        public void DestTrigger(object sender, PropertyChangedEventArgs args) {
            if (LastArgs.Contains(args)) { LastArgs.Remove(args); return; } //Allows concurrant execution.
            LastArgs.Add(args);
            DestSync.OnPropertyChanged(sender, args);
        }

        public void SourceTrigger(object sender, PropertyChangedEventArgs args) {
            if (LastArgs.Contains(args)) { LastArgs.Remove(args); return; } //Allows concurrant execution.
            LastArgs.Add(args);
            SourceSync.OnPropertyChanged(sender, args);
        }

        /*
        private int updateCount = 0;

        public void DestTrigger(object sender, PropertyChangedEventArgs args) {
            if (updateCount++ == 3) { updateCount = 0; return; }
            DestSync.OnPropertyChanged(sender,args);          
        }

        public void SourceTrigger(object sender, PropertyChangedEventArgs args) {
            if (updateCount++ == 3) { updateCount = 0; return; }
            SourceSync.OnPropertyChanged(sender, args); 
        }
        */
    }
}
