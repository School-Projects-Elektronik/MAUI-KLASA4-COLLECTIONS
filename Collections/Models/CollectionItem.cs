using System;

namespace Collections.Models
{
    public class CollectionItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Details { get; set; }
    }
}