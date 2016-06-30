using CMDLogic.EF;
using CMDLogic.Reusable;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class InitiativeLogic : BaseLogic<InitiativeRepository, Initiative>
    {
        public InitiativeLogic(int? byUserId) : base(byUserId)
        {
        }

        protected override void loadNavigationProperties(MainContext context, IList<Initiative> entities)
        {
            var gantRepository = new GantRepository();
            gantRepository.context = context;

            foreach (Initiative item in entities)
            {
                item.Gants = gantRepository.GetListByParent<Initiative>(item.ID);
            }
        }
    }
}
