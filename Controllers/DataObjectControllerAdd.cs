
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConstructionRegistry.Models;

namespace ConstructionRegistry.Controllers
{
    public class DataObjectControllerAdd
    {
        private static DataObjectControllerAdd? _instance;

        public static DataObjectControllerAdd Instance => _instance ??= new DataObjectControllerAdd();
        // Строка подключения — позже вынеси в appsettings.json
        private const string ConnectionString = 
            "Server=.\\SQLEXPRESS;Database=ConstructionRegistryDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public async Task<bool> AddObjectAsync(ConstructionObject obj, Kontragent kontragent)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(ConnectionString);

                using var db = new AppDbContext(optionsBuilder.Options);

                // Правильная загрузка с Include через лямбду (EF Core)
                var kontragentWithObjects = await db.Kontragents
                    .Include(k => k.ConstructionObjects)
                    .FirstOrDefaultAsync(k => k.ID == kontragent.ID);

                if (kontragentWithObjects == null)
                    return false;

                kontragentWithObjects.ConstructionObjects.Add(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // В реальной работе лучше логировать исключение, а не глотать
                return false;
            }
        }

        public async Task<bool> AddPersonAsync(ResponsiblPerson obj, Kontragent kontragent)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(ConnectionString);

                using var db = new AppDbContext(optionsBuilder.Options);

                var kontragentWithPersons = await db.Kontragents
                    .Include(k => k.ResponsiblPersons)
                    .FirstOrDefaultAsync(k => k.ID == kontragent.ID);

                if (kontragentWithPersons == null)
                    return false;

                kontragentWithPersons.ResponsiblPersons.Add(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddKontragentAsync(Kontragent obj)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(ConnectionString);

                using var db = new AppDbContext(optionsBuilder.Options);

                db.Kontragents.Add(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}