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

            var itemsEligibleForPromotion = cart.CartItems?.Join(SkuInvolved, cartItem => cartItem.SkuId, prom => prom.SkuId, (outer, inner) => new { cart = outer, prom = inner }).
                Where(x => x.cart.Quantity >= x.prom.Quantity);

            if (itemsEligibleForPromotion != null && itemsEligibleForPromotion.Count() == SkuInvolved.Count())
            {
                foreach (var item in itemsEligibleForPromotion)
                {
                    while (item.cart.Quantity >= item.prom.Quantity)
                    {
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
