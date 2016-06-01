using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Base_MNG;
using System.Data;

namespace CMDEntities.Reusable
{
    class Sort_CRUD : super_CRUD<Sort>
    {
        public Sort_CRUD()
        {
            sp_update = "udp_Sort_ups";
        }

        public override void addParameters(Sort entity, ref SQL DM)
        {
            //DM.Load_SP_Parameters("@SortKey", entity.id); SortKey won't be used.
            DM.Load_SP_Parameters("@Sort_TransactionAction", entity.Sort_TransactionAction);
            DM.Load_SP_Parameters("@Sort_Entity_ID", entity.Sort_Entity_ID);
            DM.Load_SP_Parameters("@Sort_Entity_Kind", entity.Sort_Entity_Kind);
            DM.Load_SP_Parameters("@Sort_User_ID", entity.Sort_User_ID);
            DM.Load_SP_Parameters("@Sort_Edited_On", entity.Sort_Edited_On);
            DM.Load_SP_Parameters("@Sort_Sequence", entity.Sort_Sequence);
        }

        public override Sort entityFromTableRow(DataRow row)
        {
            Sort entity = new Sort();
            //entity.id = long.Parse(row["SortKey"].ToString()); SortKey won't be used.
            entity.Sort_Entity_ID = long.Parse(row["Sort_Entity_ID"].ToString());
            entity.Sort_Entity_Kind = row["Sort_Entity_Kind"].ToString();
            entity.Sort_User_ID = long.Parse(row["Sort_User_ID"].ToString());
            entity.Sort_Edited_On = DateTime.Parse(row["Sort_Edited_On"].ToString());
            entity.Sort_Sequence = row["Sort_Sequence"].ToString() == "" ? (long?)null : long.Parse(row["Sort_Sequence"].ToString());
            return entity;
        }
    }
}
