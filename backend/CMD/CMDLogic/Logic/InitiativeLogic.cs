using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public class InitiativeLogic : BaseLogic<Initiative>
    {
        public InitiativeLogic(DbContext context, BaseRepository<Initiative> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, IList<Initiative> entities)
        {
            var gantRepository = RepositoryFactory.Create<Gant>(context, byUserId);

            foreach (Initiative item in entities)
            {
                item.Gants = gantRepository.GetListByParent<Initiative>(item.ID);
            }
        }
    }
}
