using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class AuxDbContext : DbContext
	{
		public DbSet<AuxData> Data { get; protected set; }

		public AuxDbContext(DbContextOptions<AuxDbContext> options) : base(options)
		{
		}
	}
}
