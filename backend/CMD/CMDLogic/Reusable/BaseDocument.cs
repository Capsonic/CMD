using CMDLogic.EF;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.Reusable
{
    public abstract class BaseDocument : BaseEntity, Trackable
    {
        [NotMapped]
        public Track InfoTrack { get; set; }

        public virtual bool sys_active { get; set; }
    }
}
