using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IInitiativeLogic : IBaseLogic<Initiative> { }

    public class InitiativeLogic : BaseLogic<Initiative>, IInitiativeLogic
    {
        private readonly IRepository<Gant> gantRepository;

        public InitiativeLogic(DbContext context, IRepository<Initiative> repository, IRepository<Gant> gantRepository) : base(context, repository)
        {
            this.gantRepository = gantRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Initiative> entities)
        {
            foreach (Initiative item in entities)
            {
                item.Gants = gantRepository.GetListByParent<Initiative>(item.id);
            }
        }
    }
}
