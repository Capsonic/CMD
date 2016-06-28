using System.Data.Entity;
using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IDashboardRepository : IGenericDocumentRepository<Dashboard> { }

    public class DashboardRepository : GenericDocumentRepository<Dashboard>, IDashboardRepository
    {
        
    }
}
