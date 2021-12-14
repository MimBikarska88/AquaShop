using System;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Fish
{
    public abstract class Fish: IFish
    {
        protected string _name;

        protected string _species;

        protected decimal _price;
        
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishName);
                }

                _name = value;
            }
        }

        protected Fish(string name, string species, decimal price)
        {
            this.Name = name;
            this.Species= species;
            this.Price = price;
        }
        public string Species
        {
            get => _species;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishSpecies);
                }

                _species = value;
            }
        }
        public int Size { get; protected set; }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishPrice);
                }

                _price = value;
            }
        }
        public abstract void Eat();
    }
}