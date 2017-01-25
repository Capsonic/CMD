using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricYearLogic : IBaseLogic<MetricYear> { }

    public class MetricYearLogic : BaseLogic<MetricYear>, IMetricYearLogic
    {
        IRepository<cat_ComparatorMethod> cat_ComparatorMethodRepository;
        IRepository<cat_MetricBasis> cat_MetricBasisRepository;
        IRepository<cat_MetricFormat> cat_MetricFormatRepository;
        IRepository<Dashboard> cat_Dashboards;

        public MetricYearLogic(DbContext context, IRepository<MetricYear> repository,
            IRepository<cat_ComparatorMethod> cat_ComparatorMethodRepository,
            IRepository<cat_MetricBasis> cat_MetricBasisRepository,
            IRepository<cat_MetricFormat> cat_MetricFormatRepository,
            IRepository<Dashboard> cat_Dashboards
            ) : base(context, repository)
        {
            this.cat_ComparatorMethodRepository = cat_ComparatorMethodRepository;
            this.cat_MetricBasisRepository = cat_MetricBasisRepository;
            this.cat_MetricFormatRepository = cat_MetricFormatRepository;
            this.cat_Dashboards = cat_Dashboards;
        }

        protected override void loadNavigationProperties(DbContext context, params MetricYear[] entities)
        {
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

    }
}