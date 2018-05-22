using EntityFramework.Extensions;
using ToDoApp.Data.Core;
using ToDoApp.Data.Model;
using ToDoApp.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Actions
{
    public class RolesActions : BaseActions<AspNetRole>
    {
        public override void Add(AspNetRole t, Guid by)
        {
            ctx.AspNetRoles.Add(t);
            Save();
        }

        public override void Change(AspNetRole t, Guid by)
        {
            Save();
        }

        public override void Delete(long id)
        {
            ctx.AspNetRoles.Where(x => x.unique_role_id == id).Delete();
            Save();
        }

        public override AspNetRole Get(long id)
        {
            return ctx.AspNetRoles.First(it => it.unique_role_id == id);
        }

        public override IQueryable<AspNetRole> GetAll()
        {
            return ctx.AspNetRoles;
        }

        public void AddRoles(RolesDto ViewModel)
        {
            try
            {
                Guid id = Guid.NewGuid();
                AspNetRole model = new AspNetRole();
                if (ViewModel != null)
                {
                    model.Id = id.ToString();
                  
                    model.Name = ViewModel.RoleName;
                    Add(model, Guid.Empty);
                }




            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public RoleDDLDto GetRolesForEdit(int id)
        {
            RoleDDLDto viewModel = new RoleDDLDto();
            if (id != 0)
            {
                AspNetRole model = Get(id);
                viewModel.uniqueid = model.unique_role_id;
                viewModel.RoleID = model.Id;
                viewModel.RoleName = model.Name;

            }
            return viewModel;

        }
    }
}
