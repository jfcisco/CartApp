using CartApi.Data;
using CartApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace CartApi.Tests
{
    public class CartServiceTests
    {
        private readonly Mock<Cart> MockCart;
        private CartService CartService;
        public CartServiceTests()
        {
            MockCart = new Mock<Cart>();
        }

        [Fact]
        public void AddToCart_Success()
        {
            MockCart.SetupProperty(c => c.Products, new List<Product>());
            CartService = new CartService(MockCart.Object);

            Product sample = new() { Name = "Sample", Price = 0.99M };
            CartService.AddToCart(sample);

            var actualProduct = MockCart.Object.Products.FirstOrDefault();
            Assert.NotNull(actualProduct);
            Assert.Equal(sample, actualProduct);
        }

        [Fact]
        public void AddToCart_Failure_NullArgument()
        {
            CartService = new CartService(MockCart.Object);

            Assert.Throws<ArgumentNullException>(() => CartService.AddToCart(null));
        }

        [Fact]
        public void CalculateBill_Success()
        {
            Bill returnBill = new("$", 1.00M, 1.00M, new Dictionary<string, decimal>(), 2.00M);
            MockCart.Setup(c => c.CalculateBill()).Returns(returnBill);
            CartService = new CartService(MockCart.Object);

            Bill actualBill = CartService.CalculateBill();
            Assert.Equal(returnBill, actualBill);
        }

        [Fact]
        public void ClearCart_Success()
        {
            Cart cart = new();
            cart.Products = new List<Product>
            {
                new Product { Name = "T-shirt", Price = 10.99M },
                new Product { Name = "Pants", Price = 14.99M }
            };

            CartService = new CartService(cart);
            CartService.ClearCart();
            
            Assert.Equal(new List<Product>(), cart.Products);
        }
    }
}
