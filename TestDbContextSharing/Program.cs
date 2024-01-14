using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Npgsql.NameTranslation;
using System;

namespace TestDbContextSharing
{
	internal static class Program
	{
		private static void Main()
		{
			using var serviceProvider = new ServiceCollection()
				.AddLogging(opts => opts.SetMinimumLevel(LogLevel.Trace).AddConsole())
				.AddSingleton<SharedDataSource>()
				.AddScoped<SharedConnection>()

				.AddDbContext<MainDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedDataSource>().DataSource))
				.AddDbContext<AuxDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedDataSource>().DataSource))
				.AddDbContext<ReadOnlyMainDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedDataSource>().DataSource))

				//.AddDbContext<MainDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedConnection>().Connection))
				//.AddDbContext<AuxDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedConnection>().Connection))
				//.AddDbContext<ReadOnlyMainDbContext>((sp, opts) => opts.UseNpgsql(sp.GetRequiredService<SharedConnection>().Connection))

				.BuildServiceProvider()
			;

			using var scope = serviceProvider.CreateScope();
			var mainDbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
			mainDbContext.Database.EnsureDeleted();
			mainDbContext.Database.EnsureCreated();

			using var transaction = mainDbContext.Database.BeginTransaction();
			var client = new Client { Name = "test_1", ClientType = ClientType.New };
			mainDbContext.Clients.Add(client);
			mainDbContext.SaveChanges();

			var readOnlyMainDbContext = scope.ServiceProvider.GetRequiredService<ReadOnlyMainDbContext>();
			var clients = readOnlyMainDbContext.Clients
				.Where(cl => cl.ClientType != ClientType.Old)
				.ToArray();

			if (clients.Length == 0)
			{
				throw new InvalidOperationException("No clients found");
			}

			var auxDbContext = scope.ServiceProvider.GetRequiredService<AuxDbContext>();
			var data = clients
				.Select(cl => new AuxData
				{
					ClientId = cl.Id,
					Value = cl.Name,
				})
				.ToArray();

			auxDbContext.Data.AddRange(data);
			auxDbContext.SaveChanges();
		}
	}
}
