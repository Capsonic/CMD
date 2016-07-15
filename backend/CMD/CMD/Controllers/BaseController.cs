﻿using CMD.Auth;
using Newtonsoft.Json;
using Reusable;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMD.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class BaseController<Entity> : ApiController where Entity : BaseEntity
    {
        protected IBaseLogic<Entity> _logic;
        
        public BaseController(IBaseLogic<Entity> logic)
        {
            _logic = logic;

            LoggedUser loggedUser = new LoggedUser((ClaimsIdentity)User.Identity);
            _logic.byUserId = loggedUser.UserID;
            _logic.byUserId = 2;
        }

        // GET: api/Base
        [HttpGet Route("")]
        public CommonResponse Get()
        {
            return _logic.GetAll();
        }

        // GET: api/Base/5
        [HttpGet Route("")]
        public CommonResponse Get(int id)
        {
           return _logic.GetByID(id);
        }

        // GET: api/Base
        [HttpGet Route("getCatalogs")]
        public CommonResponse getCatalogs()
        {
            return _logic.GetCatalogs();
        }

        // POST: api/Base
        [HttpPost Route("")]
        public CommonResponse Post([FromBody]string value)
        {
            CommonResponse response = new CommonResponse();
            Entity entity;

            try
            {
                entity = JsonConvert.DeserializeObject<Entity>(value);

                return _logic.Add(entity);
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.Message, e);
            }
        }

        // POST: api/Base
        [HttpPost Route("AddToParent/{type}/{parentId}")]
        public CommonResponse AddToParent(string type, int parentId, [FromBody]string value)
        {
            CommonResponse response = new CommonResponse();

            Entity entity;
            try
            {
                Type parentType = Type.GetType("CMDLogic.EF." + type + ", CMDLogic", true);
                entity = JsonConvert.DeserializeObject<Entity>(value);

                MethodInfo method = _logic.GetType().GetMethod("AddToParent");
                MethodInfo generic = method.MakeGenericMethod(parentType);
                response = (CommonResponse)generic.Invoke(_logic, new object[] { parentId, entity });
                return response;
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.Message, e);
            }
        }

        [HttpPost Route("Create")]
        public CommonResponse Create()
        {
            return _logic.CreateInstance();
        }

        // PUT: api/Base/5
        public CommonResponse Put(int id, [FromBody]string value)
        {
            CommonResponse response = new CommonResponse();
            Entity entity;

            try
            {
                entity = JsonConvert.DeserializeObject<Entity>(value);

                return _logic.Update(entity);
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.Message, e);
            }
        }

        // DELETE: api/Base/5
        public CommonResponse Delete(int id)
        {
            return _logic.Remove(id);
        }

        [HttpGet Route("GetAvailableForEntity/{sEntityType}/{id}")]
        public CommonResponse GetAvailableObjectivesForDashboard(string sEntityType, int id)
        {
            CommonResponse response = new CommonResponse();

            try
            {
                Type forEntityType = Type.GetType("CMDLogic.EF." + sEntityType + ", CMDLogic", true);
                
                MethodInfo method = _logic.GetType().GetMethod("GetAvailableFor");
                MethodInfo generic = method.MakeGenericMethod(forEntityType);
                response = (CommonResponse)generic.Invoke(_logic, new object[] { id });
                return response;
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.Message, e);
            }
        }

        [HttpPost Route("RemoveFromParent/{type}/{parentId}")]
        public CommonResponse RemoveFromParent(string type, int parentId, [FromBody]string value)
        {
            CommonResponse response = new CommonResponse();

            Entity entity;
            try
            {
                Type parentType = Type.GetType("CMDLogic.EF." + type + ", CMDLogic", true);
                entity = JsonConvert.DeserializeObject<Entity>(value);

                MethodInfo method = _logic.GetType().GetMethod("RemoveFromParent");
                MethodInfo generic = method.MakeGenericMethod(parentType);
                response = (CommonResponse)generic.Invoke(_logic, new object[] { parentId, entity });
                return response;
            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.Message, e);
            }
        }
    }
}
