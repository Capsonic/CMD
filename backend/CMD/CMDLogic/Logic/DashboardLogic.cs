using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class DashboardLogic : BaseLogic<Dashboard>
    {
        public DashboardLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Dashboard> entities)
        {
            var objectiveRepository = RepositoryFactory.Create<Objective>();
            objectiveRepository.context = context;

            foreach (Dashboard item in entities)
            {
                item.Objectives = objectiveRepository.GetListByParent<Dashboard>(item.ID);
            }
        }
    }
}
