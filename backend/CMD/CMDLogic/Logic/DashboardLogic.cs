using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;
using System;

namespace CMDLogic.Logic
{
    public interface IDashboardLogic : IBaseLogic<Dashboard>
    {
    }

    public class DashboardLogic : BaseLogic<Dashboard>, IDashboardLogic
    {
        private readonly IDepartmentLogic departmentLogic;
        private readonly Repository<Gridster> gridsterRepository;
        private readonly Repository<Sort> sortRepository;

        public DashboardLogic(DbContext context,
            IRepository<Dashboard> repository,
            IDepartmentLogic departmentLogic,
            Repository<Gridster> gridsterRepository,
            Repository<Sort> sortRepository) : base(context, repository)
        {
            this.departmentLogic = departmentLogic;
            this.gridsterRepository = gridsterRepository;
            this.sortRepository = sortRepository;
        }

        protected override void loadNavigationProperties(DbContext context, params Dashboard[] entities)
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

                foreach (var metric in department.Metrics)
                {
                    metric.InfoSort = sortRepository.GetSingle(e => e.Sort_Entity_ID == metric.id
                                                                && e.Sort_Entity_Kind == metric.AAA_EntityName
                                                                && e.Sort_ParentInfo == "Dashboard_" + ID + "_Department_" + department.id
                                                                && e.Sort_User_ID == byUserId);
                }

                foreach (var initiative in department.Initiatives)
                {
                    initiative.InfoSort = sortRepository.GetSingle(e => e.Sort_Entity_ID == initiative.id
                                                                && e.Sort_Entity_Kind == initiative.AAA_EntityName
                                                                && e.Sort_ParentInfo == "Dashboard_" + ID + "_Department_" + department.id
                                                                && e.Sort_User_ID == byUserId);
                }
            }

            return response;
        }
    }
}
