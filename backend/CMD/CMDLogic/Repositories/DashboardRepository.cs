using CMDLogic.EF;
using CMDLogic.Reusable;

namespace CMDLogic
{
    public interface IDashboardRepository : IGenericDataRepository<Dashboard> { }

    public class DashboardRepository : GenericDocumentRepository<Dashboard>, IDashboardRepository { }
}
