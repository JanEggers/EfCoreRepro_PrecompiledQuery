using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        public static readonly Func<MyDbContext, Item> PreCompiled = EF.CompileQuery<MyDbContext, Item>(ctx => ctx.Items.FirstOrDefault());

        [Fact]
        public void Test1()
        {
            using (var services = new MyServiceCollection("FOO").BuildServiceProvider())
            {
                var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                using (var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>())
                {
                    ctx.Add(new Item());

                    ctx.SaveChanges();

                    var item = PreCompiled(ctx);
                    Assert.NotNull(item);
                }

                using (var scope = scopeFactory.CreateScope())
                using (var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>())
                {
                    var item = PreCompiled(ctx);
                    Assert.NotNull(item);
                }
            }

            using (var services = new MyServiceCollection("BAR").BuildServiceProvider())
            {
                var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                using (var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>())
                {
                    ctx.Add(new Item());

                    ctx.SaveChanges();

                    var item = PreCompiled(ctx);
                    Assert.NotNull(item);
                }

                using (var scope = scopeFactory.CreateScope())
                using (var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>())
                {
                    var item = PreCompiled(ctx);
                    Assert.NotNull(item);
                }
            }
        }
    }
}
