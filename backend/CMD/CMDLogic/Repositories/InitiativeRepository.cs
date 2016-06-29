using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IInitiativeRepository : IDocumentRepository<Initiative> { }

    public class InitiativeRepository : BaseDocumentRepository<Initiative>, IInitiativeRepository
    {
        
    }
}
