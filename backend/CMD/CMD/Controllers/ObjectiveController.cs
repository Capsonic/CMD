using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Objective")]
    public class ObjectiveController : BaseController<Objective>
    {
        public ObjectiveController(IObjectiveLogic logic) : base(logic) { }
    }
}