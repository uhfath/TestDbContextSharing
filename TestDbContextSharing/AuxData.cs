using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbContextSharing
{
	internal class AuxData
	{
		public int Id { get; set; }

        public required int ClientId { get; set; }
        public Client Client { get; set; }

		public required string Value { get; set; }
	}
}
