using NUnit.Framework;
using MaerskSku;
using System.Collections.Generic;

namespace MaerskSkuTest
{
    [TestFixture]
    class PromotionHandlerTest
    {
        [Test]
        public void PromotionHandler_Apply_ShouldApplyCorrectPromotions_OnCartItems_ScenarioA()
        {
            //Arrange
            //Cart items
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 1},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 1},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1}
            };

            Cart cart = new Cart();
            cart.AddToCart(cartItems);

            //Promotion to apply
            Promotion promotion = new Promotion(130)
            {
                SkuInvolved = new List<Sku> { new Sku { SkuId = 'A', Quantity = 3 } },
                Successor = new Promotion(45)
                {
                    SkuInvolved = new List<Sku> { new Sku { SkuId = 'B', Quantity = 2 } },
                    Successor = new Promotion(30)
                    {
                        SkuInvolved = new List<Sku> {
                            new Sku { SkuId = 'C', Quantity = 1 } ,
                            new Sku { SkuId = 'D', Quantity = 1 }
                        }
                    }
                }
            };

            //Act
            PromotionHandler handler = new PromotionHandler();
            handler.RegisterPromotion(promotion);
            handler.Apply(cart);
            Assert.True(handler.TotalValueAfterPromotion == 100);
        }

        [Test]
        public void PromotionHandler_Apply_ShouldApplyCorrectPromotions_OnCartItems_ScenarioB()
        {
            //Arrange
            //Cart items
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 5},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 5},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1}
            };

            Cart cart = new Cart();
            cart.AddToCart(cartItems);

            //Promotion to apply
            Promotion promotion = new Promotion(130)
            {
                SkuInvolved = new List<Sku> { new Sku { SkuId = 'A', Quantity = 3 } },
                Successor = new Promotion(45)
                {
                    SkuInvolved = new List<Sku> { new Sku { SkuId = 'B', Quantity = 2 } },
                    Successor = new Promotion(30)
                    {
                        SkuInvolved = new List<Sku> {
                            new Sku { SkuId = 'C', Quantity = 1 } ,
                            new Sku { SkuId = 'D', Quantity = 1 }
                        }
                    }
                }
            };

            //Act
            PromotionHandler handler = new PromotionHandler();
            handler.RegisterPromotion(promotion);
            handler.Apply(cart);
            Assert.True(handler.TotalValueAfterPromotion == 370);
        }

        [Test]
        public void PromotionHandler_Apply_ShouldApplyCorrectPromotions_OnCartItems_ScenarioC()
        {
            //Arrange
            //Cart items
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 3},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 5},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1},
            new Sku{SkuId = 'D', UnitPrice = 15, Quantity = 1}
            };

            Cart cart = new Cart();
            cart.AddToCart(cartItems);

            //Promotion to apply
            Promotion promotion = new Promotion(130)
            {
                SkuInvolved = new List<Sku> { new Sku { SkuId = 'A', Quantity = 3 } },
                Successor = new Promotion(45)
                {
                    SkuInvolved = new List<Sku> { new Sku { SkuId = 'B', Quantity = 2 } },
                    Successor = new Promotion(30)
                    {
                        SkuInvolved = new List<Sku> {
                            new Sku { SkuId = 'C', Quantity = 1 } ,
                            new Sku { SkuId = 'D', Quantity = 1 }
                        }
                    }
                }
            };

            //Act
            PromotionHandler handler = new PromotionHandler();
            handler.RegisterPromotion(promotion);
            handler.Apply(cart);
            Assert.True(handler.TotalValueAfterPromotion == 280);
        }

        [Test]
        public void PromotionHandler_Apply_ShouldApplyCorrectPromotions_OnCartItems_ScenarioD()
        {
            //Arranges
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 2},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 1}
            };

            Cart cart = new Cart();
            cart.AddToCart(cartItems);

            //Promotion to apply
            //2A
            //1B
            Promotion promotion = new Promotion(130)
            {
                SkuInvolved = new List<Sku> { new Sku { SkuId = 'A', Quantity = 2 }, new Sku { SkuId = 'B', Quantity = 1 } }
            };

            //Act
            PromotionHandler handler = new PromotionHandler();
            handler.RegisterPromotion(promotion);
            handler.Apply(cart);
            Assert.True(handler.TotalValueAfterPromotion == 130);
        }
    }

   
}
