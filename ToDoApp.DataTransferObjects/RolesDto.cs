using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DataTransferObjects
{
   public class RolesDto
    {
      
        [Required(ErrorMessage ="Please Enter Role Name")]
        public string RoleName { get; set; }

    }
}
