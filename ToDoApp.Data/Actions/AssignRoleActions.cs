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
    public class AssignRoleActions : BaseActions<AspNetUserRole>
    {
        public override void Add(AspNetUserRole t, Guid by)
        {
            ctx.AspNetUserRoles.Add(t);
            Save();
        }

        public override void Change(AspNetUserRole t, Guid by)
        {
            Save();
        }

        public override void Delete(long id)
        {
           
            ctx.AspNetUserRoles.Where(x => x.UserId == id.ToString()).Delete();
            Save();
        }

        public override AspNetUserRole Get(long id)
        {
            return ctx.AspNetUserRoles.First(it => it.UserId == id.ToString());
        }

        public override IQueryable<AspNetUserRole> GetAll()
        {
            return ctx.AspNetUserRoles;
        }

        //public AspNetUserRole GetRoles(string id) {
        //    var Roles = ctx.AspNetUserRoles.SingleOrDefault(m => m.UserId == id); 
        //    return Roles;
        //}

        public void DeleteRole(string id)
        {
            
            ctx.AspNetUserRoles.Where(x => x.UserId == id).Delete();
            Save();
        }
    }
}
