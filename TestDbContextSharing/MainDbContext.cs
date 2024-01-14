using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class MainDbContext : DbContext
	{
		public DbSet<Client> Clients { get; protected set; }
		private DbSet<AuxData> Data { get; set; } //to simplify migrations for the sake of this sample

		protected MainDbContext(DbContextOptions options) : base(options)
		{
		}

		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasPostgresEnum<ClientType>(nameTranslator: SharedDataSource.DefaultNameTranslator);
		}
	}
}
