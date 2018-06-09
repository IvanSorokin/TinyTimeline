using System;

namespace DataAccess.Concrete.Attributes
{
    public class CollectionNameAttribute : Attribute
    {
        public string CollectionName { get; }
        
        public CollectionNameAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}