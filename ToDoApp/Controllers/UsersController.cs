using LoggerDataProject;
using LoggerDataProject.Models;
using Loggers.Models;
using LoggersDataTransferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Loggers.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
       
        private LoggerDatabaseEntities db = new LoggerDatabaseEntities();
        [Authorize]
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult GetAllUsers() {

            var Users = DataAccess.Instance.UsersActions.GetAll().Select(x => new UsersDto
            {
               id=x.UniqueId,
                Guid=x.Id,
                Email=x.Email,
                Password=x.PasswordHash,
                

            }).ToList();
            return PartialView("_Users",Users);

        }

        public ActionResult _CreateUser(UsersDto ViewModel)
        {
            try
            {
                UsersDto model = new UsersDto();

                if (!ModelState.IsValid)
                {
                    return JavaScript("OnFailure();");
                }
                else
                {
                    DataAccess.Instance.UsersActions.AddUsers(ViewModel);
                    return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
                }
               
                
            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }
           
        }

        public ActionResult _EditUsers(int id)
        {
            try
            {
                UsersDto model = DataAccess.Instance.UsersActions.GetUsersForEdit(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult _DeleteUsers(int id)
        {
            try
            {
               
                DataAccess.Instance.UsersActions.Delete(id);
                return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }
        }
        [HttpPost]
        public ActionResult _EditUsers(UsersDto viewModel)
        {
           
            try
            {
                UsersDto model = new UsersDto();

                if (!ModelState.IsValid)
                {
                    return JavaScript("OnFailure();");
                }
                else
                {
                    AspNetUser models = DataAccess.Instance.UsersActions.Get(viewModel.id);
                    if (model != null)
                    {
                        models.Email = viewModel.Email;
                        models.PasswordHash = HashPassword( viewModel.Password);
                        DataAccess.Instance.UsersActions.Change(models, Guid.Empty);
                    }
                  
                    return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }
        }

        public ActionResult _AssignRoles(int UserId, string RoleId, string UserGuid) {
            var ExistRecord = DataAccess.Instance.AssignUserRolesActions.GetAll().Where(x => x.UserId == UserGuid).FirstOrDefault();
            AspNetUserRole roles = new AspNetUserRole();
            if (ExistRecord != null)
            {

                 DataAccess.Instance.AssignUserRolesActions.DeleteRole(UserGuid);
                 roles.UserId = UserGuid;
                 roles.RoleId = RoleId;
                 DataAccess.Instance.AssignUserRolesActions.Add(roles, Guid.Empty);



            }
            else {
                roles.UserId = UserGuid;
                roles.RoleId = RoleId;
                DataAccess.Instance.AssignUserRolesActions.Add(roles, Guid.Empty);
            }


            return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);


        }
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] bytes;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }
        //public AspNetUserRole GetRoles(string id)
        //{

        //    AspNetUserRole Roles = db.AspNetUserRoles.Find(id);

        //    return Roles;

        //}



    }
}