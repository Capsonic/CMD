using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IDashboardRepository : IDocumentRepository<Dashboard> { }

    public class DashboardRepository : BaseDocumentRepository<Dashboard>, IDashboardRepository
    {
        
    }
}
