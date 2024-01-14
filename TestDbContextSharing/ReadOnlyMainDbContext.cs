using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class ReadOnlyMainDbContext : MainDbContext
	{
		public ReadOnlyMainDbContext(DbContextOptions<ReadOnlyMainDbContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			base.OnConfiguring(optionsBuilder);
		}

		public override int SaveChanges() =>
			throw new InvalidOperationException("This is a readonly db context");

		public override int SaveChanges(bool _) =>
			throw new InvalidOperationException("This is a readonly db context");

		public override Task<int> SaveChangesAsync(bool _, CancellationToken __ = default) =>
			throw new InvalidOperationException("This is a readonly db context");

		public override Task<int> SaveChangesAsync(CancellationToken _ = default) =>
			throw new InvalidOperationException("This is a readonly db context");
	}
}
