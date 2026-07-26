using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites.Orders;
using E_Commerce.Domain.Entites.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder : IDataSeeder
    {
        private readonly StoreDbContext _dbContext;
        private readonly ILogger<CatalogDataSeeder> _logger;

        public CatalogDataSeeder(StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
           try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);
                var seedRoot=Path.Combine(AppContext.BaseDirectory,"DataSeed");
                await SeedIfEmptyAsync<ProductBrand,int>(seedRoot,"brands.json.json",ct); 
               await SeedIfEmptyAsync<ProductType,int>(seedRoot, "types.json.json", ct); 
               await SeedIfEmptyAsync<Product,int>(seedRoot, "products.json.json", ct); 
               await SeedIfEmptyAsync<DeliveryMethod,int>(seedRoot, "delivery.json.json", ct); 

             int result= await _dbContext.SaveChangesAsync(ct);
                if(result>0)
                    _logger.LogInformation($"{result}Data Seeded Successfully");
                else
                    _logger.LogInformation("No Data Seeded");

            }
            catch
            {

            }
        }

        private async Task SeedIfEmptyAsync<T,Tkey>(string rootPath,string fileName,CancellationToken ct) where T : BaseEntity<Tkey>
        {
             if(await _dbContext.Set<T>().AnyAsync(ct))
            {
                _logger.LogInformation("Table Already Has Data");
                return;
            }
             var filePath=Path.Combine(rootPath,fileName);

            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File {fileName} Is Not Exist");
                return;
            }
           
            using var fileStream=File.OpenRead(filePath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var items= await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options,ct);
            if(items?.Any()?? false)
                _dbContext.Set<T>().AddRange(items);


        }
    }
}
