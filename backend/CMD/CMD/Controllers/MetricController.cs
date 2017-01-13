using CMDLogic.EF;
using CMDLogic.Logic;
using Reusable;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Metric")]
    public class MetricController : BaseController<Metric>
    {
        public MetricController(IMetricLogic logic) : base(logic) { }

        // GET: api/Base
        [HttpGet Route("GetAll")]
        virtual public CommonResponse GetAll()
        {
            return ((IMetricLogic)_logic).GetAllWithNavigationProperties();
        }
    }
}