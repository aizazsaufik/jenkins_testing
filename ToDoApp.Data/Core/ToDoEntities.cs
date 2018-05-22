using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Model
{
    public partial class ToDoEntities : DbContext
    {
        public ToDoEntities(string cnnstr) : base(cnnstr) { }
    }
}
