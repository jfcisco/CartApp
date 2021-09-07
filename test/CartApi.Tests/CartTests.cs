using CartApi.Data;
using System.Collections.Generic;
using Xunit;

namespace CartApi.Tests
{
    public class CartTests
    {
        private readonly Cart cart;

        public CartTests()
        {
            cart = new Cart();
        }

        [Fact]
        public void CalculateBill_Success_FilledCart()
        {
            cart.Products.Add(new Product { Name = "T-shirt", Price = 10.99M });
            cart.Products.Add(new Product { Name = "Pants", Price = 14.99M });
            Bill bill = cart.CalculateBill();

            Assert.NotNull(bill);
            Assert.Equal(25.98M, bill.Subtotal);
            Assert.Equal(3.6372M, bill.Taxes);
            Assert.Equal(0, bill.Discounts.Count);
            Assert.Equal(29.6172M, bill.Total);
        }

        [Fact]
        public void CalculateBill_Success_EmptyCart()
        {
            Bill bill = cart.CalculateBill();
            
            Assert.NotNull(bill);
            Assert.Equal(0.0M, bill.Subtotal);
            Assert.Equal(0.0M, bill.Taxes);
            Assert.Equal(0, bill.Discounts.Count);
            Assert.Equal(0.0M, bill.Total);
        }

        [Fact]
        public void CalculateBill_Success_AppliesShoeDiscount()
        {
            cart.Products.Add(new Product { Name = "Shoes", Price = 24.99M });

            Bill bill = cart.CalculateBill();

            Assert.Equal(24.99M, bill.Subtotal);
            Assert.Equal(24.99M * 0.14M, bill.Taxes);
            Assert.Equal(1, bill.Discounts.Count);
            // Check that 10% off shoes offer is included
            Assert.Contains(bill.Discounts, offer => offer.Key == "10% off shoes" && offer.Value == 0.1M * 24.99M);

            decimal expectedTotal = 24.99M + (24.99M * 0.14M) - (0.1M * 24.99M);
            Assert.Equal(expectedTotal, bill.Total);
        }


        [Theory]
        [InlineData(2, 1, 9.995)]
        [InlineData(2, 2, 9.995)]
        [InlineData(4, 3, 19.99)]
        public void CalculateBill_Success_AppliesHalfOffJacketDiscount(int tshirtCount, int jacketCount, decimal expectedDiscountAmount)
        {
            var products = GenerateTshirtsAndJackets(tshirtCount, jacketCount);

            cart.Products = products;
            Bill bill = cart.CalculateBill();

            Assert.NotNull(bill);
            string expectedOfferName = "50% off jackets";
            Assert.Contains(bill.Discounts, offer => offer.Key == expectedOfferName && offer.Value == expectedDiscountAmount);
        }

        // Generates a list with user-specified counts of T-shirt and Jacket products
        private List<Product> GenerateTshirtsAndJackets(int tshirtCount, int jacketCount)
        {
            List<Product> productsList = new();
            for(int i = 0; i < tshirtCount; i++)
            {
                productsList.Add(new Product { Name = "T-shirt", Price = 10.99M });
            }

            for(int j = 0; j < jacketCount; j++)
            {
                productsList.Add(new Product { Name = "Jacket", Price = 19.99M });
            }

            return productsList;
        }
    }
}
