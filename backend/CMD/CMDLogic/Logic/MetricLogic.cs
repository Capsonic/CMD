using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricLogic : IBaseLogic<Metric> { }

    public class MetricLogic : BaseLogic<Metric>, IMetricLogic
    {
        public MetricLogic(DbContext context, IRepository<Metric> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, IList<Metric> entities)
        {
            //Empty by the momemnt.
        }
    }
}