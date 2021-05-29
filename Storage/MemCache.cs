using System;
using System.Collections.Generic;
using System.Linq;
using PISLab.Models;

namespace PISLab.Storage
{
    public class MemCache : IStorage<Product>
    {
        private object _sync = new object();
        private List<Product> _memCache = new List<Product>();
        public Product this[Guid id] 
        { 
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectLabDataException($"No Product with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectLabDataException("Cannot request Product with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<Product> All => _memCache.Select(x => x).ToList();

        public void Add(Product value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectLabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }

        public string StorageType => $"{nameof(MemCache)}";
    }
}