using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ToDoApp.DataTransferObjects
{
   public class AssignRolesDto
    {
        
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleNames { get; set; }
    }
}
