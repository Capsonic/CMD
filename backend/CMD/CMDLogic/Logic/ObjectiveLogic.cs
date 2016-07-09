using CMDLogic.EF;
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

        public ObjectiveLogic(DbContext context,
            IRepository<Objective> repository,
            IRepository<Initiative> initiativeRepository,
            IRepository<Metric> metricRepository,
            IRepository<Gridster> gridsterRepository) : base(context, repository)
        {
            this.initiativeRepository = initiativeRepository;
            this.metricRepository = metricRepository;
            this.gridsterRepository = gridsterRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Objective> entities)
        {
            initiativeRepository.byUserId = byUserId;
            metricRepository.byUserId = byUserId;
            gridsterRepository.byUserId = byUserId;

            foreach (Objective item in entities)
            {
                item.Initiatives = initiativeRepository.GetListByParent<Objective>(item.id);
                item.Metrics = metricRepository.GetListByParent<Objective>(item.id);
                item.InfoGridster = gridsterRepository.GetSingle(e => e.Gridster_Entity_ID == item.id
                                                                && e.Gridster_Entity_Kind == item.AAA_EntityName
                                                                && e.Gridster_User_ID == byUserId);
            }
        }

        protected override void onSaving(DbContext context, Objective entity)
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

        protected override void onCreate(Objective entity)
        {
            base.onCreate(entity);
            entity.InfoGridster = new Gridster();
        }
    }
}