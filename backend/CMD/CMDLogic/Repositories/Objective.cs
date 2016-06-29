using System.Data.Entity;
using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IObjectiveRepository : IDocumentRepository<Objective> { }

    public class ObjectiveRepository : BaseDocumentRepository<Objective>, IObjectiveRepository
    {
        
    }
}
