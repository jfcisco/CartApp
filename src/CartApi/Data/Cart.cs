using System;
using System.Collections.Generic;
using System.Linq;

namespace CartApi.Data
{
	public class Cart
	{
		private const decimal TAX_RATE = 0.14M;
		public virtual ICollection<Product> Products { get; set; } = new List<Product>();

		/// <summary>
		/// Calculates the bill of the products in the cart
		/// </summary>
		/// <returns>The bill</returns>
        public virtual Bill CalculateBill()
		{
			decimal subtotal = decimal.Zero;
			// Get subtotal by summing up the products' prices
			subtotal += Products.Select(p => p.Price).Sum();
			
			// Get tax amount according to tax rate
			decimal taxes = subtotal * TAX_RATE;

			// Calculate applicable offers and sum up their discount amounts
			IDictionary<string, decimal> offers = GetOffers(Products);
			decimal discounts = 0.0M;

			discounts = offers.Values.Sum();

			// Get total
			decimal total = subtotal + taxes - discounts;

			// Provide bill
			return new Bill("$", subtotal, taxes, offers, total);
        }

		/// <summary>
		/// Clears the products in the cart.
		/// </summary>
		public virtual void Clear()
        {
			Products.Clear();
        }

		private IDictionary<string, decimal> GetOffers(IEnumerable<Product> products)
        {
			Dictionary<string, decimal> offers = new();
			/* Apply Offer 1: Shoes are on 10% off. */
			decimal applicableShoeDiscounts = 
				products.Where(p => p.Name == "Shoes")
				.Select(p => p.Price * 0.1M)
				.Sum();
			
			if (applicableShoeDiscounts > 0.0M)
            {
				offers.Add("10% off shoes", applicableShoeDiscounts);
            }

			/* Offer 2: Buy two t-shirts and get a jacket half its price.*/

			var jackets = products.Where(p => p.Name == "Jacket");
			if (jackets.Any())
            {
				int numOfTshirtPairs = products.Where(p => p.Name == "T-shirt").Count() / 2;
				var numOfJackets = products.Where(p => p.Name == "Jacket").Count();
				decimal priceOfJacket = jackets.First().Price;
				
				// Calculate total discount amount
				// Ensures number of discounted jackets does not exceed the number of t-shirt pairs bought by the customer.
				decimal totalJacketDiscountAmount = Math.Min(numOfTshirtPairs, numOfJackets) * priceOfJacket * 0.5M;

				offers.Add("50% off jackets", totalJacketDiscountAmount);
			}

			return offers;
		}
	}
}