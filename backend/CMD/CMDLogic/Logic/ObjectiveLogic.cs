using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class ObjectiveLogic : BaseLogic<Objective>
    {
        public ObjectiveLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Objective> entities)
        {
            var initiativeRepository = RepositoryFactory.Create<Initiative>();
            var metricRepository = RepositoryFactory.Create<Metric>();

            initiativeRepository.context = context;
            metricRepository.context = context;

            foreach (Objective item in entities)
            {
                item.Initiatives = initiativeRepository.GetListByParent<Objective>(item.ID);
                item.Metrics = metricRepository.GetListByParent<Objective>(item.ID);
            }
        }
    }
}