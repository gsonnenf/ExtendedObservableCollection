﻿using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {
    /// <summary>
    /// The Observable list adapter is a one-way synchronizer between a source observable list of type
    /// <TInput> and a destination observable list of <TOutput>. The method Convert(...) must be implemented
    /// as an adapter between the source element and destination element. 
    /// 
    /// This class is intended to serve as a one-way map between a collection of models and a collection of
    /// ViewModels though can be used for arbitrary purpose. It is recommened the destination element should
    /// contain a reference to the source element and propogate changes to the destination element via
    /// INotifyPropertyChanged or direct mapping to the source. 

    /// </summary>
    /// <typeparam name="TInput">Source element type (e.g. Model class)</typeparam>
    /// <typeparam name="TOutput">Destination element type (e.g. ViewModel class</typeparam>
    public abstract class ObservableListAdapter<TInput, TOutput> : ObservableList<TOutput> {

        //Conversion method for 
        public abstract TOutput Convert(TInput item);

        public abstract TInput Convert(TOutput item);

        private IObservableCollection<TInput> _sourceCollection;

        protected ObservableListAdapter() { }

        /// <summary>
        /// Initializes a ObservableListAdapter with a collection implementing IObservable collection.
        /// </summary>
        /// <param name="sourceCollection"></param>
        protected ObservableListAdapter(IObservableCollection<TInput> sourceCollection) {
            if (sourceCollection == null) throw new ArgumentNullException("BaseCollection can not be null");
            SourceCollection = sourceCollection;
        }

        public IObservableCollection<TInput> SourceCollection {
            get => _sourceCollection;
            set {
                if (_sourceCollection != null) _sourceCollection.CollectionChanged -= SourceCollectionChanged;
                _sourceCollection = value;
                if (_sourceCollection == null) return;

                SourceCollection.CollectionChanged += SourceCollectionChanged;

                Clear();
                foreach (var element in _sourceCollection) {
                    var item = Convert(element);
                    Add(item);
                    Notifier(item);
                }
            }
        }

        public void Notifier(TOutput item) {
            var notifyPropertyChanged = item as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged +=
                    (sender, args) => {
                        OnCollectionReplace(item, item, IndexOf(item));
                        Console.WriteLine(args.PropertyName);
                    };
        }

        public void SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) {
            switch (args.Action) {
                case NotifyCollectionChangedAction.Add:
                    for (var i = 0; i < args.NewItems.Count; i++) {
                        var item = Convert((TInput) args.NewItems[i]);
                        Insert(args.NewStartingIndex + i, item);
                        Notifier(item);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (var i = 0; i < args.OldItems.Count; i++) RemoveAt(args.OldStartingIndex + i);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    for (var i = 0; i < args.NewItems.Count; i++) {
                        var item = Convert((TInput) args.NewItems[i]);
                        this[args.OldStartingIndex + i] = item;
                        Notifier(item);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    for (var i = 0; i < args.OldItems.Count; i++)
                        Move(args.OldStartingIndex + i, args.NewStartingIndex + i);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    foreach (var element in SourceCollection) {
                        var item = Convert(element);
                        Add(item);
                        Notifier(item);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}