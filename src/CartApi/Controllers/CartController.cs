using CartApi.Data;
using CartApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService CartService;
        private readonly ProductService ProductService;

        public CartController(CartService cartService, ProductService productService)
        {
            CartService = cartService;
            ProductService = productService;
        }

        [HttpPost("products")]
        public ActionResult AddToCart(string productName)
        {
            Product productToAdd;
            try
            {
                productToAdd = ProductService.SearchProductByName(productName);
            }
            catch(ProductNotFoundException)
            {
                return NotFound();
            }

            CartService.AddToCart(productToAdd);
            return Created("cart/products", productToAdd.Name);
        }

        [HttpGet("bill")]
        public ActionResult<Bill> CalculateBill()
        {
            return CartService.CalculateBill();
        }

        [HttpDelete]
        public ActionResult ClearCart()
        {
            CartService.ClearCart();
            return NoContent();
        }
    }
}