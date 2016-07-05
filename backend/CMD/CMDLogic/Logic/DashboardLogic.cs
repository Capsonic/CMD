using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public class DashboardLogic : BaseLogic<Dashboard>
    {
        public DashboardLogic(DbContext context, BaseRepository<Dashboard> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, IList<Dashboard> entities)
        {
            var objectiveRepository = RepositoryFactory.Create<Objective>(context, byUserId);

            foreach (Dashboard item in entities)
            {
                item.Objectives = objectiveRepository.GetListByParent<Dashboard>(item.ID);
            }
        }
    }
}
