using CMDLogic.EF;
using CMDLogic.Logic;
using Reusable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMD.Controllers
{
    public class SampleController : BaseController<Dashboard>
    {
        public SampleController(IDashboardLogic logic) : base(logic)
        {
            CommonResponse response = logic.GetAll();
        }
    }
}