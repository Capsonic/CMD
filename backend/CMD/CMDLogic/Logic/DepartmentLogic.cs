using CMDLogic.EF;
using Reusable;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IDepartmentLogic : IBaseLogic<Department> { }

    public class DepartmentLogic : BaseLogic<Department>, IDepartmentLogic
    {
        private readonly IRepository<Initiative> initiativeRepository;
        private readonly IRepository<Metric> metricRepository;
        private readonly IRepository<Gridster> gridsterRepository;
        private readonly IRepository<Dashboard> dashboardRepository;

        public DepartmentLogic(DbContext context,
            IRepository<Department> repository,
            IRepository<Initiative> initiativeRepository,
            IRepository<Metric> metricRepository,
            IRepository<Gridster> gridsterRepository,
            IRepository<Dashboard> dashboardRepository) : base(context, repository)
        {
            this.initiativeRepository = initiativeRepository;
            this.metricRepository = metricRepository;
            this.gridsterRepository = gridsterRepository;
            this.dashboardRepository = dashboardRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Department> entities)
        {
            initiativeRepository.byUserId = byUserId;
            metricRepository.byUserId = byUserId;
            gridsterRepository.byUserId = byUserId;

            foreach (Department item in entities)
            {
                //item.Dashboards = dashboardRepository.GetListByParent<Department>(item.id);
                item.Initiatives = initiativeRepository.GetListByParent<Department>(item.id);
                item.Metrics = metricRepository.GetListByParent<Department>(item.id);
                                
                //if (item.InfoGridster == null)
                //{
                //    Gridster gridsterFromFirstCreator = gridsterRepository.GetSingle(e => e.Gridster_Entity_ID == item.id
                //                                                && e.Gridster_Entity_Kind == item.AAA_EntityName
                //                                                && e.Gridster_User_ID == item.InfoTrack.User_CreatedByKey);
                //    if (gridsterFromFirstCreator != null)
                //    {
                //        item.InfoGridster = (Gridster)gridsterFromFirstCreator.Clone();
                //        item.InfoGridster.Gridster_User_ID = (int)byUserId;
                //    }
                //}
            }
        }

        protected override void onSaving(DbContext context, Department entity, int? parentId = null)
        {
            if (entity.InfoGridster != null)
            {
                entity.InfoGridster.Gridster_ManyToMany_ID = parentId;
                entity.InfoGridster.Gridster_Edited_On = DateTime.Now;
                entity.InfoGridster.Gridster_Entity_ID = entity.id;
                entity.InfoGridster.Gridster_Entity_Kind = entity.AAA_EntityName;
                entity.InfoGridster.Gridster_User_ID = (int)byUserId;

                if (entity.InfoGridster.FontSize == 0 || entity.InfoGridster.FontSize == null)
                {
                    entity.InfoGridster.FontSize = 12m;
                }

                if (entity.InfoGridster.id > 0)
                {
                    context.Entry(entity.InfoGridster).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(entity.InfoGridster).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        protected override void onCreate(Department entity)
        {
            base.onCreate(entity);
            //entity.InfoGridster = new Gridster();
        }
    }
}