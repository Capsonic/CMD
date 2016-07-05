using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Track : BaseEntity, ITrack
    {
        [NotMapped]
        public override int ID { get { return TrackKey; } }
    }
}