using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICatalogRepository
    {
        public Task<List<PlaneModels>> GetPlaneModels();
    }

    public class CatalogRepository: ICatalogRepository
    {
        private readonly WebApplicationDbContext _context;

        public CatalogRepository(WebApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlaneModels>> GetPlaneModels()
        {
            try
            {
                string query = @"
              SELECT U.[Id]
              ,U.[Name]
              ,U.[FuselageInventoryId]
              ,U.[WingsInventoryId]
              ,U.[Wing_Count]
              ,U.[EngineInventoryId]
              ,U.[Engine_Count]
              ,U.[Max_Range]
              ,U.[Length]
              ,U.[Width]
              ,U.[BasePrice]
              ,U.[Image_url] FROM [dbo].[PlaneModels] U
              LEFT JOIN [dbo].[Inventory] Q ON U.[FuselageInventoryId] = Q.Id
              LEFT JOIN [dbo].[Inventory] K ON U.[WingsInventoryId] = K.Id
              LEFT JOIN [dbo].[Inventory] L ON U.[EngineInventoryId] = L.Id
              WHERE Q.AvailableCount > 5 AND K.AvailableCount >= U.Wing_Count AND L.AvailableCount >= U.Engine_Count";

                var planeModels = await _context.PlaneModels
                                          .FromSqlRaw(query)
                                          .ToListAsync();

                return planeModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<PlaneModels>();
            }
        }
    }
}
