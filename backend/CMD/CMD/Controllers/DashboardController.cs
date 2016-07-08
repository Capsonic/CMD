using CMDLogic.EF;
using CMDLogic.Logic;

namespace CMD.Controllers
{
    public class DashboardController : BaseController<Dashboard>
    {
        public DashboardController(IDashboardLogic logic) : base(logic) { }
    }
}