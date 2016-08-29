using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IDashboardLogic : IBaseLogic<Dashboard>
    {
    }

    public class DashboardLogic : BaseLogic<Dashboard>, IDashboardLogic
    {
        private readonly IDepartmentLogic departmentLogic;
        private readonly Repository<Gridster> gridsterRepository;

        public DashboardLogic(DbContext context, IRepository<Dashboard> repository, IDepartmentLogic departmentLogic, Repository<Gridster> gridsterRepository) : base(context, repository)
        {
            this.departmentLogic = departmentLogic;
            this.gridsterRepository = gridsterRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Dashboard> entities)
        {
            departmentLogic.byUserId = byUserId;

            foreach (Dashboard item in entities)
            {
                item.Departments = (ICollection<Department>)departmentLogic.GetAllByParent<Dashboard>(item.id).Result;
            }
        }

        public override CommonResponse GetByID(int ID)
        {
            CommonResponse response = base.GetByID(ID);
            if (response.ErrorThrown)
            {
                return response;
            }

            Dashboard dashboard = (Dashboard)response.Result;
            foreach (var department in dashboard.Departments)
            {
                department.InfoGridster = gridsterRepository.GetSingle(e => e.Gridster_Entity_ID == department.id
                                                                && e.Gridster_Entity_Kind == department.AAA_EntityName
                                                                && e.Gridster_User_ID == byUserId
                                                                && e.Gridster_ManyToMany_ID == dashboard.id);
            }

            return response;
        }
    }
}
