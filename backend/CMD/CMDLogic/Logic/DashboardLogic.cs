using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IDashboardLogic : IBaseLogic<Dashboard> { }

    public class DashboardLogic : BaseLogic<Dashboard>, IDashboardLogic
    {
        private readonly IDepartmentLogic departmentLogic;

        public DashboardLogic(DbContext context, IRepository<Dashboard> repository, IDepartmentLogic departmentLogic) : base(context, repository)
        {
            this.departmentLogic = departmentLogic;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Dashboard> entities)
        {
            departmentLogic.byUserId = byUserId;

            foreach (Dashboard item in entities)
            {
                item.Departments = (ICollection<Department>) departmentLogic.GetAllByParent<Dashboard>(item.id).Result;
            }
        }
    }
}
