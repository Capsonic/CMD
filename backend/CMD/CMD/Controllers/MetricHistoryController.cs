using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/MetricHistory")]
    public class MetricHistoryController : BaseController<MetricHistory>
    {
        public MetricHistoryController(IMetricHistoryLogic logic) : base(logic) { }
    }
}