﻿using CartApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Services
{
    public class CartService
    {
        private readonly Cart Cart;

        public CartService()
        {
            Cart = new Cart();
        }

        public void AddToCart(Product product)
        {
            if (product != null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Cart.Products.Add(product);
        }

        public Bill CalculateBill()
        {
            return Cart.CalculateBill();
        }

        public void ClearCart()
        {
            Cart.Clear();
        }
    }
}