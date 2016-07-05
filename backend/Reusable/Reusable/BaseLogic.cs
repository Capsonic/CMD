﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;

namespace Reusable
{
    public abstract class BaseLogic<Entity> where Entity : BaseEntity
    {
        protected int? byUserId = null;
        protected DbContext context;
        protected IRepository<Entity> repository;

        public BaseLogic(DbContext context, IRepository<Entity> repository)//, int? byUserId)
        {
            this.context = context;
            this.repository = repository;
            //this.byUserId = byUserId;
        }

        protected abstract void loadNavigationProperties(DbContext context, IList<Entity> entities);

        public CommonResponse Add(Entity entity)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

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
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse GetAll()
        {
            CommonResponse response = new CommonResponse();
            IList<Entity> entities;
            try
            {
                //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

                entities = repository.GetAll();

                loadNavigationProperties(context, entities);
            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entities);
        }

        public CommonResponse GetByID(int ID)
        {
            CommonResponse response = new CommonResponse();
            List<Entity> entities = new List<Entity>();
            try
            {

                //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

                Entity entity = repository.GetByID(ID);
                if (entity != null)
                {
                    entities.Add(entity);
                    loadNavigationProperties(context, entities);
                }
                return response.Success(entity);

            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }
        }

        public CommonResponse Remove(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

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
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }
            return response.Success(id);
        }

        public CommonResponse Update(Entity entity)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

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
            catch (Exception ex)
            {

                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse AddToParent<ParentType>(int parentID, Entity entity) where ParentType : BaseEntity
        {
            CommonResponse response = new CommonResponse();
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

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

                        repository.AddToParent<ParentType>(parentID, entity);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return response.Error(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entity);
        }

        public CommonResponse GetAllByParent<ParentType>(int parentID) where ParentType : BaseEntity
        {
            CommonResponse response = new CommonResponse();
            IList<Entity> entities;

            try
            {

                //var repository = RepositoryFactory.Create<Entity>(context, byUserId);

                entities = repository.GetListByParent<ParentType>(parentID);

                //MethodInfo method = repository.GetType().GetMethod("GetListByParent");
                //MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { typeof(ParentType) });
                //entities = (IList<Entity>) genericMethod.Invoke(repository, new object[] { parentID });

            }
            catch (Exception ex)
            {
                return response.Error(ex.Message);
            }

            return response.Success(entities);
        }
    }
}