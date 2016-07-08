using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IDashboardLogic : IBaseLogic<Dashboard> { }

    public class DashboardLogic : BaseLogic<Dashboard>, IDashboardLogic
    {
        private readonly IObjectiveLogic objectiveLogic;

        public DashboardLogic(DbContext context, IRepository<Dashboard> repository, IObjectiveLogic objectiveLogic) : base(context, repository)
        {
            this.objectiveLogic = objectiveLogic;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Dashboard> entities)
        {
            objectiveLogic.byUserId = byUserId;

            foreach (Dashboard item in entities)
            {
                item.Objectives = (ICollection<Objective>) objectiveLogic.GetAllByParent<Dashboard>(item.id).Result;
            }
        }
    }
}
