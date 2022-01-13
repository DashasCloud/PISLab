using PISLab.Models;

namespace PISLab.Storage
{
    public class StorageService
    {
        private readonly IStorage<Product> _storage;

        public StorageService(IStorage<Product> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
}