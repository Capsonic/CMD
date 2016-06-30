using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class DashboardLogic : BaseLogic<DashboardRepository, Dashboard>
    {
        public DashboardLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Dashboard> entities)
        {
            var objectiveRepository = new ObjectiveRepository();
            objectiveRepository.context = context;

            foreach (Dashboard item in entities)
            {
                item.Objectives = objectiveRepository.GetListByParent<Dashboard>(item.ID);
            }
        }
    }
}
