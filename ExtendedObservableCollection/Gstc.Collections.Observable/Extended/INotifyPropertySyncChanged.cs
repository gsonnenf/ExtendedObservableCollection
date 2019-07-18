using System.ComponentModel;

namespace Gstc.Collections.Observable.Extended {
    public interface INotifyPropertySyncChanged : INotifyPropertyChanged {
        void OnPropertyChanged(object sender, PropertyChangedEventArgs args);
    }
}
