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
        private readonly IMetricLogic metricLogic;
        private readonly IRepository<Gridster> gridsterRepository;
        private readonly IRepository<Dashboard> dashboardRepository;

        public DepartmentLogic(DbContext context,
            IRepository<Department> repository,
            IRepository<Initiative> initiativeRepository,
            IMetricLogic metricLogic,
            IRepository<Gridster> gridsterRepository,
            IRepository<Dashboard> dashboardRepository) : base(context, repository)
        {
            this.initiativeRepository = initiativeRepository;
            this.metricLogic = metricLogic;
            this.gridsterRepository = gridsterRepository;
            this.dashboardRepository = dashboardRepository;
        }

        protected override void loadNavigationProperties(DbContext context, params Department[] entities)
        {
            initiativeRepository.byUserId = byUserId;
            metricLogic.byUserId = byUserId;
            gridsterRepository.byUserId = byUserId;

            foreach (Department item in entities)
            {
                //item.Dashboards = dashboardRepository.GetListByParent<Department>(item.id);
                item.Initiatives = initiativeRepository.GetListByParent<Department>(item.id);
                item.Metrics = (ICollection<Metric>)metricLogic.GetAllByParent<Department>(item.id).Result;

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

        protected override void onSaving(DbContext context, Department entity, BaseEntity parent = null)
        {
            //parent is a Dashboard
            if (parent != null)
            {
                bool isShared = (parent as Dashboard).IsShared;

                if (isShared)
                {
                    var arrOwners = (parent as Dashboard).Owners.Split(',');
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
                        throw new Exception("User not allowed to update this Dashboard.");
                    }
                }

                if (entity.InfoGridster != null)
                {
                    entity.InfoGridster.IsShared = isShared;
                    if (isShared)
                    {
                        entity.InfoGridster.Gridster_User_ID = null;
                    }
                    else
                    {
                        entity.InfoGridster.Gridster_User_ID = (int)byUserId;
                    }

                    entity.InfoGridster.Gridster_ManyToMany_ID = parent.id;
                    entity.InfoGridster.Gridster_Edited_On = DateTime.Now;
                    entity.InfoGridster.Gridster_Entity_ID = entity.id;
                    entity.InfoGridster.Gridster_Entity_Kind = entity.AAA_EntityName;


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


                foreach (var metric in entity.Metrics)
                {
                    if (metric.InfoSort != null)
                    {
                        metric.InfoSort.IsShared = isShared;

                        if (isShared)
                        {
                            metric.InfoSort.Sort_User_ID = null;
                        }
                        else
                        {
                            metric.InfoSort.Sort_User_ID = (int)byUserId;
                        }

                        metric.InfoSort.Sort_Edited_On = DateTime.Now;
                        metric.InfoSort.Sort_Entity_ID = metric.id;
                        metric.InfoSort.Sort_Entity_Kind = metric.AAA_EntityName;
                        metric.InfoSort.Sort_ParentInfo = "Dashboard_" + parent.id + "_Department_" + entity.id;


                        if (metric.InfoSort.id > 0)
                        {
                            context.Entry(metric.InfoSort).State = EntityState.Modified;
                        }
                        else
                        {
                            context.Entry(metric.InfoSort).State = EntityState.Added;
                        }
                        context.SaveChanges();
                    }
                }

                foreach (var initiative in entity.Initiatives)
                {
                    if (initiative.InfoSort != null)
                    {
                        initiative.InfoSort.IsShared = isShared;

                        if (isShared)
                        {
                            initiative.InfoSort.Sort_User_ID = null;
                        }
                        else
                        {
                            initiative.InfoSort.Sort_User_ID = (int)byUserId;
                        }

                        initiative.InfoSort.Sort_Edited_On = DateTime.Now;
                        initiative.InfoSort.Sort_Entity_ID = initiative.id;
                        initiative.InfoSort.Sort_Entity_Kind = initiative.AAA_EntityName;
                        initiative.InfoSort.Sort_ParentInfo = "Dashboard_" + parent.id + "_Department_" + entity.id;
                        initiative.InfoSort.Sort_User_ID = (int)byUserId;

                        if (initiative.InfoSort.id > 0)
                        {
                            context.Entry(initiative.InfoSort).State = EntityState.Modified;
                        }
                        else
                        {
                            context.Entry(initiative.InfoSort).State = EntityState.Added;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        protected override void onCreate(Department entity)
        {
            base.onCreate(entity);
            //entity.InfoGridster = new Gridster();
        }
    }
}