using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage.AlienTrick.Attributes
{
    public class RightsAttribute : AuthorizeAttribute
    {
        public bool AllowStudents { get; set; } = false;

        public bool AllowAdmins { get; set; } = true;

        public bool AllowSupervisors { get; set; } = true;

        public RightsAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
            {
                return false;
            }

            // dbcontext
            using (var context = new PortalEntities())
            {

                var user = context.WindowsUsersAndRoles.Where(u => u.WindowsUserAccount == httpContext.User.Identity.Name).FirstOrDefault();

                if (user == null)
                {
                    return false;
                }

                if (user.IsAdmin != 0 && AllowAdmins)
                {
                    return true;
                }

                if(user.IsStudent != 0)
                {
                    return true;
                }

                if(user.IsSupervisor != 0 && AllowAdmins)
                {
                    return true;
                }
            }



                return false;
        }
    }
}
