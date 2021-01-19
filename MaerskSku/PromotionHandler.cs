using System.Linq;

namespace MaerskSku
{
    public class PromotionHandler
    {
        private Promotion _promotion = null;
        public decimal TotalValueAfterPromotion { get; private set; } = 0;
        public void RegisterPromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                _promotion = promotion;
            }
        }

        public void Apply(Cart cart)
        {
            if (_promotion != null)
            {
                Promotion currentPromotion = _promotion;
                while (currentPromotion != null)
                {
                    TotalValueAfterPromotion += currentPromotion.Calculate(cart);
                    currentPromotion = currentPromotion.Successor;
                }
                foreach (var item in cart.CartItems.Where(x => x.Quantity > 0))
                {
                    TotalValueAfterPromotion += item.Quantity * item.UnitPrice;
                }
            }
        }
    }
}
