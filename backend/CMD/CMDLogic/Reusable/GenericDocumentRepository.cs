using CMDLogic.EF;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CMDLogic.Reusable
{
    public class GenericDocumentRepository<T> : GenericDataRepository<T> where T : BaseDocument
    {
        private readonly ITrackRepository trackRepository = new TrackRepository();

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
                trackRepository.Add(context, byUserID, track);
                entity.InfoTrack = track;
            }
            context.SaveChanges();
        }
    }
}
