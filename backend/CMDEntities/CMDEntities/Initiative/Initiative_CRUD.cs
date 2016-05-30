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
}
