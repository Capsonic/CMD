using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/MetricYear")]
    public class MetricYearController : BaseController<MetricYear>
    {
        public MetricYearController(IMetricYearLogic logic) : base(logic) { }
    }
}