using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public class ObjectiveLogic : BaseLogic<Objective>
    {
        public ObjectiveLogic(DbContext context, BaseRepository<Objective> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, IList<Objective> entities)
        {
            var initiativeRepository = RepositoryFactory.Create<Initiative>(context, byUserId);
            var metricRepository = RepositoryFactory.Create<Metric>(context, byUserId);

            foreach (Objective item in entities)
            {
                item.Initiatives = initiativeRepository.GetListByParent<Objective>(item.ID);
                item.Metrics = metricRepository.GetListByParent<Objective>(item.ID);
            }
        }
    }
}