using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class InitiativeLogic : BaseLogic<InitiativeRepository, Initiative>
    {
        protected override void loadNavigationProperties(MainContext context, IList<Initiative> entities)
        {
            //
        }
    }
}
