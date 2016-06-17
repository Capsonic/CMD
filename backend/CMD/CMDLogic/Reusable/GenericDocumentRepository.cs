using CMDLogic.EF;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace CMDLogic.Reusable
{
    public class GenericDocumentRepository<T> : GenericDataRepository<T>, IGenericDocumentRepository<T> where T : BaseDocument
    {
        private readonly ITrackRepository _trackRepository = new TrackRepository();

        public override void Add(DbContext context, int? byUserID, params T[] items)
        {
            base.Add(context, byUserID, items);
            foreach (T entity in items)
            {
                //(entity as Trackable).InfoTrack = trackRepository.GetSingle(context, t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);
                Track track = new Track()
                {
                    Date_CreatedOn = DateTime.Now,
                    Entity_ID = entity.ID,
                    Entity_Kind = entity.AAA_EntityName,
                    User_CreatedBy = byUserID ?? 0
                };
                _trackRepository.Add(context, byUserID, track);
                entity.InfoTrack = track;
            }
            context.SaveChanges();
        }

        public override IList<T> GetAll(DbContext context, params Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> result = base.GetList(context, r => r.sys_active == true, navigationProperties);
            foreach (T item in result)
            {
                item.InfoTrack = _trackRepository.GetSingle(context, t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
            }
            return result;
        }

        public override IList<T> GetList(DbContext context, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> result;
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            result = dbQuery
                .AsNoTracking()
                .Where(where)
                .Where(r => r.sys_active == true)
                .ToList<T>();

            foreach (T item in result)
            {
                item.InfoTrack = _trackRepository.GetSingle(context, t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
            }
            return result;
        }

        public void SetActive(DbContext context, int? byUserID, bool bActive, params T[] items)
        {
            foreach (T item in items)
            {
                if (item.InfoTrack == null)
                {
                    item.InfoTrack = _trackRepository.GetSingle(context, t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
                }

                if (item.InfoTrack != null)
                {
                    item.InfoTrack.Date_RemovedOn = DateTime.Now;
                    item.InfoTrack.User_RemovedBy = byUserID;
                                        
                    _trackRepository.Update(context, byUserID, item.InfoTrack);
                }

                item.sys_active = bActive;
                context.Entry(item).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
