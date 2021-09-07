using CartApi.Data;
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
            Assert.Equal(22.3428M, bill.Total);
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

            decimal expectedTotal = 24.99M - (24.99M * 0.14M) - (0.1M * 24.99M);
            Assert.Equal(expectedTotal, bill.Total);
        }
    }
}
