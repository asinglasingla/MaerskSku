using System;
using System.Collections.Generic;

namespace MaerskSku
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sku present in the cart
            List<Sku> cartItems = new List<Sku> {
            new Sku{SkuId = 'A', UnitPrice = 50, Quantity = 3},
            new Sku{SkuId = 'B', UnitPrice = 30, Quantity = 5},
            new Sku{SkuId = 'C', UnitPrice = 20, Quantity = 1},
            new Sku{SkuId = 'D', UnitPrice = 30, Quantity = 1}
            };

            //Confifured cart based on scenarios
            Cart cart = new Cart();
            cart.AddToCart(cartItems);


            //Configuring what all SkuId's involved in promotion. These promotions can also be configured based on the 
            //the values in database. For e.g if we add all these sku details in DB then these can be generated automatically
            Promotion promotion = new Promotion(130)
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

            //Registered Promotions with the Promotion handler which will calculate the promotion for the cart items
            PromotionHandler promotionHandler = new PromotionHandler();
            promotionHandler.RegisterPromotion(promotion);
            promotionHandler.Apply(cart);

            Console.WriteLine(promotionHandler.TotalValueAfterPromotion);
        }
    }
}
