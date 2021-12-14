using System.Collections.Generic;
using System.Linq;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {

        private List<IDecoration> _models;

        public DecorationRepository()
        {
            this._models = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models { get=>_models; }
        public void Add(IDecoration model)
        {
            _models.Add(model);
        }

        public bool Remove(IDecoration model)
        {
            return _models.Remove(model);
        }

        public IDecoration FindByType(string type)
        {
            return _models.FirstOrDefault(model => model.GetType().Name == type);
        }
    }
}