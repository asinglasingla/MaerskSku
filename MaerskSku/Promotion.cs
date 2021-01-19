using System.Collections.Generic;
using System.Linq;

namespace MaerskSku
{
    public class Promotion
    {
        public Promotion(decimal valueAfterPromotion)
        {
            ValueAfterPromotion = valueAfterPromotion;
        }
        public decimal ValueAfterPromotion { get; }
        public IEnumerable<Sku> SkuInvolved { get; set; }
        public Promotion Successor { get; set; }

        public decimal Calculate(Cart cart)
        {
            decimal totalValue = 0;
            if (cart == null || SkuInvolved == null)
            {
                return totalValue;
            }

            //filter out items from the cart and Sku's involved in the promotion
            var itemsEligibleForPromotion = cart.CartItems?.Join(SkuInvolved, cartItem => cartItem.SkuId, prom => prom.SkuId, (outer, inner) => new { cart = outer, prom = inner }).
                Where(x => x.cart.Quantity >= x.prom.Quantity);

            if (itemsEligibleForPromotion != null && itemsEligibleForPromotion.Count() == SkuInvolved.Count())
            {
                //if out of 10 items in the cart 2 are eligible then this will run 2 times.
                foreach (var item in itemsEligibleForPromotion)
                {
                    //will execute till there is not chance of applying promotion left, which mean items in the cart are now not eligible for this promotion
                    while (item.cart.Quantity >= item.prom.Quantity)
                    {
                        //reduce the quantity of Sku in the cart which all are involved in the promotion with its corresponding promotion quantity
                        foreach (var itemForPromo in itemsEligibleForPromotion)
                        {
                            itemForPromo.cart.Quantity -= itemForPromo.prom.Quantity;
                        }
                        totalValue += ValueAfterPromotion;
                    }
                }
            }
            return totalValue;
        }
    }
}
