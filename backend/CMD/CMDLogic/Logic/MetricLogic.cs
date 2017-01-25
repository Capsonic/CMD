using CMDLogic.EF;
using Reusable;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMDLogic.Logic
{
    public interface IMetricLogic : IBaseLogic<Metric>
    {
        CommonResponse GetAllWithNavigationProperties();
    }

    public class MetricLogic : BaseLogic<Metric>, IMetricLogic
    {
        IRepository<MetricYear> metricYearRepository;
        IRepository<MetricHistory> metricHistoryRepository;

        public MetricLogic(DbContext context, IRepository<Metric> repository,
            IRepository<MetricYear> metricYearRepository,
            IRepository<MetricHistory> metricHistoryRepository) : base(context, repository)
        {
            this.metricYearRepository = metricYearRepository;
            this.metricHistoryRepository = metricHistoryRepository;
        }

        protected override void loadNavigationProperties(DbContext context, params Metric[] entities)
        {
            foreach (var item in entities)
            {
                item.MetricYears = metricYearRepository.GetListByParent<Metric>(item.id);
                foreach (var year in item.MetricYears)
                {
                    year.MetricHistorys = metricHistoryRepository.GetListByParent<MetricYear>(year.id);
                }
            }
        }

        protected override void onSaving(DbContext context, Metric entity, BaseEntity parent = null)
        {
            //foreach (var year in entity.MetricYears)
            //{
            //    foreach (var item in year.MetricHistorys)
            //    {
            //        if (item.id < 1)
            //        {
            //            metricHistoryRepository.AddToParent<Metric>(entity.id, item);
            //        }
            //        else
            //        {
            //            if (item.EF_State == BaseEntity.EF_EntityState.Modified)
            //            {
            //                metricHistoryRepository.Update(item);
            //            }
            //            else if (item.EF_State == BaseEntity.EF_EntityState.Deleted)
            //            {
            //                metricHistoryRepository.Delete(item.id);
            //            }
            //        }
            //    }
            //}
        }

        public CommonResponse GetAllWithNavigationProperties()
        {
            CommonResponse response = new CommonResponse();
            List<Metric> entities;
            try
            {
                //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

                repository.byUserId = byUserId;
                entities = (List<Metric>)repository.GetAll();

                loadNavigationProperties(context, entities.ToArray());
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.ToString());
            }

            return response.Success(entities);
        }
    }
}