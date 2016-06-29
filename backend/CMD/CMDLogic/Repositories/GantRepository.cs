using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IGantRepository : IDocumentRepository<Gant> { }

    public class GantRepository : BaseDocumentRepository<Gant>, IGantRepository
    {
        
    }
}
