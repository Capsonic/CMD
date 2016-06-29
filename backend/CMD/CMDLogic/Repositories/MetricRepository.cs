using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IMetricRepository : IDocumentRepository<Metric> { }

    public class MetricRepository : BaseDocumentRepository<Metric>, IMetricRepository
    {
        
    }
}
