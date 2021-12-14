using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using Microsoft.VisualBasic;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository _repository;
        private List<IAquarium> _aquaria;

        public Controller()
        {
            this._repository = new DecorationRepository();
            this._aquaria = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            switch (aquariumType)
            {
                case "SaltwaterAquarium" : _aquaria.Add(new SaltwaterAquarium(aquariumName));
                    return string.Format(OutputMessages.SuccessfullyAdded,aquariumType);
                
                case "FreshwaterAquarium":_aquaria.Add(new FreshwaterAquarium(aquariumName));
                    return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
                
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
        }

        public string AddDecoration(string decorationType)
        {
            switch (decorationType)
            {
                case "Ornament": _repository.Add(new Ornament());
                    return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
                case "Plant": _repository.Add(new Plant());
                    return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decoration = _repository.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration,decorationType));
            }

            var aquarium = _aquaria.Find(aqua => aqua.Name == aquariumName);
            aquarium.AddDecoration(decoration);
            _repository.Remove(decoration);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType,aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            switch (fishType)
            {
                case "FreshwaterFish": fish = new FreshwaterFish(fishName,fishSpecies,price);
                    break;
                case "SaltwaterFish": fish = new SaltwaterFish(fishName, fishSpecies, price);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            var waterTypeOfFish = fishType.Contains("Freshwater") ? "Freshwater" : "Saltwater";
            
            var aquarium = _aquaria.Find(aqua => aqua.Name==aquariumName);
            if (aquarium.GetType().Name.Contains(waterTypeOfFish))
            {
                aquarium.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType,aquariumName);
            }
            else
            {
                return string.Format(OutputMessages.UnsuitableWater);
            }
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = _aquaria.Find(aqua => aqua.Name == aquariumName);
            var fishCount = aquarium.Fish.Count;
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, fishCount);
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = _aquaria.Find(aqua => aqua.Name == aquariumName);
            var total = aquarium.Decorations.Select(decoration => decoration.Price).Sum() +
                        aquarium.Fish.Select(fish => fish.Price).Sum();
            return string.Format(OutputMessages.AquariumValue,aquariumName ,$"{total:f2}");

        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var aquarium in _aquaria)
            {
                stringBuilder.Append(aquarium.GetInfo() + Environment.NewLine);
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}