using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Reusable
{
    public class BaseDocumentRepository<T> : BaseEntityRepository<T>, IDocumentRepository<T> where T : BaseDocument
    {
        //protected readonly BaseRepository<ITrack> _trackRepository;

        public BaseDocumentRepository(DbContext context, ITrack track, int? byUserId = 1) : base(context, track, byUserId)
        {
        }

        //public ITrack track { get; set; }

        ////public BaseDocumentRepository(DbContext context, BaseRepository<ITrack> trackRepository, int? byUserId = 1) : base(context, byUserId)
        //public BaseDocumentRepository(DbContext context, int? byUserId = 1) : base(context, byUserId)
        //{
        //    //_trackRepository = trackRepository;
        //}

        public override void Add(params T[] items)
        {
            base.Add(items);
            foreach (T entity in items)
            {
                //(entity as Trackable).InfoTrack = trackRepository.GetSingle(context, t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);
                track.Date_CreatedOn = DateTime.Now;
                track.Entity_ID = entity.ID;
                track.Entity_Kind = entity.AAA_EntityName;
                track.User_CreatedByKey = byUserId ?? 0;


                _trackRepository.Add(track);
                entity.InfoTrack = track;
            }
            context.SaveChanges();
        }

        public override IList<T> GetAll()
        {
            IList<T> result = base.GetList(r => r.sys_active == true);
            foreach (T item in result)
            {
                item.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
            }
            return result;
        }

        public override IList<T> GetList(Func<T, bool> where)
        {
            List<T> result;
            IQueryable<T> dbQuery = context.Set<T>();

            result = dbQuery
                .AsNoTracking()
                .Where(where)
                .Where(r => r.sys_active == true)
                .ToList<T>();

            foreach (T item in result)
            {
                item.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
            }
            return result;
        }

        public override T GetSingle(Func<T, bool> where)
        {
            T result = base.GetSingle(where);

            result.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == result.ID && t.Entity_Kind == result.AAA_EntityName);

            return result;
        }

        public override T GetByID(int ID)
        {
            T result = base.GetByID(ID);
            if (result != null && result.sys_active != false)
            {
                result.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == result.ID && t.Entity_Kind == result.AAA_EntityName);
                return result;
            }
            else
            {
                return null;
            }
        }

        public void Activate(int id)
        {
            DbSet<T> tSet = context.Set<T>();
            T entity = tSet.Find(id);
            if (entity != null)
            {
                entity.sys_active = true;
                context.Entry(entity).State = EntityState.Modified;

                entity.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);

                if (entity.InfoTrack != null)
                {
                    entity.InfoTrack.Date_EditedOn = DateTime.Now;
                    entity.InfoTrack.User_LastEditedByKey = byUserId;

                    entity.InfoTrack.Date_RemovedOn = null;
                    entity.InfoTrack.User_RemovedByKey = null;

                    _trackRepository.Update(entity.InfoTrack);
                }

                context.SaveChanges();
            }
            else
            {
                throw new Exception("Entity not found.");
            }
        }

        public void Deactivate(int id)
        {
            DbSet<T> tSet = context.Set<T>();
            T entity = tSet.Find(id);
            if (entity != null)
            {
                entity.sys_active = false;
                context.Entry(entity).State = EntityState.Modified;

                entity.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);

                if (entity.InfoTrack != null)
                {
                    entity.InfoTrack.Date_RemovedOn = DateTime.Now;
                    entity.InfoTrack.User_RemovedByKey = byUserId;

                    _trackRepository.Update(entity.InfoTrack);
                }

                context.SaveChanges();
            }
            else
            {
                throw new Exception("Entity not found.");
            }
        }

        public override void Update(params T[] items)
        {
            base.Update(items);
            foreach (T entity in items)
            {
                entity.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);

                if (entity.InfoTrack != null)
                {
                    entity.InfoTrack.User_LastEditedByKey = byUserId;
                    entity.InfoTrack.Date_EditedOn = DateTime.Now;

                    _trackRepository.Update(entity.InfoTrack);
                }
            }
            context.SaveChanges();
        }

        public override void Delete(int id)
        {
            throw new Exception("Cannot delete Document entities.");
        }

        public override IList<T> GetListByParent<P>(int parentID)
        {
            List<T> list = new List<T>();

            DbSet<P> setParent = context.Set<P>();

            P parent = setParent.Find(parentID);

            if (parent == null)
            {
                throw new Exception("Parent non-existent.");
            }

            string tName = typeof(T).Name + "s";
            list = context.Entry(parent).Collection<T>(tName)
                .Query()
                .AsNoTracking()
                .Where(e => e.sys_active == true)
                .ToList<T>();

            foreach (T item in list)
            {
                item.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == item.ID && t.Entity_Kind == item.AAA_EntityName);
            }

            return list;
        }

        public override T GetSingleByParent<P>(int parentID)
        {
            T entity = null;

            DbSet<P> setParent = context.Set<P>();

            P parent = setParent.Find(parentID);

            if (parent == null)
            {
                throw new Exception("Parent non-existent.");
            }

            string tName = typeof(T).Name;
            entity = context.Entry(parent).Reference<T>(tName).Query().FirstOrDefault();

            if (entity != null)
            {
                entity.InfoTrack = _trackRepository.GetSingle(t => t.Entity_ID == entity.ID && t.Entity_Kind == entity.AAA_EntityName);
            }

            return entity;
        }
    }
}
