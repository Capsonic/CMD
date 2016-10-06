using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IInitiativeLogic : IBaseLogic<Initiative> { }

    public class InitiativeLogic : BaseLogic<Initiative>, IInitiativeLogic
    {
        private readonly IRepository<Gant> gantRepository;
        IRepository<Dashboard> cat_Dashboards;

        public InitiativeLogic(DbContext context, IRepository<Initiative> repository,
            IRepository<Gant> gantRepository,
            IRepository<Dashboard> cat_Dashboards) : base(context, repository)
        {
            this.gantRepository = gantRepository;
            this.cat_Dashboards = cat_Dashboards;
        }

        protected override void loadNavigationProperties(DbContext context, params Initiative[] entities)
        {
            foreach (Initiative item in entities)
            {
                item.Gants = gantRepository.GetListByParent<Initiative>(item.id);
            }
        }
        
        protected override ICatalogContainer LoadCatalogs()
        {
            return new Catalogs()
            {
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
