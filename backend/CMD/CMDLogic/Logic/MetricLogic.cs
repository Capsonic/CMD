using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricLogic : IBaseLogic<Metric> { }

    public class MetricLogic : BaseLogic<Metric>, IMetricLogic
    {
        IRepository<cat_ComparatorMethod> cat_ComparatorMethodRepository;
        IRepository<cat_MetricBasis> cat_MetricBasisRepository;
        IRepository<cat_MetricFormat> cat_MetricFormatRepository;
        IRepository<Dashboard> cat_Dashboards;
        IRepository<MetricHistory> metricHistoryRepository;

        public MetricLogic(DbContext context, IRepository<Metric> repository,
            IRepository<cat_ComparatorMethod> cat_ComparatorMethodRepository,
            IRepository<cat_MetricBasis> cat_MetricBasisRepository,
            IRepository<cat_MetricFormat> cat_MetricFormatRepository,
            IRepository<Dashboard> cat_Dashboards,
            IRepository<MetricHistory> metricHistoryRepository) : base(context, repository)
        {
            this.cat_ComparatorMethodRepository = cat_ComparatorMethodRepository;
            this.cat_MetricBasisRepository = cat_MetricBasisRepository;
            this.cat_MetricFormatRepository = cat_MetricFormatRepository;
            this.cat_Dashboards = cat_Dashboards;
            this.metricHistoryRepository = metricHistoryRepository;
        }

        protected override void loadNavigationProperties(DbContext context, params Metric[] entities)
        {
            foreach (var item in entities)
            {
                item.MetricHistorys = metricHistoryRepository.GetListByParent<Metric>(item.id);
            }
        }
        
        protected override ICatalogContainer LoadCatalogs()
        {
            return new Catalogs()
            {
                ComparatorMethod = cat_ComparatorMethodRepository.GetAll(),
                MetricBasis = cat_MetricBasisRepository.GetAll(),
                MetricFormat = cat_MetricFormatRepository.GetAll(),
                Dashboards = cat_Dashboards.GetAll()
            };
        }

        private class Catalogs : ICatalogContainer
        {
            public IList<cat_ComparatorMethod> ComparatorMethod { get; set; }
            public IList<cat_MetricFormat> MetricFormat { get; set; }
            public IList<cat_MetricBasis> MetricBasis { get; set; }
            public IList<Dashboard> Dashboards { get; set; }
        }

        protected override void onSaving(DbContext context, Metric entity, BaseEntity parent = null)
        {
            foreach (var item in entity.MetricHistorys)
            {
                if (item.id < 1)
                {
                    metricHistoryRepository.AddToParent<Metric>(entity.id, item);
                }
            }
        }
    }
}