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
        private readonly Repository<Sort> sortRepository;
        private readonly Repository<MetricHistory> metricHistoryRepository;
        private readonly Repository<User> userRepository;

        public DashboardLogic(DbContext context,
            IRepository<Dashboard> repository,
            IDepartmentLogic departmentLogic,
            Repository<Gridster> gridsterRepository,
            Repository<Sort> sortRepository,
            Repository<MetricHistory> metricHistoryRepository,
            Repository<User> userRepository) : base(context, repository)
        {
            this.departmentLogic = departmentLogic;
            this.gridsterRepository = gridsterRepository;
            this.sortRepository = sortRepository;
            this.metricHistoryRepository = metricHistoryRepository;
            this.userRepository = userRepository;
        }

        protected override void loadNavigationProperties(DbContext context, params Dashboard[] entities)
        {
            departmentLogic.byUserId = byUserId;

            foreach (Dashboard item in entities)
            {
                item.Departments = (ICollection<Department>)departmentLogic.GetAllByParent<Dashboard>(item.id).Result;
            }
        }

        protected override ICatalogContainer LoadCatalogs()
        {
            return new Catalogs()
            {
                Users = userRepository.GetAll()
            };
        }

        private class Catalogs : ICatalogContainer
        {
            public IList<User> Users { get; set; }
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
                if (dashboard.IsShared)
                {
                    department.InfoGridster = gridsterRepository.GetSingle(e => e.Gridster_Entity_ID == department.id
                                                                && e.Gridster_Entity_Kind == department.AAA_EntityName
                                                                && e.IsShared == true
                                                                && e.Gridster_ManyToMany_ID == dashboard.id);

                    foreach (var metric in department.Metrics)
                    {
                        metric.InfoSort = sortRepository.GetSingle(e => e.Sort_Entity_ID == metric.id
                                                                    && e.Sort_Entity_Kind == metric.AAA_EntityName
                                                                    && e.Sort_ParentInfo == "Dashboard_" + ID + "_Department_" + department.id
                                                                    && e.IsShared == true);
                        metric.MetricHistorys = metricHistoryRepository.GetList(m => m.MetricKey == metric.MetricKey);
                    }

                    foreach (var initiative in department.Initiatives)
                    {
                        initiative.InfoSort = sortRepository.GetSingle(e => e.Sort_Entity_ID == initiative.id
                                                                    && e.Sort_Entity_Kind == initiative.AAA_EntityName
                                                                    && e.Sort_ParentInfo == "Dashboard_" + ID + "_Department_" + department.id
                                                                    && e.IsShared == true);
                    }
                }
                else
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
                        metric.MetricHistorys = metricHistoryRepository.GetList(m => m.MetricKey == metric.MetricKey);
                    }

                    foreach (var initiative in department.Initiatives)
                    {
                        initiative.InfoSort = sortRepository.GetSingle(e => e.Sort_Entity_ID == initiative.id
                                                                    && e.Sort_Entity_Kind == initiative.AAA_EntityName
                                                                    && e.Sort_ParentInfo == "Dashboard_" + ID + "_Department_" + department.id
                                                                    && e.Sort_User_ID == byUserId);
                    }
                }



            }

            return response;
        }

        protected override void onSaving(DbContext context, Dashboard entity, BaseEntity parent = null)
        {
            if (entity.IsShared)
            {
                var arrOwners = entity.Owners.Split(',');
                bool userIsAllowed = false;
                foreach (var userKey in arrOwners)
                {
                    if (byUserId.ToString() == userKey.Trim())
                    {
                        userIsAllowed = true;
                        break;
                    }
                }
                if (!userIsAllowed)
                {
                    throw new System.Exception("User not allowed to update this Dashboard.");
                }
            }
        }
    }
}
