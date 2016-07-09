using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : BaseController<Dashboard>
    {
        public DashboardController(IDashboardLogic logic) : base(logic) { }
    }
}