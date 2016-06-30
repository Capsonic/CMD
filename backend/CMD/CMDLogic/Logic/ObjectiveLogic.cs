using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class ObjectiveLogic : BaseLogic<ObjectiveRepository, Objective>
    {
        public ObjectiveLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Objective> entities)
        {
            var initiativeRepository = new InitiativeRepository();
            var metricRepository = new MetricRepository();

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