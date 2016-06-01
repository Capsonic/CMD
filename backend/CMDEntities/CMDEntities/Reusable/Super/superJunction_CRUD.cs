using CMDEntities.Reusable.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Super
{
    /*
     * T is the Entity
     * J is the Junction Entity
     */
    abstract class superJunction_CRUD<T, J> where T : IEntity where J : IEntity
    {
        protected ConnectionManager connectionManager = new ConnectionManager();
        protected Data_Base_MNG.SQL DM;

        public bool ErrorOccur = false;
        public string ErrorMessage = "";


        protected string query_GetByID = "";
        protected string query_GetAll = "";
        protected string query_GetByParent = "";
        protected string sp_update = "";
        protected string query_SetActive = "";
        protected string query_DeleteByParent = "";
        protected string query_DeleteByID = "";
        protected string query_GetSingleByParent = "";

        abstract public T entityFromTableRow(System.Data.DataRow row);
        abstract public void addParameters(J entity, ref Data_Base_MNG.SQL DM);


        public string createAndReturnIdGenerated(J entity, ref Data_Base_MNG.SQL DM)
        {
            ErrorOccur = false;
            string idGenerated = "";

            try
            {
                entity.id = 0;
                addParameters(entity, ref DM);
                idGenerated = DM.Execute_StoreProcedure_Scalar_Open_Conn(sp_update, true);
                if (DM.ErrorOccur)
                {
                    ErrorOccur = true;
                    ErrorMessage = DM.Error_Mjs;
                    return "";
                }

                if (entity.GetType().IsSubclassOf(typeof(Sort)))
                {
                    Sort_CRUD sort_CRUD = new Sort_CRUD();
                    Sort sortEntity = new Sort();
                    sortEntity.copyFields(entity as Sort);
                    sortEntity.Sort_Entity_ID = long.Parse(idGenerated);
                    sortEntity.Sort_TransactionAction = "INSERT";
                    sortEntity.Sort_Edited_On = DateTime.Now;
                    string idGenerated_Sort = sort_CRUD.createAndReturnIdGenerated(sortEntity, ref DM);
                    if (sort_CRUD.ErrorOccur)
                    {
                        ErrorOccur = true;
                        ErrorMessage = sort_CRUD.ErrorMessage;
                        return "";
                    }
                }
            }
            catch (Exception e)
            {
                ErrorOccur = true;
                ErrorMessage = e.Message;
                return "";
            }

            return idGenerated;
        }

        public T readByID(long id)
        {
            ErrorOccur = false;
            T entity;

            DM = connectionManager.getDataManager();
            string query = query_GetByID + id;
            DataTable table = new DataTable();
            table = DM.Execute_Query(query);
            if (DM.ErrorOccur)
            {
                ErrorOccur = true;
                ErrorMessage = DM.Error_Mjs;
                return default(T);
            }

            if (table.Rows.Count > 0)
            {
                entity = entityFromTableRow(table.Rows[0]);
                if (entity.GetType().IsSubclassOf(typeof(Sortable)))
                {                    
                     Sort.entityFromTableRow(table.Rows[0], (entity as Sortable).SortInfo);                    
                }
            }
            else
            {
                return default(T);
            }

            return entity;
        }

        public List<T> readAll()
        {
            ErrorOccur = false;
            List<T> recordset = new List<T>();

            DM = connectionManager.getDataManager();

            DataTable table = new DataTable();
            table = DM.Execute_Query(query_GetAll);
            if (DM.ErrorOccur)
            {
                ErrorMessage = DM.Error_Mjs;
                ErrorOccur = true;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                T entity = entityFromTableRow(table.Rows[i]);
                if (entity.GetType().IsSubclassOf(typeof(Sortable)))
                {
                    Sort.entityFromTableRow(table.Rows[i], (entity as Sortable).SortInfo);
                }
                recordset.Add(entity);
            }
            return recordset;
        }

        public List<T> readByParentID(long? id) //, long? userKey = null)
        {
            List<T> recordset = new List<T>();
            if (id == null)
            {
                return recordset;
            }

            DataTable table = new DataTable();
            SqlConnection sqlConnection = connectionManager.getConnection();
            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(query_GetByParent, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@key", id);
                //if (userKey != null)
                //{
                //    sqlCommand.Parameters.AddWithValue("@userKey", id);
                //}
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    T entity = entityFromTableRow(table.Rows[i]);
                    if (entity.GetType().IsSubclassOf(typeof(Sortable)))
                    {
                        Sort.entityFromTableRow(table.Rows[i], (entity as Sortable).SortInfo);
                    }
                    recordset.Add(entity);
                }
            }
            return recordset;
        }

        public T readSingleByParentID(long parentId)
        {
            ErrorOccur = false;
            T entity;

            DM = connectionManager.getDataManager();
            string query = query_GetSingleByParent + parentId;
            DataTable table = new DataTable();
            table = DM.Execute_Query(query);

            if (DM.ErrorOccur)
            {
                ErrorOccur = true;
                ErrorMessage = DM.Error_Mjs;
                return default(T);
            }
            if (table.Rows.Count > 0)
            {
                entity = entityFromTableRow(table.Rows[0]);
                if (entity.GetType().IsSubclassOf(typeof(Sortable)))
                {
                    Sort.entityFromTableRow(table.Rows[0], (entity as Sortable).SortInfo);
                }
            }
            else
            {
                return default(T);
            }

            return entity;
        }

        public bool update(J entity, ref Data_Base_MNG.SQL DM)
        {
            ErrorOccur = false;
            bool result = false;
            try
            {
                addParameters(entity, ref DM);

                result = DM.Execute_StoreProcedure_Open_Conn(sp_update, true);

                if (DM.ErrorOccur)
                {
                    ErrorOccur = true;
                    ErrorMessage = DM.Error_Mjs;
                    return false;
                }

                if (entity.GetType().IsSubclassOf(typeof(Sort)))
                {
                    Sort_CRUD sort_CRUD = new Sort_CRUD();
                    Sort sortEntity = new Sort();
                    sortEntity.copyFields(entity as Sort);
                    sortEntity.Sort_Entity_ID = entity.id;
                    sortEntity.Sort_TransactionAction = ""; //Empty is Update.
                    sortEntity.Sort_Edited_On = DateTime.Now;
                    sort_CRUD.update(sortEntity, ref DM);
                    if (sort_CRUD.ErrorOccur)
                    {
                        ErrorOccur = true;
                        ErrorMessage = sort_CRUD.ErrorMessage;
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorOccur = true;
                ErrorMessage = e.Message;
                return false;
            }

            return result;
        }

        public bool deleteByParentID(long id, ref Data_Base_MNG.SQL DM)
        {
            ErrorOccur = false;
            string query = query_DeleteByParent + id;
            if (DM.Execute_Command_Open_Connection(query))
            {
                ErrorOccur = DM.ErrorOccur;
                ErrorMessage = DM.Error_Mjs;
                return true;
            }
            ErrorOccur = DM.ErrorOccur;
            ErrorMessage = DM.Error_Mjs;
            return false;
        }

        public bool deleteByID(long id, ref Data_Base_MNG.SQL DM)
        {
            ErrorOccur = false;
            string query = query_DeleteByID + id;
            if (DM.Execute_Command_Open_Connection(query))
            {
                ErrorOccur = DM.ErrorOccur;
                ErrorMessage = DM.Error_Mjs;
                return true;
            }
            ErrorOccur = DM.ErrorOccur;
            ErrorMessage = DM.Error_Mjs;
            return false;
        }
    }
}
