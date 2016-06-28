using System.Data.Entity;
using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IObjectiveRepository : IGenericDocumentRepository<Objective> { }

    public class ObjectiveRepository : GenericDocumentRepository<Objective>, IObjectiveRepository
    {
        
    }
}
