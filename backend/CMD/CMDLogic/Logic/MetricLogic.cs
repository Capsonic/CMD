using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class MetricLogic : BaseLogic<Metric>
    {
        public MetricLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Metric> entities)
        {
            foreach (Metric item in entities)
            {
                //TODO
            }
        }
    }
}