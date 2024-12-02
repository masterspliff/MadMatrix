using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories.Base;

public enum StorageType
{
    MongoDB,
    File
}

public class RepositoryFactory
{
    private readonly MongoDbContext? _mongoContext;
    
    public RepositoryFactory(MongoDbContext? mongoContext = null)
    {
        _mongoContext = mongoContext;
    }

    public IUserRepository CreateUserRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.MongoDB when _mongoContext != null => 
                new MongoUserRepository(new MongoRepository<User>(_mongoContext.Users)),
            StorageType.File => 
                new LocalUserRepository(new LocalRepository<User>("users")),
            _ => throw new ArgumentException("Invalid storage type or missing MongoDB context")
        };
    }
}
