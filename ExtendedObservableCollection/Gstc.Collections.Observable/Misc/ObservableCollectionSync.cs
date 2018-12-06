using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Gstc.Collections.Observable.misc {

    public class ObservableCollectionSync<T1, T2> : ObservableCollection<T2> where T1 : class where T2 : class{
        public ObservableCollection<T1> SourceCollection {
            get { return _sourceCollection; }
            set {
                _sourceCollection = value;
                OnNewCollection();
            }
        }

        private readonly Converter<T1, T2> _convert;
        private Converter<T2, T1> _convertBack;
        private ObservableCollection<T1> _sourceCollection;

        public ObservableCollectionSync(Converter<T1, T2> convert, Converter<T2, T1> convertBack, ObservableCollection<T1> sourceCollection = null) {           
            _convert = convert;
            _convertBack = convertBack;
            SourceCollection = sourceCollection ?? new ObservableCollection<T1>();            
        }

        private void OnNewCollection() {
            foreach (var item in SourceCollection) Add(_convert(item));
            SourceCollection.CollectionChanged += SourceCollectionOnCollectionChanged;
        }

        private void SourceCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) {
            int indexNew;
            int indexOld;
            switch (args.Action) {
                case NotifyCollectionChangedAction.Add:
                    indexNew = args.NewStartingIndex;
                    foreach (var item in args.NewItems) InsertItem(indexNew++,_convert(item as T1));                   
                    break;
                case NotifyCollectionChangedAction.Remove:
                    indexOld = args.OldStartingIndex;
                    foreach (var item in args.OldItems) RemoveAt(indexOld++);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    indexOld = args.OldStartingIndex;
                    indexNew = args.NewStartingIndex;
                    foreach (var item in args.OldItems) RemoveAt(indexOld++);                    
                    foreach (var item in args.NewItems) InsertItem(indexNew++, _convert(item as T1));
                    break;
                case NotifyCollectionChangedAction.Move:
                    indexOld = args.OldStartingIndex;
                    indexNew = args.NewStartingIndex;
                    foreach (var item in args.OldItems) MoveItem(indexOld++,indexNew++);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    foreach (var item in SourceCollection) Add(_convert(item));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

       
    }

}




