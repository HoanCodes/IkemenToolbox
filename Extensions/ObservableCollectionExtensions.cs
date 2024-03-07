using System.Collections.ObjectModel;

namespace IkemenToolbox.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddToStart<T>(this ObservableCollection<T> observableCollection, T item) => observableCollection.Insert(0, item);
    }
}
