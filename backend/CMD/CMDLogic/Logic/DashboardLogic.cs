using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public class DashboardLogic : BaseLogic<Dashboard>
    {
        private readonly IRepository<Objective> objectiveRepository;

        public DashboardLogic(DbContext context, IRepository<Dashboard> repository, IRepository<Objective> objectiveRepository) : base(context, repository)
        {
            this.objectiveRepository = objectiveRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Dashboard> entities)
        {
            foreach (Dashboard item in entities)
            {
                item.Objectives = objectiveRepository.GetListByParent<Dashboard>(item.ID);
            }
        }
    }
}
