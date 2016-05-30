using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Catalogs
{
    class Catalog_CRUD<T> : super_CRUD<T> where T : Catalog
    {
        private string catalogKind;
        public string CatalogKind
        {
            get { return this.catalogKind; }
            set
            {
                this.catalogKind = value;
                sp_update = "udp_cat_" + this.catalogKind + "_ups";
                query_GetAll = "SELECT * FROM cat_" + this.catalogKind;
            }
        }
        public override T entityFromTableRow(DataRow row)
        {
            T entity = (T)Activator.CreateInstance(typeof(T), new object[] { });
            entity.id = long.Parse(row[this.catalogKind + "Key"].ToString());
            entity.Value = row["Value"].ToString();
            return entity;
        }

        public override void addParameters(T entity, ref Data_Base_MNG.SQL DM)
        {
            DM.Load_SP_Parameters("@" + this.catalogKind + "Key", entity.id);
            DM.Load_SP_Parameters("@Value", entity.Value);
        }
    }
}
