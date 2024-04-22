using eLibrary.Domain.Entities;

namespace eLibrary.Data
{
    public class EFInitialSeed
    {
        public static void Seed (ELibraryContext context)//Сделаем посев начальными значениями в случае, если база данных пуста:
        {
            if(!context.Roles.Any())
            {
                Role client = new Role
                {
                    Name = "client"
                };
                Role admin = new Role
                {
                    Name = "admin"
                };
                context.Roles.Add(client);
                context.Roles.Add(admin);
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}
