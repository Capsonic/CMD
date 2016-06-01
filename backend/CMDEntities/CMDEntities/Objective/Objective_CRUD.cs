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
            entity.DashboardKey = row["DashboardKey"].ToString() == "" ? (long?)null : long.Parse(row["DashboardKey"].ToString());

            return entity;
        }
    }

    class cross_Dashboard_Objective_CRUD : superJunction_CRUD<Objective, cross_Dashboard_Objective>
    {
        public cross_Dashboard_Objective_CRUD()
        {
            sp_update = "udp_cross_Dashboard_Objective_ups";
            query_GetByParent = "SELECT * from vw_objective"
                                + " WHERE DashboardKey = @parentKey"
                                + " AND User_ID = @userKey";
        }

        public override void addParameters(cross_Dashboard_Objective entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@ID", entity.id);
            DM.Load_SP_Parameters("@DashboardKey", entity.DashboardKey);
            DM.Load_SP_Parameters("@ObjectiveKey", entity.ObjectiveKey);
        }

        public override Objective entityFromTableRow(DataRow row)
        {
            Objective_CRUD objective_CRUD = new Objective_CRUD();
            return objective_CRUD.entityFromTableRow(row);
        }
    }
}
