# ExtendedObservableCollection
 This library contains a set of classes for making your Lists, Dictionaries and other Collections observable. The Observable Collections contained in this library can wrap existing collections or can be used on their own, generating their own backing collection. The library attempts to implement all interfaces of its backing collections and generate events for interface method calls when downcast. The library has a comprehensive unit test which tests the ObservableCollection and its interfaces.

 This library also includes utility classes including a synchronization observable collection which can synchronize an observable list of viewmodels to a source list of models.

## OBSERVABLES

The observable collections in this library implement INotifyCollectionChanged and INotifyPropertyChanged. The INotifyCollectionChanged implementation generates NotifyCollectionChangedEventArgs that fully implements the NotifyCollectionChangedAction and supports multiple Adds in a single event. Example libraries, such as ParallelExtensionsExtras, often take a shortcut and only generate NotifyCollectionChangedAction.Reset events. 

The observable dictionaries in this library implement a custom INotifyDictionaryChanged. INotifyDictionaryChanged is templated after NotifyCollectionChanged, but is used for Dictionary instead of Collection<>. Consequently, NotifyDictionaryChanged uses Keys instead of Indexes in its event args. Otherwise, the operations are analogous.


## STANDARD COLLECTION CLASSES
 
### ObservableList< TItem >
 The ObservableList implements INotifyCollectionChanged and INotifyPropertyChanged and is a wrapper for a standard List<>. It triggers NotifyCollectionChanged and NotifyPropertyChanged events for List operation: Add, AddRanmge, Clear, Insert, Move, Remove, RemoveAt and this[]. It implements all interfaces of List and triggers notify events for interface methods when downcast.

### ObservableDictionary< TKey, TValue > 
The ObservableDictionary implements a INotifyDictionaryChanged and is a wrapper for a standard Dictionary<,>. It triggers NotifyDictionaryChanged events for the Dictionary operations: Add, Clear, Remove, and this[]. It implements all interfaces of Dictionary and triggers notify events for interface methods when downcast.

### ObservableSortedList< TKey, TValue > 
The ObservableSortedList implements INotifyCollectionChanged, INotifyPropertyChanged and INotifyDictionaryChanged and is a wrapper for a standard SortedList<,>. It triggers events for Standad SortedList operations. It implements all interfaces of SortedList and triggers notify events for interface methods when downcast.

## EXTENDED COLLECTION CLASSES

### ObservableListKeyed< TKey, TItem >
The ObservableListKeyed<,> is an ObservableList that allows indexing by a Key that is mapped to a property of the TItem. The mapping can be specified by implementing the abstract GetKey() method. Alternately, one can instantiate ObservableListKeyedFunc<,> with the mapping given as an anonymous function. It implements INotifyCollectionChanged, INotifyPropertyChanged and INotifyDictionaryChanged.

### ObservableListAdapter< TInputItem, TOutputItem >
The ObservableListAdapter<,> is an ObservableList<TOutputItem> that performs a unidirectional syncronization with an SourceCollection of type IObservableCollection<TInputItem>. The syncronization is peformed when an IObservableCollection is added via Constructor or changed using the SourceCollection property. The syncronization is maintained via added events. The The mapping can be specified by implementing the abstract "TOutput Convert(TInput item)" method. Alternately, one can instantiate ObservableListAdapterFunc<,> with the mapping given as an anonymous function. Its important to note, changes to items in the SourceCollection will propagate to the ObservableListAdapter, but changes to the ObservableListAdapter will NOT propagate back to the source collection. This option may be added in a future release.
 
### ObservableDictionaryCollection<,> - TBA



 
