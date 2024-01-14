using Npgsql;
using Npgsql.NameTranslation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class SharedDataSource : IDisposable
	{
		private const string ConnectionString = "Host=localhost;Database=DbContextShareTest;Username=postgres;Password=123;";
		public static readonly INpgsqlNameTranslator DefaultNameTranslator = new NpgsqlSnakeCaseNameTranslator();
		
		public SharedDataSource()
        {
			var builder = new NpgsqlDataSourceBuilder(ConnectionString);
			builder.DefaultNameTranslator = DefaultNameTranslator;
			builder.MapEnum<ClientType>(nameTranslator: DefaultNameTranslator);

			DataSource = builder.Build();
		}

		public NpgsqlDataSource DataSource { get; }

		public void Dispose()
		{
			((IDisposable)DataSource).Dispose();
		}
	}
}
