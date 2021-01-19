using NUnit.Framework;
using MaerskSku;
using System.Linq;
using System.Collections.Generic;

namespace MaerskSkuTest
{
    public class CartTest
    {
        private Cart _cart = null;

        [SetUp]
        public void Setup()
        {
            _cart = new Cart();

        }

        [Test]
        public void Cart_AddToCart_ShouldBeAbleToAddSkuItems()
        {
            //Arrange
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 3},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 5},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1},
            new Sku{SkuId = 'D', UnitPrice = 30, Quantity = 1}
            };

            //Act
            _cart.AddToCart(cartItems);

            //Assert
            Assert.True(_cart.CartItems.Any() && _cart.CartItems.Count() == 4);
        }

        [TearDown]
        public void TearDown()
        {
            _cart = null;
        }
    }
}
