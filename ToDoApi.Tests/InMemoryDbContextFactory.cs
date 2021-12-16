using Microsoft.EntityFrameworkCore;
using ToDoApi.Persistence.Context;

namespace ToDoApi.Tests
{
    public static class InMemoryDbContextFactory
    {
        private const string DatabaseName = "ToDoTestDb";

        public static ApplicationDbContext CreateInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(DatabaseName);
            var options = builder.Options;

            var context = new ApplicationDbContext(options);

            return context;
        }
    }
}
