using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Metric")]
    public class MetricController : BaseController<Metric>
    {
        public MetricController(IMetricLogic logic) : base(logic) { }
    }
}