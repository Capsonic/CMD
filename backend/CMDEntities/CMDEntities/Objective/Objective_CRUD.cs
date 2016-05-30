using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Base_MNG;
using System.Data;

namespace CMDEntities
{
    class Objective_CRUD : super_CRUD<Objective>
    {
        public Objective_CRUD()
        {
            sp_update = "udp_Metric_ups";
        }
        public override void addParameters(Objective entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@ObjectiveKey", entity.id);
            DM.Load_SP_Parameters("@Title", entity.Title);
        }

        public override Objective entityFromTableRow(DataRow row)
        {
            Objective entity = new Objective();

            entity.id = long.Parse(row["ObjectiveKey"].ToString());
            entity.Title = row["Title"].ToString();
            return entity;
        }
    }

    //class Objective_Sequence_CRUD : superJunction_CRUD<Objective, cross_Objective_Sequence>
    //{
    //    public Objective_Sequence_CRUD()
    //    {
    //        query_GetByParent = "SELECT Objective.ObjectiveKey, " +
    //                            "       Objective.Title, " +
    //                            "       Sequence_Dashboard_Objective.UserKey, " +
    //                            "       Sequence_Dashboard_Objective.Sequence " +
    //                            "FROM cross_Dashboard_Objective " +
    //                            "INNER JOIN Sequence_Dashboard_Objective ON cross_Dashboard_Objective.ID = Sequence_Dashboard_Objective.cross_Dashboard_Objective_ID " +
    //                            "INNER JOIN Objective ON cross_Dashboard_Objective.ObjectiveKey = Objective.ObjectiveKey " +
    //                            "WHERE Objective.sys_status = 1 " +
    //                            "  AND cross_Dashboard_Objective.DashboardKey = @key " +
    //                            "  AND Sequence_Dashboard_Objective.UserKey = @userKey ";
    //    }
    //    public override void addParameters(cross_Objective_Sequence entity, ref SQL DM)
    //    {
    //        DM.Load_SP_Parameters("@ID", entity.id);
    //    }

    //    public override Objective entityFromTableRow(DataRow row)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
