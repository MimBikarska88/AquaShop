using AquaShop.Models.Decorations.Contracts;

namespace AquaShop.Models.Decorations
{
    public abstract class Decoration : IDecoration
    {
        public int Comfort { get; }
        public decimal Price { get; }

        protected Decoration(int comfort, int price)
        {
            this.Comfort = comfort;
            this.Price = price;
        }
    }
}