using System.Collections.ObjectModel;

namespace CategoryDD
{
    public class Item
    {
        public Item()
        {
            Children = new ObservableCollection<Item>();
        }
        public string Name { get; set; }
        public ObservableCollection<Item> Children { get; set; }
    }
}