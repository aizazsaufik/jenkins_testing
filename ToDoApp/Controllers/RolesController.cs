using LoggerDataProject;
using LoggerDataProject.Models;
using LoggersDataTransferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loggers.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult _CreateRoles(RolesDto ViewModel)
        {
            try
            {
                RolesDto model = new RolesDto();

                if (!ModelState.IsValid)
                {
                    return JavaScript("OnFailure();");
                }
                else
                {
                    DataAccess.Instance.RolesActions.AddRoles(ViewModel);
                    return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }

        }
        public ActionResult GetAllRoles()
        {

            var Roles = DataAccess.Instance.RolesActions.GetAll().Select(x => new RoleDDLDto
            {
                uniqueid=x.unique_role_id,
                RoleID=x.Id,
               RoleName=x.Name
              


            }).ToList();
            return PartialView("_Roles", Roles);

        }
        public ActionResult GetAllRolesDDL()
        {

            var Roles = DataAccess.Instance.RolesActions.GetAll().Select(x => new RoleDDLDto
            {
                RoleID = x.Id,
                RoleName = x.Name



            }).ToList();
            return Json(new { key = true, Message = "",RoleDDL= Roles }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _EditRoles(int id) {
            try
            {
                RoleDDLDto rolemodel = DataAccess.Instance.RolesActions.GetRolesForEdit(id);
                return PartialView(rolemodel);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult _DeleteRoles(int id)
        {
            try
            {

                DataAccess.Instance.RolesActions.Delete(id);
                return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }
        }
        [HttpPost]
        public ActionResult _EditRoles(RoleDDLDto viewModel)
        {

            try
            {
                RoleDDLDto model = new RoleDDLDto();

                if (!ModelState.IsValid)
                {
                    return JavaScript("OnFailure();");
                }
                else
                {
                    AspNetRole models = DataAccess.Instance.RolesActions.Get(viewModel.uniqueid);
                    if (model != null)
                    {
                        models.Name = viewModel.RoleName;
                        
                        DataAccess.Instance.RolesActions.Change(models, Guid.Empty);
                    }

                    return Json(new { key = true, Message = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return JavaScript("OnFailure();");
            }
        }
    }

}