using CMDLogic.EF;
using CMDLogic.Reusable;
using System;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class DashboardLogic : BaseLogic<DashboardRepository, Dashboard>
    {
        protected override void attachParent(MainContext context, Dashboard entity, BaseEntity parent)
        {
            throw new NotImplementedException();
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
