using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Reusable;
using Reusable.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BusinessSpecificLogic.Logic
{
    public interface IUserLogic : IBaseLogic<User>
    {
    }

    public class UserLogic : BaseLogic<User>, IUserLogic
    {
        public UserLogic(DbContext context, IRepository<User> repository) : base(context, repository)
        {
        }

        protected override void onCreate(User entity)
        {
            entity.Role = "User";
        }

        protected override void onBeforeSaving(User entity, BaseEntity parent = null, OPERATION_MODE mode = OPERATION_MODE.NONE)
        {
            if (string.IsNullOrWhiteSpace(entity.Value))
            {
                throw new Exception("[User Name] is a required field.");
            }

            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                throw new Exception("[User Name] is a required field.");
            }
            if (entity.id == 0 || (entity.id > 0 && entity.ChangePassword))
            {
                if (string.IsNullOrWhiteSpace(entity.Password) || string.IsNullOrWhiteSpace(entity.ConfirmPassword))
                {
                    throw new Exception("[Password] and [Confirm Password] are required fields.");
                }

                if (entity.Password != entity.ConfirmPassword)
                {
                    throw new Exception("[Password] does not match with its confirmation.");
                }

                if (entity.Password.Length < 6)
                {
                    throw new Exception("[User Name] has to have at least 6 characters.");
                }
            }

            //Updating..
            if (entity.id > 0)
            {
                User originalUser = repository.GetByID(entity.id);
                if (originalUser == null)
                {
                    throw new Exception("Error. User to be updated not found (Table: Users).");
                }
                using (var authContext = new AuthContext())
                {
                    UserManager<IdentityUser> _userManager;
                    _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(authContext));

                    IdentityUser user = authContext.Users.FirstOrDefault(u => u.UserName == originalUser.UserName);
                    if (user == null)
                    {
                        throw new Exception("Error. User to be updated not found (Table: ASPNetUsers).");
                    }

                    user.UserName = entity.UserName;
                    //password was updated:
                    if (!string.IsNullOrWhiteSpace(entity.Password))
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(entity.Password);
                    }

                    var result = _userManager.Update(user);
                    if (result == null || !result.Succeeded)
                    {
                        throw new Exception("An error has occurried when trying to update a user:\n" + result.Errors.First());
                    }
                }
            }
            //Creating..
            else
            {
                using (var authContext = new AuthContext())
                {
                    UserManager<IdentityUser> _userManager;
                    _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(authContext));

                    IdentityUser user = new IdentityUser
                    {
                        UserName = entity.UserName
                    };

                    var result = _userManager.Create(user, entity.Password);
                    if (result == null || !result.Succeeded)
                    {
                        string sError = result.Errors.First();
                        if (sError.EndsWith("is already taken."))
                        {
                            throw new Exception("Usuario is already taken.");
                        }
                        else
                        {
                            throw new Exception(sError);
                        }
                    }
                }
            }

            //Bitmap identicon;
            //try
            //{
            //    var bg = new StaticColorBrushGenerator(StaticColorBrushGenerator.ColorFromText(entity.UserName));
            //    identicon = new IdenticonGenerator("MD5")
            //        .WithSize(100, 100)
            //        .WithBackgroundColor(Color.White)
            //        .WithBlocks(4, 4)
            //        .WithBlockGenerators(IdenticonGenerator.ExtendedBlockGeneratorsConfig)
            //        .WithBrushGenerator(bg)
            //        .Create(entity.UserName);
            //}
            //catch (Exception ex)
            //{
            //    throw new KnownError("Ha ocurrido un error al intentar crear el usuario.");
            //}

            //ImageConverter converter = new ImageConverter();

            //try
            //{
            //    entity.Identicon64 = Convert.ToBase64String(ConvertBitMapToByteArray(identicon));
            //    entity.Identicon = (byte[])converter.ConvertTo(identicon, typeof(byte[]));
            //}
            //catch (Exception ex)
            //{
            //    throw new KnownError("Ha ocurrido un error al intentar crear el usuario");
            //}

        }
        //private byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        //{
        //    byte[] result = null;

        //    if (bitmap != null)
        //    {
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
        //            result = stream.ToArray();
        //        }
        //    }

        //    return result;
        //}

        //Specific for UserLogic
        public CommonResponse GetByName(string sName)
        {
            CommonResponse response = new CommonResponse();
            List<User> entities = new List<User>();
            try
            {
                User entity = repository.GetSingle(e => e.UserName == sName);
                if (entity != null)
                {
                    entities.Add(entity);
                    loadNavigationProperties(context, entities.ToArray());
                }
                return response.Success(entity);

            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.ToString());
            }
        }

        //protected override void onBeforeRemoving(User entity, BaseEntity parent = null)
        //{
        //    using (var authContext = new AuthContext())
        //    {
        //        #region Instead of Delete User, lets just disable it.

        //        UserManager<IdentityUser> _userManager;
        //        _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(authContext));

        //        IdentityUser user = authContext.Users.FirstOrDefault(u => u.UserName == entity.UserName);
        //        if (user == null)
        //        {
        //            throw new Exception("Error. User to be deleted not found (Table: ASPNetUsers).");
        //        }

        //        var result = _userManager.SetLockoutEnabled(user.Id, true);
        //        if (result == null || !result.Succeeded)
        //        {
        //            throw new Exception("An error has occurried when trying to delete a user.");
        //        }

        //        result = _userManager.SetLockoutEndDate(user.Id, DateTime.Now.AddYears(10));
        //        if (result == null || !result.Succeeded)
        //        {
        //            throw new Exception("An error has occurried when trying to delete a user.");
        //        }
        //        #endregion
        //    }
        //}

        protected override void loadNavigationProperties(DbContext context, params User[] entities)
        {
            //Empty
        }
    }
}
