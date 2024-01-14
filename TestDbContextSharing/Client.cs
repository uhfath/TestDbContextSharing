using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class Client
	{
		public int Id { get; set; }
		public required string Name { get; set; }
        public required ClientType ClientType { get; set; }
    }
}
