using Microsoft.Extensions.DependencyInjection;
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
            _factory = _serviceProvider.GetRequiredService<SQLFactory>();
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
            _factory = storageType switch
            {
                StorageType.SQL => _serviceProvider.GetRequiredService<SQLFactory>(),
                StorageType.XML => _serviceProvider.GetRequiredService<XMLFactory>(),
                _ => throw new InvalidEnumArgumentException(nameof(storageType)),
            };

            return this;
        }

        public StorageType GetStorageType()
        {
            return _storageType;
        }
    }
}
