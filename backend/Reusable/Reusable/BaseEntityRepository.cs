using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using static Reusable.BaseEntity;

namespace Reusable
{
    public class BaseEntityRepository<T> : BaseRepository<T>, IEntityRepository<T> where T : BaseEntity
    {
        public BaseEntityRepository(DbContext context, ITrack track, int? byUserId = 1) : base(context, track, byUserId)
        {
        }
    }
}
