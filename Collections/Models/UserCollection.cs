using System.Collections.ObjectModel;

namespace Collections.Models
{
    public class UserCollection
    {
        public string Name { get; set; }
        public ObservableCollection<CollectionItem> Items { get; set; } = new ObservableCollection<CollectionItem>();
    }
}