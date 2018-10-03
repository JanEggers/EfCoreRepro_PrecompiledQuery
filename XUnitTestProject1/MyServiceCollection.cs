using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitTestProject1
{
    public class MyServiceCollection : ServiceCollection
    {
        public MyServiceCollection(string dbName)
        {
            this.AddDbContext<MyDbContext>(o => o.UseInMemoryDatabase(dbName));
        }
    }
}
