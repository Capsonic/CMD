using CMDLogic.EF;
using CMDLogic.Reusable;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public class ObjectiveLogic : BaseLogic<ObjectiveRepository, Objective>
    {
        public ObjectiveLogic()
        {
            ParentRepository = new DashboardRepository();
            ParentType = typeof(Dashboard);
        }

        protected override void attachParent(MainContext context, Objective entity, BaseEntity parent)
        {
            entity.Dashboards.Add((Dashboard)parent);
        }

        protected override void loadNavigationProperties(MainContext context, IList<Objective> entities)
        {
            //           
        }
    }
}
