using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class ObjectiveLogic : BaseLogic<ObjectiveRepository, Objective>
    {
        protected override void loadNavigationProperties(MainContext context, IList<Objective> entities)
        {
            //
        }
    }
}
