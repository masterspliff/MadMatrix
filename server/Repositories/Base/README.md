# Base Repository Pattern Implementation

## Overview
This directory contains the base implementation of the repository pattern used throughout the application. The pattern provides a consistent way to handle data access operations across different storage mechanisms (MongoDB and File-based storage).

## Core Components

### 1. IRepository<T> Interface
The foundation interface that defines standard CRUD operations:
```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

### 2. MongoRepository<T>
Generic MongoDB implementation that works with any entity type:
- Handles all basic CRUD operations using MongoDB.Driver
- Uses dependency injection for MongoDB collections
- Implements async operations for better performance

### 3. FileRepository<T>
Local file-based storage implementation:
- Stores entities in JSON files
- Maintains in-memory collection with file persistence
- Handles ID generation and management
- Inherits from LocalRepositoryBase<T>

### 4. LocalRepositoryBase<T>
Abstract base class for local storage implementations:
- Provides file I/O operations
- Manages entity collections
- Handles JSON serialization/deserialization

### 5. RepositoryFactory
Factory pattern implementation for creating repositories:
- Supports multiple storage types (MongoDB, File)
- Handles dependency injection
- Provides type-safe repository creation

## Usage Examples

### 1. Using the Factory
```csharp
// Create a repository factory
var factory = new RepositoryFactory(mongoContext);

// Get MongoDB repository
var mongoUserRepo = factory.CreateUserRepository(StorageType.MongoDB);

// Get File repository
var fileUserRepo = factory.CreateUserRepository(StorageType.File);
```

### 2. Direct Repository Usage
```csharp
// MongoDB Repository
public class UserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> GetUser(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
```

## Best Practices

1. **Always use the interface**: Program to IRepository<T> rather than concrete implementations
2. **Async operations**: All repository operations are async - always await them
3. **Error handling**: Implement proper error handling around repository operations
4. **ID management**: Ensure entities have an Id property for proper tracking
5. **Dependency injection**: Use the RepositoryFactory or inject repositories directly

## Storage Type Selection

Choose storage type based on your needs:

- **MongoDB**: 
  - Production environments
  - Large datasets
  - Distributed systems
  
- **Local Storage**: 
  - Development/testing
  - Simple data persistence
  - Local applications

## Implementation Notes

1. **Entity Requirements**:
   - Must have an "Id" property
   - Must be a class (reference type)
   - Must be serializable (for file storage)

2. **Thread Safety**:
   - MongoDB implementation is thread-safe
   - File storage implementation requires additional synchronization for concurrent access

3. **Performance Considerations**:
   - MongoDB operations are optimized for large datasets
   - File storage loads entire dataset into memory

## Extension Points

The base implementation can be extended in several ways:

1. Add new storage types by:
   - Implementing IRepository<T>
   - Adding new enum value to StorageType
   - Updating RepositoryFactory

2. Add custom repository operations by:
   - Creating new interface extending IRepository<T>
   - Implementing in concrete repositories

3. Add cross-cutting concerns by:
   - Creating decorator classes
   - Implementing additional repository middleware

## Troubleshooting

Common issues and solutions:

1. **Local Storage Issues**:
   - Check file permissions
   - Verify JSON serialization compatibility
   - Ensure unique IDs

2. **MongoDB Issues**:
   - Verify connection string
   - Check MongoDB service status
   - Validate document schema

## Contributing

When extending or modifying the base repository implementation:

1. Maintain the async pattern
2. Follow existing naming conventions
3. Update unit tests
4. Document new features or changes
5. Consider backward compatibility
