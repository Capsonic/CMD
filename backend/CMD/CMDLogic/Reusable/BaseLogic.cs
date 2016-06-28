using CMDLogic.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMDLogic.Reusable
{
    public abstract class BaseLogic<Repository, Entity>
        where Entity : BaseEntity
        where Repository : GenericEntityRepository<Entity>
    {
        protected abstract void loadNavigationProperties(MainContext context, IList<Entity> entities);
        protected abstract void attachParent(MainContext context, Entity entity, BaseEntity parent);

        protected IGenericDocumentRepository<Dashboard> ParentRepository = null;
        protected Type ParentType = null;

        public CommonResponse Add(Entity entity, int byUserID)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var context = new MainContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                            repository.context = context;
                            repository.byUserID = byUserID;

                            repository.Add(entity);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return response.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse GetAll(int? byUserID = null)
        {
            CommonResponse response = new CommonResponse();
            IList<Entity> entities;
            try
            {
                using (var context = new MainContext())
                {
                    Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                    repository.context = context;
                    repository.byUserID = byUserID;

                    entities = repository.GetAll();

                    loadNavigationProperties(context, entities);
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entities);
        }

        public CommonResponse GetByID(int ID, int? byUserID = null)
        {
            CommonResponse response = new CommonResponse();
            List<Entity> entities = new List<Entity>();
            try
            {
                using (var context = new MainContext())
                {
                    Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                    repository.context = context;
                    repository.byUserID = byUserID;

                    Entity entity = repository.GetByID(ID);
                    if (entity != null)
                    {
                        entities.Add(entity);
                        loadNavigationProperties(context, entities);
                    }
                    return response.Success(entity);
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }
        }

        public CommonResponse Remove(int byUserID, Entity entity)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var context = new MainContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                            repository.context = context;
                            repository.byUserID = byUserID;

                            if (repository is IGenericDocumentRepository<Entity>)
                            {
                                (repository as IGenericDocumentRepository<Entity>).Deactivate(entity);
                            }
                            else
                            {
                                repository.Delete(entity);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return response.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }
            return response.Success(entity);
        }

        public CommonResponse Update(int byUserID, Entity entity)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var context = new MainContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                            repository.context = context;
                            repository.byUserID = byUserID;

                            repository.Update(entity);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return response.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse AddToParent(int byUserID, int parentID, Entity entity)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var context = new MainContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                            repository.context = context;
                            repository.byUserID = byUserID;

                            if (ParentRepository == null)
                            {
                                response.Error("Entity " + entity.AAA_EntityName + " has no Parent Entity specified");
                            }

                            (ParentRepository as GenericEntityRepository<BaseEntity>).context = context;
                            (ParentRepository as GenericEntityRepository<BaseEntity>).byUserID = byUserID;

                            BaseEntity parent = (ParentRepository as GenericEntityRepository<BaseEntity>).GetByID(parentID);
                            if (parent == null)
                            {
                                return response.Error("Non-existent Parent Entity.");
                            }

                            context.Entry(parent).State = EntityState.Unchanged;

                            attachParent(context, entity, parent);
                            repository.Add(entity);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return response.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse GetAllByParent(int parentID, int? byUserID = null)
        {
            CommonResponse response = new CommonResponse();
            IList<Entity> entities;

            try
            {
                using (var context = new MainContext())
                {
                    if (ParentType == null)
                    {
                        return response.Error("Entity has no Parent Type specified");
                    }

                    Repository repository = (Repository)Activator.CreateInstance(typeof(Repository));
                    repository.context = context;
                    repository.byUserID = byUserID;
                    
                    MethodInfo method = repository.GetType().GetMethod("GetListByParent");
                    MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { ParentType });
                    entities = (IList<Entity>) genericMethod.Invoke(repository, new object[] { parentID });
                    //entities = repository.GetListByParent<ParentType.GetType()>(parentID);
                }
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entities);
        }
    }
}
