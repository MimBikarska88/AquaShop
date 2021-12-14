using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        protected string _name;
        protected List<IFish> _fishes;
        protected List<IDecoration> _decorations;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            _fishes = new List<IFish>();
            _decorations = new List<IDecoration>();
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                _name = value;
            }
        }
        public int Capacity { get; }

        public int Comfort
        {
            get => Decorations.Select(decoration => decoration.Comfort).Sum();
        }

        public ICollection<IDecoration> Decorations
        {
            get => _decorations;
        }

        public ICollection<IFish> Fish
        {
            get => _fishes;
        }

        public virtual void AddFish(IFish fish)
        {
            if (Fish.Count > this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            Fish.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            return Fish.Remove(fish);
        }

        public void AddDecoration(IDecoration decoration)
        {
            Decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var fish in Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{Name} ({this.GetType().Name}):"+Environment.NewLine);
            string fishCount = "none";
            if (Fish.Count > 0)
            {
                fishCount = string.Join(", ", Fish.Select(fish => fish.Name));
            }

            stringBuilder.Append($"Fish: {fishCount.Trim()}"+Environment.NewLine);
            stringBuilder.Append($"Decorations: {Decorations.Count}"+Environment.NewLine);
            stringBuilder.Append($"Comfort: {Comfort}"+Environment.NewLine);
            return stringBuilder.ToString().TrimEnd();
        }
    }
}