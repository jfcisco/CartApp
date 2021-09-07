using System;
using System.Collections.Generic;

namespace CartApi.Data
{
	public class Cart
	{
		public int Id { get; set; }
		public IDictionary<Product, int> Items { get; set; }
	}
}