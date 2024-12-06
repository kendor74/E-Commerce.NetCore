using E_Commerce.MiddleWare;

namespace E_Commerce.Service.Interface
{
    public interface IService<T>
    {
        // Get all entities with optional navigation properties
        Task<ApiResponse<IEnumerable<T>>> GetAll(List<string> navigationProperties = null);

        // Get an entity by ID with optional navigation properties
        Task<ApiResponse<T>> GetById(Guid id, List<string> navigationProperties = null);

        // Create a new entity
        Task<ApiResponse<T>> Create(T entity);

        // Update an existing entity by ID with optional navigation properties
        Task<ApiResponse<T>> Update(Guid id, T entity, List<string> navigationProperties = null);

        // Delete an entity by ID with optional navigation properties
        Task<ApiResponse<T>> Delete(Guid id, List<string> navigationProperties = null);

        // Check if an entity exists by ID with optional navigation properties
        Task<ApiResponse<T>> Exists(Guid id, List<string> navigationProperties = null);
    }
}
