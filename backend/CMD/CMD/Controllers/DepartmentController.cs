using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IDepartmentLogic logic) : base(logic) { }
    }
}