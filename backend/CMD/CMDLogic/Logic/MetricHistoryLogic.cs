using CMDLogic.EF;
using Reusable;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricHistoryLogic : IBaseLogic<MetricHistory> { }

    public class MetricHistoryLogic : BaseLogic<MetricHistory>, IMetricHistoryLogic
    {
        public MetricHistoryLogic(DbContext context, IRepository<MetricHistory> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, params MetricHistory[] entities)
        {
        }
    }
}