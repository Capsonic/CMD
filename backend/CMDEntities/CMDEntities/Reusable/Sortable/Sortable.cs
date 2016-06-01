using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable
{
    interface Sortable
    {
        Sort SortInfo { get; set; }
    }

    class Sort : IEntity
    {
        public long Sort_Entity_ID { get; set; }
        public string Sort_Entity_Kind { get; set; }
        public long Sort_User_ID { get; set; }
        public DateTime Sort_Edited_On { get; set; }
        public long? Sort_Sequence { get; set; }

        //Aux:
        public string Sort_TransactionAction { get; set; }
        public bool SortMade { get; set; }

        public void copyFields(Sort entity)
        {
            Sort_Entity_ID = entity.Sort_Entity_ID;
            Sort_Entity_Kind = entity.GetType().Name;
            Sort_User_ID = entity.Sort_User_ID;
            Sort_Edited_On = entity.Sort_Edited_On;
            Sort_Sequence = entity.Sort_Sequence;
            Sort_TransactionAction = entity.Sort_TransactionAction;
        }

        public static void entityFromTableRow(DataRow row, Sort targetEntity)
        {
            //entity.id = long.Parse(row["SortKey"].ToString()); SortKey won't be used.
            targetEntity.Sort_Entity_ID = long.Parse(row["Sort_Entity_ID"].ToString());
            targetEntity.Sort_Entity_Kind = row["Sort_Entity_Kind"].ToString();
            targetEntity.Sort_User_ID = long.Parse(row["Sort_User_ID"].ToString());
            targetEntity.Sort_Edited_On = DateTime.Parse(row["Sort_Edited_On"].ToString());
            targetEntity.Sort_Sequence = row["Sort_Sequence"].ToString() == "" ? (long?)null : long.Parse(row["Sort_Sequence"].ToString());
        }
    }
}
