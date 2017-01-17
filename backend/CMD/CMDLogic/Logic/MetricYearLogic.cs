using CMDLogic.EF;
using Reusable;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricYearLogic : IBaseLogic<MetricYear> { }

    public class MetricYearLogic : BaseLogic<MetricYear>, IMetricYearLogic
    {
        public MetricYearLogic(DbContext context, IRepository<MetricYear> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, params MetricYear[] entities)
        {
        }
    }
}