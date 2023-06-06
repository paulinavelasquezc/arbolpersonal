using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Identity { get; set; }        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public string ConfirmPassword { get; set; }
        public bool Active { get; set; }
        public DateTime UpdateDate { get; set; }

        public string MachineID { get; set; }
    }
}