using System;
using System.Collections.Generic;

namespace CartApi.Data
{
	public class Bill
	{
		public string Currency { get; private set; }
		public decimal Subtotal { get; private set; }
		public decimal Taxes { get; private set; }
		public IDictionary<string, decimal> Discounts { get; private set; }
		public decimal Total { get; private set; }

		// Limitation: No validation logic especially for object state (e.g. Subtotal - Taxes - Discounts = Total)
		public Bill(string currency, decimal subtotal, decimal taxes, IDictionary<string, decimal> discounts, decimal total)
		{
			Currency = currency;
			Subtotal = subtotal;
			Taxes = taxes;
			Discounts = discounts;
			Total = total;
		}
	}
}