using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class SharedConnection : IDisposable
	{
        public SharedConnection(SharedDataSource sharedDataSource)
        {
            Connection = sharedDataSource.DataSource.CreateConnection();
        }

		public NpgsqlConnection Connection { get; }

		public void Dispose()
		{
			Connection.Dispose();
		}
	}
}
