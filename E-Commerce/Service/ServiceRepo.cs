using E_Commerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using E_Commerce.MiddleWare;
using E_Commerce.Context; // For ApiResponse


namespace E_Commerce.Service
{
    public class ServiceRepo<T> : IService<T> where T : class
    {
        private readonly ECommerceDbContext _context;
        private readonly DbSet<T> _dbSet;

        public ServiceRepo(ECommerceDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private IQueryable<T> IncludeProperties(IQueryable<T> query, List<string> navigationProperties)
        {
            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        public async Task<ApiResponse<IEnumerable<T>>> GetAll(List<string> navigationProperties = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                // Include related entities dynamically if navigation properties are provided
                if (navigationProperties != null && navigationProperties.Any())
                {
                    query = IncludeProperties(query, navigationProperties);
                }

                // Fetch all entities from the database
                var entities = await query.ToListAsync();

                return new ApiResponse<IEnumerable<T>>(200, "Entities retrieved successfully.", entities);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<T>>(500, $"An error occurred: {ex.Message}", null);
            }
        }


        public async Task<ApiResponse<T>> GetById(Guid id, List<string> navigationProperties = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                // Include related entities dynamically if navigation properties are provided
                if (navigationProperties != null && navigationProperties.Any())
                {
                    query = IncludeProperties(query, navigationProperties);
                }

                // Fetch the entity by its ID
                var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

                if (entity == null)
                {
                    return new ApiResponse<T>(404, "Entity not found", null);
                }

                return new ApiResponse<T>(200, "Entity retrieved successfully.", entity);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>(500, $"An error occurred: {ex.Message}", null);
            }
        }

        // Add method
        public async Task<ApiResponse<T>> Create(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ApiResponse<T>(201, "Entity created successfully.", entity);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>(500, $"An error occurred: {ex.Message}", null);
            }
        }

        public async Task<ApiResponse<T>> Update(Guid id, T entity, List<string> navigationProperties = null)
        {
            try
            {
                // Check if the entity with the given id exists, including navigation properties if needed
                var response = await Exists(id, navigationProperties);
                if (response.Data == null)
                {
                    return response;
                }

                // Detach the existing entity if necessary (EF Core tracking)
                _context.Entry(response.Data).State = EntityState.Detached;

                // Assign the id to the incoming entity (optional, depending on requirements)
                var entityProperty = typeof(T).GetProperty("Id");
                if (entityProperty != null)
                {
                    entityProperty.SetValue(entity, id);
                }

                // Update the entity
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                return new ApiResponse<T>(200, "Entity updated successfully.", entity);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>(500, $"An error occurred: {ex.Message}", null);
            }
        }


        public async Task<ApiResponse<T>> Delete(Guid id, List<string> navigationProperties = null)
        {
            try
            {
                var response = await Exists(id, navigationProperties);
                if (response.Data == null)
                    return response;

                _dbSet.Remove(response.Data);
                await _context.SaveChangesAsync();
                return new ApiResponse<T>(200, "Entity deleted successfully.", response.Data);
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>(500, $"An error occurred: {ex.Message}", null);
            }
        }


        public async Task<ApiResponse<T>> Exists(Guid id, List<string> navigationProperties = null)
        {
            IQueryable<T> query = _dbSet;

            // Include related entities dynamically if navigation properties are provided
            if (navigationProperties != null && navigationProperties.Any())
            {
                query = IncludeProperties(query, navigationProperties);
            }

            var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

            if (entity == null)
            {
                return new ApiResponse<T>(404, "Entity not found", null);
            }

            return new ApiResponse<T>(200, "Entity Found", entity);
        }
    }
}
