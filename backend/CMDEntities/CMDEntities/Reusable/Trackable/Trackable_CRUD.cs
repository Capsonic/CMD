using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable
{
    class Trackable_CRUD : super_CRUD<Trackable>
    {
        public Trackable_CRUD()
        {
            sp_update = "udp_Track_ups";
        }
        public override Trackable entityFromTableRow(DataRow row)
        {
            Trackable entity = new Trackable();
            //entity.id = long.Parse(row["TrackKey"].ToString()); TrackKey won't be used.
            entity.Date_CreatedOn = DateTime.Parse(row["Date_CreatedOn"].ToString());
            entity.Date_EditedOn = row["Date_EditedOn"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_EditedOn"].ToString());
            entity.Date_RemovedOn = row["Date_RemovedOn"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_RemovedOn"].ToString());
            entity.Date_LastTimeUsed = row["Date_LastTimeUsed"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["Date_LastTimeUsed"].ToString());
            entity.User_CreatedBy = long.Parse(row["User_CreatedBy"].ToString());
            entity.User_LastEditedBy = row["User_LastEditedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_LastEditedBy"].ToString());
            entity.User_RemovedBy = row["User_RemovedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_RemovedBy"].ToString());
            entity.User_AssignedTo = row["User_AssignedTo"].ToString() == "" ? (long?)null : long.Parse(row["User_AssignedTo"].ToString());
            entity.User_AssignedBy = row["User_AssignedBy"].ToString() == "" ? (long?)null : long.Parse(row["User_AssignedBy"].ToString());
            entity.Entity_Kind = row["Entity_Kind"].ToString();
            entity.Entity_ID = long.Parse(row["Entity_ID"].ToString());
            return entity;
        }

        public override void addParameters(Trackable entity, ref Data_Base_MNG.SQL DM)
        {
            //DM.Load_SP_Parameters("@TrackKey", entity.id); TrackKey won't be used.
            DM.Load_SP_Parameters("@TransactionAction", entity.TransactionAction);
            DM.Load_SP_Parameters("@Date_EditedOn", entity.Date_EditedOn);
            DM.Load_SP_Parameters("@Date_RemovedOn", entity.Date_RemovedOn);
            DM.Load_SP_Parameters("@Date_LastTimeUsed", entity.Date_LastTimeUsed);
            DM.Load_SP_Parameters("@User_CreatedBy", entity.User_CreatedBy);
            DM.Load_SP_Parameters("@User_LastEditedBy", entity.User_LastEditedBy);
            DM.Load_SP_Parameters("@User_RemovedBy", entity.User_RemovedBy);
            DM.Load_SP_Parameters("@User_AssignedTo", entity.User_AssignedTo);
            DM.Load_SP_Parameters("@User_AssignedBy", entity.User_AssignedBy);
            DM.Load_SP_Parameters("@Entity_Kind", entity.Entity_Kind);
            DM.Load_SP_Parameters("@Entity_ID", entity.Entity_ID);
        }
    }
}
