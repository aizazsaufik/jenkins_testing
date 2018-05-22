using ToDoApp.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Model;
using System.Web;
using System.Net.Http;
using ToDoApp.Data.Actions;

namespace ToDoApp.Data
{
    public class DataAccess
    {

       
            internal ToDoEntities _ctx = null;
            static DataAccess self = null;

            private DataAccess()
            {
                var tmp = new ConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["ToDoConnection"].ConnectionString);
                _ctx = new ToDoEntities(tmp.ToDoAppConnectionString);

                // temporarily dissabling the EF entity validation - it should be removed in the next code sync.
                _ctx.Configuration.ValidateOnSaveEnabled = false;
            }
            public static DataAccess Instance
            {
                get
                {
                    var context = HttpContext.Current;
                    if (context != null)
                    {
                        const string kApplicationSettings = "ApplicationObject";
                        if (context != null && context.Items[kApplicationSettings] != null)
                        {
                            var da = context.Items[kApplicationSettings] as DataAccess;
                            return da;
                        }

                        self = new DataAccess();
                        context.Items[kApplicationSettings] = self;
                    }
                    else
                    {
                        self = new DataAccess();
                    }
                    return self;
                }
            }
            public void Save()
            {
                _ctx.SaveChanges();
            }
            internal ToDoEntities Ctx { get { return _ctx; } }
            public void Dispose()
            {
                if (self != null)
                {
                    //DataAccess.Instance.Dispose();
                    const string kApplicationSettings = "ApplicationObject";
                    var context = HttpContext.Current;
                    if (context != null && context.Items[kApplicationSettings] != null)
                        context.Items[kApplicationSettings] = null;
                    _ctx.Dispose();
                    _ctx = null;
                    GC.SuppressFinalize(this);
                    self = null;
                }
            }

            public ToDoEntities Context
            {
                get
                {
                    return this._ctx;
                }
            }
        public UsersActions UsersActions
        {
            get
            {
                UsersActions Ans = new UsersActions();
                Ans.SetContainer(this);
                return Ans;
            }
        }

        public RolesActions RolesActions
        {
            get
            {
                RolesActions Ans = new RolesActions();
                Ans.SetContainer(this);
                return Ans;
            }

        }
        public AssignRoleActions AssignUserRolesActions
        {
            get
            {
                AssignRoleActions Ans = new AssignRoleActions();
                Ans.SetContainer(this);
                return Ans;
            }
        }


    }

    
}
