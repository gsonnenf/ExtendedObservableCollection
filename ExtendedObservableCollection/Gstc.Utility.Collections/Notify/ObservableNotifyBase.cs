using System;
using System.ComponentModel;

namespace Gstc.Collections.Observable.Base {
    public abstract class ObservableNotifyBase : INotifyPropertyChanged {
        protected const string CountString = "Count";
        protected const string IndexerName = "Item[]";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        protected void OnPropertyChanged(string propertyName) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChangedIndexerCount() {
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
        }

        protected void OnPropertyChangedIndexer() {
            OnPropertyChanged(IndexerName);
        }

        #region Reentrancy
        private readonly SimpleMonitor _monitor = new SimpleMonitor();


        protected IDisposable BlockReentrancy() {
            _monitor.Enter();
            return _monitor;
        }

        //TODO: Add Monitor for Collection Changed and Dictionary Changed

        protected void CheckReentrancy() {
            if (!_monitor.Busy) return;
            //if ((CollectionChanged == null) || (CollectionChanged.GetInvocationList().Length <= 1)) return;
            //throw new InvalidOperationException("ObservableCollectionReentrancyNotAllowed");
        }

        private class SimpleMonitor : IDisposable {
            int _busyCount;
            public void Enter() => ++_busyCount;
            public void Dispose() => --_busyCount;
            public bool Busy => _busyCount > 0;
        }

        #endregion
    }
}
