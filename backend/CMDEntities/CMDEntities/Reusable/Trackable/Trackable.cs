using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable
{
    class Trackable : IEntity
    {
        public long Entity_ID { get; set; }
        public string Entity_Kind { get; set; }
        public long User_CreatedBy { get; set; }
        public DateTime Date_CreatedOn { get; set; }
        public DateTime? Date_EditedOn { get; set; }
        public DateTime? Date_RemovedOn { get; set; }
        public DateTime? Date_LastTimeUsed { get; set; }
        public long? User_LastEditedBy { get; set; }
        public long? User_RemovedBy { get; set; }
        public long? User_AssignedTo { get; set; }
        public long? User_AssignedBy { get; set; }
        public string TransactionAction { get; set; }
        public bool AssignationMade { get; set; }

        public void copyFields(Trackable entity)
        {
            Entity_ID = entity.Entity_ID;
            Entity_Kind = entity.GetType().Name;
            User_CreatedBy = entity.User_CreatedBy;
            Date_CreatedOn = entity.Date_CreatedOn;
            Date_EditedOn = entity.Date_EditedOn;
            Date_RemovedOn = entity.Date_RemovedOn;
            Date_LastTimeUsed = entity.Date_LastTimeUsed;
            User_LastEditedBy = entity.User_LastEditedBy;
            User_RemovedBy = entity.User_RemovedBy;
            User_AssignedTo = entity.User_AssignedTo;
            User_AssignedBy = entity.User_AssignedBy;
            TransactionAction = entity.TransactionAction;
        }
        public static void entityFromTableRow(DataRow row, Trackable targetEntity)
        {
            //entity.id = long.Parse(row["TrackKey"].ToString()); TrackKey won't be used.
            targetEntity.Date_CreatedOn = DateTime.Parse(row["Date_CreatedOn"].ToString());
            targetEntity.Date_EditedOn = row["Date_EditedOn"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_EditedOn"].ToString());
            targetEntity.Date_RemovedOn = row["Date_RemovedOn"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_RemovedOn"].ToString());
            targetEntity.Date_LastTimeUsed = row["Date_LastTimeUsed"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_LastTimeUsed"].ToString());
            targetEntity.User_CreatedBy = long.Parse(row["User_CreatedBy"].ToString());
            targetEntity.User_LastEditedBy = row["User_LastEditedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_LastEditedBy"].ToString());
            targetEntity.User_RemovedBy = row["User_RemovedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_RemovedBy"].ToString());
            targetEntity.User_AssignedTo = row["User_AssignedTo"].ToString() == "" ? (long?)null : long.Parse(row["User_AssignedTo"].ToString());
            targetEntity.User_AssignedBy = row["User_AssignedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_AssignedBy"].ToString());
            targetEntity.Entity_Kind = row["Entity_Kind"].ToString();
            targetEntity.Entity_ID = long.Parse(row["Entity_ID"].ToString());
        }
    }
}
