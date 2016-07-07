﻿using CMDLogic.EF;
using Reusable;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IObjectiveLogic : IBaseLogic<Objective> { }

    public class ObjectiveLogic : BaseLogic<Objective>, IObjectiveLogic
    {
        private readonly IRepository<Initiative> initiativeRepository;
        private readonly IRepository<Metric> metricRepository;

        public ObjectiveLogic(DbContext context, IRepository<Objective> repository, IRepository<Initiative> initiativeRepository, IRepository<Metric> metricRepository) : base(context, repository)
        {
            this.initiativeRepository = initiativeRepository;
            this.metricRepository = metricRepository;
        }

        protected override void loadNavigationProperties(DbContext context, IList<Objective> entities)
        {
            foreach (Objective item in entities)
            {
                item.Initiatives = initiativeRepository.GetListByParent<Objective>(item.id);
                item.Metrics = metricRepository.GetListByParent<Objective>(item.id);
            }
        }
    }
}