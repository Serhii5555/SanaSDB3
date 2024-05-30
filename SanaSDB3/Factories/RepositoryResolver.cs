using Microsoft.Extensions.DependencyInjection;
using SanaSDB3.Factory;
using SanaSDB3.Repositories;
using SanaSDB3.Repositories.SQLRepositories;
using SanaSDB3.Repositories.XMLRepositories;
using System;
using System.ComponentModel;

namespace SanaSDB3.Factories
{
    public enum StorageType
    {
        SQL,
        XML
    }

    public class RepositoryResolver : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private StorageType _storageType = StorageType.SQL;
        private IRepositoryFactory _factory;

        public RepositoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _storageType = StorageType.SQL;
            _factory = serviceProvider.GetRequiredService<SQLFactory>();
        }

        public ICategoriesRepository GetCategoriesRepository()
        {
            return _factory.GetCategoriesRepository();
        }

        public ITasksRepository GetTasksRepository()
        {
            return _factory.GetTasksRepository();
        }

        public RepositoryResolver SetStorageType(StorageType storageType)
        {
            _storageType = storageType;
            switch (storageType)
            {
                case StorageType.SQL: _factory = _serviceProvider.GetRequiredService<SQLFactory>(); break;
                case StorageType.XML: _factory = _serviceProvider.GetRequiredService<XMLFactory>(); break;
                default: throw new InvalidEnumArgumentException(nameof(storageType));
            }

            return this;
        }

        public StorageType GetStorageType()
        {
            return _storageType;
        }
    }
}
