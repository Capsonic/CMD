using System;

namespace Reusable
{
    interface Trackable
    {
        ITrack InfoTrack { get; set; }
    }

    public interface ITrack : IEntity
    {
        int TrackKey { get; set; }

        int Entity_ID { get; set; }

        string Entity_Kind { get; set; }

        int User_CreatedByKey { get; set; }

        DateTime Date_CreatedOn { get; set; }

        DateTime? Date_EditedOn { get; set; }

        DateTime? Date_RemovedOn { get; set; }

        DateTime? Date_LastTimeUsed { get; set; }

        int? User_LastEditedByKey { get; set; }

        int? User_RemovedByKey { get; set; }

        int? User_AssignedToKey { get; set; }

        int? User_AssignedByKey { get; set; }
    }
}
