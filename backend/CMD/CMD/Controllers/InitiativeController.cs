using CMDLogic.EF;
using CMDLogic.Logic;
using System.Web.Http;

namespace CMD.Controllers
{
    [RoutePrefix("api/Initiative")]
    public class InitiativeController : BaseController<Initiative>
    {
        public InitiativeController(IInitiativeLogic logic) : base(logic) { }
    }
}