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
    class Initiative_CRUD : super_CRUD<Initiative>
    {
        public Initiative_CRUD()
        {
            sp_update = "udp_Initiative_ups";
        }

        public override void addParameters(Initiative entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@InitiativeKey", entity.id);
            DM.Load_SP_Parameters("@Description", entity.Description);
            DM.Load_SP_Parameters("@ProgressValue", entity.ProgressValue);
            DM.Load_SP_Parameters("@DueDate", entity.DueDate);
            DM.Load_SP_Parameters("@ActualDate", entity.ActualDate);
        }

        public override Initiative entityFromTableRow(DataRow row)
        {
            Initiative entity = new Initiative();

            entity.id = long.Parse(row["InitiativeKey"].ToString());
            entity.Description = row["Description"].ToString();
            entity.ProgressValue = row["ProgressValue"].ToString() == "" ? (decimal?)null : decimal.Parse(row["ProgressValue"].ToString());
            entity.DueDate = row["DueDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["DueDate"].ToString());
            entity.ActualDate = row["ActualDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["ActualDate"].ToString());
            return entity;
        }
    }

    class cross_Objective_Initiative_CRUD : superJunction_CRUD<Initiative, cross_Objective_Initiative>
    {
        public cross_Objective_Initiative_CRUD()
        {
            sp_update = "udp_cross_Objective_Initiative_ups";
            query_GetByParent = "SELECT * FROM vw_initiative"
                                + " WHERE ObjectiveKey = @parentKey"
                                + " AND User_ID = @userKey";
        }

        public override void addParameters(cross_Objective_Initiative entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@ID", entity.id);
            DM.Load_SP_Parameters("@ObjectiveKey", entity.ObjectiveKey);
            DM.Load_SP_Parameters("@InitiativeKey", entity.InitiativeKey);
        }

        public override Initiative entityFromTableRow(DataRow row)
        {
            Initiative_CRUD initiative_CRUD = new Initiative_CRUD();
            return initiative_CRUD.entityFromTableRow(row);
        }
    }
}
