using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories.Base;

public enum StorageType
    {
    MongoDB,
    Local,
    }

public class RepositoryFactory
{
    private readonly MongoDbContext? _mongoContext;
    
    private readonly IConfiguration _configuration;

    public RepositoryFactory(IConfiguration configuration, MongoDbContext mongoContext)
    {
        _configuration = configuration;
        _mongoContext = mongoContext;
    }

    public ITaskRepository CreateTaskRepository()
    {
        var repositoryType = _configuration.GetValue<string>("RepositoryType");
        if (repositoryType == "MongoDB")
        {
            return new MongoTaskRepository(new MongoRepository<TaskItem>(_mongoContext.Tasks));
        }
        else
        {
            return new LocalTaskRepository(new LocalRepository<TaskItem>("tasks.json"));
        }
    }

    public IUserRepository CreateUserRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.MongoDB when _mongoContext != null => 
                new MongoUserRepository(new MongoRepository<User>(_mongoContext.Users)),
            StorageType.Local => 
                new LocalUserRepository(new LocalRepository<User>("users.json")),
            _ => throw new ArgumentException("Invalid storage type or missing MongoDB context")
        };
    }
    
    public IEventRepository CreateEventRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.MongoDB when _mongoContext != null => 
                new MongoEventRepository(new MongoRepository<Event>(_mongoContext.Events)),
            StorageType.Local => 
                new LocalEventRepository(new LocalRepository<Event>("events.json")),
            _ => throw new ArgumentException("Invalid storage type or missing MongoDB context")
        };
    }

    public ILocationRepository CreateLocationRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.MongoDB when _mongoContext != null => 
                new MongoLocationRepository(new MongoRepository<Location>(_mongoContext.Locations)),
            StorageType.Local => 
                new LocalLocationRepository(new LocalRepository<Location>("locations.json")),
            _ => throw new ArgumentException("Invalid storage type or missing MongoDB context")
        };
    }
}
