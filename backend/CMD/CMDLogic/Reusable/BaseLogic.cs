using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;

namespace CMDLogic.Reusable
{
    public abstract class BaseLogic<Repository, Entity>
        where Entity : BaseEntity
        where Repository : BaseEntityRepository<Entity>
    {
        protected abstract void loadNavigationProperties(MainContext context, IList<Entity> entities);

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

        public CommonResponse Remove(int byUserID, int id)
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

                            if (typeof(Entity).IsSubclassOf(typeof(BaseDocument)))
                            {
                                MethodInfo method = repository.GetType().GetMethod("Deactivate");
                                method.Invoke(repository, new object[] { id });
                            }
                            else
                            {
                                repository.Delete(id);
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
            return response.Success(id);
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

        public CommonResponse AddToParent<ParentType>(int byUserID, int parentID, Entity entity) where ParentType : BaseEntity
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

                            //var parentRepoType = typeof(BaseEntityRepository<>);
                            //Type[] parentRepositoryArgs = { typeof(ParentType) };
                            //var makeme = parentRepoType.MakeGenericType(parentRepositoryArgs);
                            //object parentRepository = Activator.CreateInstance(makeme);

                            //PropertyInfo propContext = parentRepository.GetType().GetProperty("context", BindingFlags.Public | BindingFlags.Instance);
                            //propContext.SetValue(parentRepository, context);

                            //PropertyInfo propByUser = parentRepository.GetType().GetProperty("byUserID", BindingFlags.Public | BindingFlags.Instance);
                            //propByUser.SetValue(parentRepository, byUserID);

                            //MethodInfo method = parentRepository.GetType().GetMethod("GetByID");
                            //BaseEntity parent = (Entity)method.Invoke(parentRepository, new object[] { parentID });
                            //if (parent == null)
                            //{
                            //    return response.Error("Non-existent Parent Entity.");
                            //}

                            DbSet<ParentType> parentSet = context.Set<ParentType>();
                            ParentType parent = parentSet.Find(parentID);
                            if (parent == null)
                            {
                                throw new Exception("Non-existent Parent Entity.");
                            }
                            if (parent is BaseDocument)
                            {
                                if ((parent as BaseDocument).sys_active == false)
                                {
                                    throw new Exception("Non-existent Parent Entity.");
                                }
                            }

                            context.Entry(parent).State = EntityState.Unchanged;

                            string navigationPropertyName = typeof(ParentType).Name + "s";

                            PropertyInfo navigationProperty = entity.GetType().GetProperty(navigationPropertyName, BindingFlags.Public | BindingFlags.Instance);
                            ICollection<ParentType> parentsList = (ICollection<ParentType>) navigationProperty.GetValue(entity);

                            parentsList.Add(parent);

                            if (entity.ID > 0)
                            {
                                repository.Update(entity);
                            }else
                            {
                                repository.Add(entity);
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

        public CommonResponse GetAllByParent<ParentType>(int parentID, int? byUserID = null) where ParentType : BaseEntity
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

                    entities = repository.GetListByParent<ParentType>(parentID);

                    //MethodInfo method = repository.GetType().GetMethod("GetListByParent");
                    //MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { typeof(ParentType) });
                    //entities = (IList<Entity>) genericMethod.Invoke(repository, new object[] { parentID });
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
