using NUnit.Framework;
using MaerskSku;
using System.Linq;
using System.Collections.Generic;

namespace MaerskSkuTest
{
    [TestFixture]
    class PromotionTest
    {
        private Promotion _promotion = null;

        [SetUp]
        public void Setup()
        {

            _promotion = new Promotion(130)
            {
                SkuInvolved = new List<Sku> { new Sku { SkuId = 'A', Quantity = 3 } },
                Successor = new Promotion(45)
                {
                    SkuInvolved = new List<Sku> { new Sku { SkuId = 'B', Quantity = 2 } },
                    Successor = new Promotion(30)
                    {
                        SkuInvolved = new List<Sku> {
                            new Sku { SkuId = 'C', Quantity = 2 } ,
                            new Sku { SkuId = 'D', Quantity = 1 }
                        }
                    }
                }
            };
        }

        [Test]
        public void Promotion_Calculate_ShouldReturnZeroIfPromotionIsNotApplicable()
        {
            //Arrange
            //Sku present in the cart
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 1},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 1},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1}
            };

            //Confifured cart based on scenarios
            Cart cart = new Cart();
            cart.AddToCart(cartItems);

            //Act
            decimal valueAfterApplyingPromotion = _promotion.Calculate(cart);
            Assert.True(valueAfterApplyingPromotion == 0);
        }

        //[TestCase('A', 50, 5, ExpectedResult = 130)]
        //[TestCase('B', 30, 5, ExpectedResult = 90)]
        [TestCase('C', 20, 1, ExpectedResult = 0)]
        public decimal Promotion_Calculate_Should_ApplyPromotionFor_(char sku, decimal unitPrice, int quantity)
        {
            //Confifured cart based on scenarios
            Cart cart = new Cart();
            cart.AddToCart(new Sku { SkuId = sku, UnitPrice = unitPrice, Quantity = quantity });

            //Act
            //It will calculate only for the first cart item, because Promotion handler class is iterating over all cart items one by one

            Promotion specificPromotion = FindPromotion(sku, _promotion);

            //Also for only C or D it will not apply the promotion, as per promotion for C + D at least one of both should be there,
            //else it will just return 0. And Sku specific costs is handled in PromotionHandler.
            var result = specificPromotion.Calculate(cart);
            return result;
        }

        private Promotion FindPromotion(char sku, Promotion promotion)
        {
            if (promotion == null)
            {
                return new Promotion(0) { SkuInvolved = null, Successor = null };
            }

            if (promotion.SkuInvolved.Any(x => x.SkuId == sku))
            {
                return promotion;
            }
            else
            {
                return FindPromotion(sku, promotion.Successor);
            }
        }
    }
}
