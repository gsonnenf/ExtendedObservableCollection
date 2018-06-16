/*
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace Gstc.Collections.Observable {
    public abstract class CollectionViewAdapter<T1, T2> : ICollectionView where T1 : class where T2 : class {

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event CurrentChangingEventHandler CurrentChanging;
        public event EventHandler CurrentChanged;

        private ICollectionView DefaultCollectionView;

        protected CollectionViewAdapter(ObservableCollection<T1> sourceCollection ) {
            //SourceCollection = sourceCollection;
            //ObservableCollection = sourceCollection;
            //DefaultCollectionView = new CollectionView(sourceCollection);          
        }

        protected CollectionViewAdapter(ICollectionView collectionView) {

            //SourceCollection = sourceCollection;
            //ObservableCollection = sourceCollection;
            DefaultCollectionView = collectionView;
            DefaultCollectionView.CurrentChanged += CurrentChanged;
            DefaultCollectionView.CollectionChanged += CollectionChanged;
            DefaultCollectionView.CurrentChanging += CurrentChanging;
        }

        public IEnumerable Collection { get; set; } 

       // private ObservableCollection<T1> ObservableCollection { get; set; }
        public IEnumerable SourceCollection => DefaultCollectionView.SourceCollection;
        public IEnumerator GetEnumerator() => DefaultCollectionView.GetEnumerator();//new EnumeratorAdapter<T1, T2>(SourceCollection.GetEnumerator(), Convert);

        public abstract T2 Convert(T1 item);
        public abstract T1 Convert(T2 item);

        #region Passthrough mappings

        public CultureInfo Culture {
            get { return DefaultCollectionView.Culture; }
            set { DefaultCollectionView.Culture = value; }
        }

        public Predicate<object> Filter {
            get { return DefaultCollectionView.Filter; }
            set { DefaultCollectionView.Filter = value; }
        }


        public bool CanFilter => DefaultCollectionView.CanFilter;
        public SortDescriptionCollection SortDescriptions => DefaultCollectionView.SortDescriptions;
        public bool CanSort => DefaultCollectionView.CanSort;
        public bool CanGroup => DefaultCollectionView.CanSort;
        public ObservableCollection<GroupDescription> GroupDescriptions => DefaultCollectionView.GroupDescriptions;
        public ReadOnlyObservableCollection<object> Groups => DefaultCollectionView.Groups;
        public bool IsEmpty => DefaultCollectionView.IsEmpty;
        public object CurrentItem => DefaultCollectionView.CurrentItem;
        public int CurrentPosition => DefaultCollectionView.CurrentPosition;
        public bool IsCurrentAfterLast => DefaultCollectionView.IsCurrentAfterLast;
        public bool IsCurrentBeforeFirst => DefaultCollectionView.IsCurrentBeforeFirst;       
        public bool Contains(object item) => DefaultCollectionView.Contains(item);
        public void Refresh() => DefaultCollectionView.Refresh();
        public IDisposable DeferRefresh() => DefaultCollectionView.DeferRefresh();
        public bool MoveCurrentToFirst() => DefaultCollectionView.MoveCurrentToFirst();
        public bool MoveCurrentToLast() => DefaultCollectionView.MoveCurrentToLast();
        public bool MoveCurrentToNext() => DefaultCollectionView.MoveCurrentToNext();
        public bool MoveCurrentToPrevious() => DefaultCollectionView.MoveCurrentToPrevious();
        public bool MoveCurrentTo(object item) => DefaultCollectionView.MoveCurrentTo(item);
        public bool MoveCurrentToPosition(int position) => DefaultCollectionView.MoveCurrentToPosition(position);

        #endregion

    }
}
*/