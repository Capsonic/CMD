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
    abstract class super_CRUD<T> where T : IEntity
    {
        protected ConnectionManager connectionManager = new ConnectionManager();
        protected Data_Base_MNG.SQL DM;

        public bool ErrorOccur = false;
        public string ErrorMessage = "";

        protected string query_GetByID = "";
        protected string query_GetAll = "";
        protected string query_GetByParent = "";
        protected string sp_update = "";
        public string query_SetActive = "";
        protected string query_DeleteByParent = "";
        protected string query_DeleteByID = "";
        protected string query_GetSingleByParent = "";
        protected string query_SetLock = "";

        abstract public T entityFromTableRow(System.Data.DataRow row);
        abstract public void addParameters(T entity, ref Data_Base_MNG.SQL DM);

        public string createAndReturnIdGenerated(T entity, ref Data_Base_MNG.SQL DM)
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

                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable_CRUD track_CRUD = new Trackable_CRUD();
                    Trackable trackEntity = new Trackable();
                    trackEntity.copyFields(entity as Trackable);
                    trackEntity.Entity_ID = long.Parse(idGenerated);
                    trackEntity.TransactionAction = "INSERT";
                    string idGenerated_Track = track_CRUD.createAndReturnIdGenerated(trackEntity, ref DM);
                    if (track_CRUD.ErrorOccur)
                    {
                        ErrorOccur = true;
                        ErrorMessage = track_CRUD.ErrorMessage;
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
                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable.entityFromTableRow(table.Rows[0], entity as Trackable);
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
                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable.entityFromTableRow(table.Rows[i], entity as Trackable);
                }
                recordset.Add(entity);
            }

            return recordset;
        }

        public List<T> readByParentID(long? id)
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
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    T entity = entityFromTableRow(table.Rows[i]);
                    if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                    {
                        Trackable.entityFromTableRow(table.Rows[i], entity as Trackable);
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
                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable.entityFromTableRow(table.Rows[0], entity as Trackable);
                }
            }
            else
            {
                return default(T);
            }

            return entity;
        }

        //public List<T> readByParentID(long? id, ref Data_Base_MNG.SQL DM)
        //{
        //    List<T> recordset = new List<T>();
        //    if (id == null)
        //    {
        //        return recordset;
        //    }

        //    DataTable table = new DataTable();
        //    DM.Execute_Query_Open_Connection(query_GetByParent);
        //    sqlCommand.Parameters.AddWithValue("@key", id);
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        //        sqlDataAdapter.Fill(table);

        //        for (int i = 0; i < table.Rows.Count; i++)
        //        {
        //            recordset.Add(entityFromTableRow(table.Rows[i]));
        //        }

        //    return recordset;
        //}

        public bool update(T entity, ref Data_Base_MNG.SQL DM)
        {
            ErrorOccur = false;
            bool result = false;
            try
            {
                if (entity is Stateful)
                {
                    T auxEntity = readByID(entity.id);
                    if ((auxEntity as Stateful).Entity_IsLocked == true)
                    {
                        DM.RollBack();
                        ErrorOccur = true;
                        ErrorMessage = "No more changes are allowed because entity is locked out.";
                        return false;
                    }
                }

                addParameters(entity, ref DM);
                result = DM.Execute_StoreProcedure_Open_Conn(sp_update, true);
                if (DM.ErrorOccur)
                {
                    ErrorOccur = true;
                    ErrorMessage = DM.Error_Mjs;
                    return false;
                }

                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable_CRUD track_CRUD = new Trackable_CRUD();
                    Trackable trackEntity = new Trackable();
                    trackEntity.copyFields(entity as Trackable);
                    trackEntity.Entity_ID = entity.id;
                    trackEntity.TransactionAction = ""; //Empty is Update.
                    trackEntity.Date_EditedOn = DateTime.Now;
                    track_CRUD.update(trackEntity, ref DM);
                    if (track_CRUD.ErrorOccur)
                    {
                        ErrorOccur = true;
                        ErrorMessage = track_CRUD.ErrorMessage;
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

        public bool setActive(T entity, bool bActive)
        {
            ErrorOccur = false;
            if (entity is Stateful)
            {
                T auxEntity = readByID(entity.id);
                if ((auxEntity as Stateful).Entity_IsLocked == true)
                {
                    ErrorOccur = true;
                    ErrorMessage = "No more changes are allowed because entity is locked out.";
                    return false;
                }
            }
            int rowsAffected = 0;
            SqlConnection sqlConnection = connectionManager.getConnection();
            SqlCommand sqlCommand = null;
            if (sqlConnection != null)
            {

                try
                {
                    sqlCommand = new SqlCommand(query_SetActive, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@key", entity.id);
                    sqlCommand.Parameters.AddWithValue("@bActive", bActive);
                    sqlConnection.Open();
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                        //{
                        //    Trackable_CRUD track_CRUD = new Trackable_CRUD();
                        //    Trackable trackEntity = new Trackable();
                        //    //trackEntity.copyFields(entity as Trackable);
                        //    if (bActive == true)
                        //    {
                        //        track_CRUD.query_SetActive = "UPDATE Track SET Date_RemovedOn = '" + DateTime.Now + "' WHERE Entity_ID = @key AND @bActive = @bActive";
                        //    }
                        //    else
                        //    {
                        //        track_CRUD.query_SetActive = "UPDATE Track SET Date_RemovedOn = null WHERE Entity_ID = @key AND @bActive = @bActive";
                        //    }
                        //    track_CRUD.setActive(entity as Trackable, bActive);
                        //    if (track_CRUD.ErrorOccur)
                        //    {
                        //        ErrorOccur = true;
                        //        ErrorMessage = track_CRUD.ErrorMessage;
                        //        return false;
                        //    }
                        //}
                        return true;
                    }
                    else
                    {
                        ErrorOccur = true;
                        ErrorMessage = "There were no rows affected.";
                        return true;
                    }
                }
                catch (Exception e)
                {
                    ErrorOccur = true;
                    ErrorMessage = e.Message;
                    //using return false below
                }
                finally
                {
                    sqlConnection.Dispose();
                    sqlCommand.Dispose();
                }
            }
            else
            {
                ErrorOccur = true;
                ErrorMessage = "Error. Could not connect to database.";
            }
            return false;
        }

        public CommonResponse setLock<S>(S entity, ref Data_Base_MNG.SQL DM) where S : Stateful
        {
            ErrorOccur = false;
            bool result = false;
            CommonResponse response = new CommonResponse();
            List<ValidationResult> validationResultList = new List<ValidationResult>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@IsLocked", entity.Entity_IsLocked));
                parameters.Add(new SqlParameter("@Status", entity.Entity_Status));
                parameters.Add(new SqlParameter("@key", (entity as IEntity).id));

                result = DM.Execute_Query_With_Parameters(query_SetLock, parameters);
                if (DM.ErrorOccur)
                {
                    ErrorOccur = true;
                    ErrorMessage = DM.Error_Mjs;

                    //Common Failed Response:
                    response.ErrorThrown = true;
                    response.ResponseDescription = DM.Error_Mjs;
                    validationResultList.Add((new ValidationResult
                    {
                        Description = DM.Error_Mjs,
                        EntityId = (entity as IEntity).id,
                        EntityKind = (entity as IEntity).AAA_EntityName
                    }));
                    response.Result = validationResultList;
                    return response;
                    //End Common Failed Response.
                }

                if (entity.GetType().IsSubclassOf(typeof(Trackable)))
                {
                    Trackable_CRUD track_CRUD = new Trackable_CRUD();
                    Trackable trackEntity = new Trackable();
                    trackEntity.copyFields(entity as Trackable);
                    trackEntity.Entity_ID = (entity as IEntity).id;
                    trackEntity.TransactionAction = ""; //Empty is Update.
                    trackEntity.Date_EditedOn = DateTime.Now;
                    track_CRUD.update(trackEntity, ref DM);
                    if (track_CRUD.ErrorOccur)
                    {
                        ErrorOccur = true;
                        ErrorMessage = track_CRUD.ErrorMessage;

                        //Common Failed Response:
                        response.ErrorThrown = true;
                        response.ResponseDescription = ErrorMessage;
                        validationResultList.Add((new ValidationResult
                        {
                            Description = ErrorMessage,
                            EntityId = trackEntity.id,
                            EntityKind = trackEntity.AAA_EntityName
                        }));
                        response.Result = validationResultList;
                        return response;
                        //End Common Failed Response.
                    }
                }
            }
            catch (Exception e)
            {
                ErrorOccur = true;
                ErrorMessage = e.Message;
                //Common Failed Response:
                response.ErrorThrown = true;
                response.ResponseDescription = ErrorMessage;
                validationResultList.Add((new ValidationResult
                {
                    Description = ErrorMessage,
                    EntityId = (entity as IEntity).id,
                    EntityKind = (entity as IEntity).AAA_EntityName
                }));
                response.Result = validationResultList;
                return response;
                //End Common Failed Response.
            }


            //Common Success Response:
            response.ErrorThrown = false;
            response.ResponseDescription = "Lock set to " + entity.Entity_IsLocked + " successfully.";
            response.Result = null;
            //End Common Success Response.
            return response;
        }

        //public bool setActive(long id, bool bActive)
        //{
        //    ErrorOccur = false;
        //    int rowsAffected = 0;
        //    SqlConnection sqlConnection = connectionManager.getConnection();
        //    SqlCommand sqlCommand = null;
        //    if (sqlConnection != null)
        //    {
        //        try
        //        {
        //            sqlCommand = new SqlCommand(query_SetActive, sqlConnection);
        //            sqlCommand.Parameters.AddWithValue("@key", id);
        //            sqlCommand.Parameters.AddWithValue("@bActive", bActive);
        //            sqlConnection.Open();
        //            rowsAffected = sqlCommand.ExecuteNonQuery();
        //            if (rowsAffected > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                ErrorOccur = true;
        //                ErrorMessage = "There were no rows affected.";
        //                return true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ErrorOccur = true;
        //            ErrorMessage = e.Message;
        //            //using return false below
        //        }
        //        finally
        //        {
        //            sqlConnection.Dispose();
        //            sqlCommand.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        ErrorOccur = true;
        //        ErrorMessage = "Error. Could not connect to database.";
        //    }
        //    return false;
        //}
    }
}
