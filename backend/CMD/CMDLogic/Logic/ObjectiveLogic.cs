﻿using CMDLogic.EF;
using Reusable;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IObjectiveLogic : IBaseLogic<Objective> { }

    public class ObjectiveLogic : BaseLogic<Objective>, IObjectiveLogic
    {
        private readonly IRepository<Initiative> initiativeRepository;
        private readonly IRepository<Metric> metricRepository;
        private readonly IRepository<Gridster> gridsterRepository;
        private readonly IRepository<Dashboard> dashboardRepository;

        public ObjectiveLogic(DbContext context,
            IRepository<Objective> repository,
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

        protected override void loadNavigationProperties(DbContext context, IList<Objective> entities)
        {
            initiativeRepository.byUserId = byUserId;
            metricRepository.byUserId = byUserId;
            gridsterRepository.byUserId = byUserId;

            foreach (Objective item in entities)
            {
                item.Dashboards = dashboardRepository.GetListByParent<Objective>(item.id);
                item.Initiatives = initiativeRepository.GetListByParent<Objective>(item.id);
                item.Metrics = metricRepository.GetListByParent<Objective>(item.id);
                item.InfoGridster = gridsterRepository.GetSingle(e => e.Gridster_Entity_ID == item.id
                                                                && e.Gridster_Entity_Kind == item.AAA_EntityName
                                                                && e.Gridster_User_ID == byUserId);
                
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

        protected override void onSaving(DbContext context, Objective entity)
        {
            if (entity.InfoGridster != null)
            {
                entity.InfoGridster.Gridster_Edited_On = DateTime.Now;
                entity.InfoGridster.Gridster_Entity_ID = entity.id;
                entity.InfoGridster.Gridster_Entity_Kind = entity.AAA_EntityName;
                entity.InfoGridster.Gridster_User_ID = (int)byUserId;

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

        protected override void onCreate(Objective entity)
        {
            base.onCreate(entity);
            //entity.InfoGridster = new Gridster();
        }
    }
}