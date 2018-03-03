using CoreTest.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CoreTest.Context
{
    public static class DbInitializer
    {
        public static void Initialize(CDEContext context)
        {
            // Using this prevents migration  for later use
            // This doesn't use migration  to update de DB
            //context.Database.EnsureCreated();
            context.Database.Migrate();

            if (context.AdminData.IsEmpty())
            {
                context.AdminData.Add(new Models.Entities.AdminData() { Password = "CdeAdminPwd2018".GetHashString(), DateCreated = DateTime.Now.ToUniversalTime(), DateModified = DateTime.Now.ToUniversalTime() });
                context.SaveChangesAsync();
            }
        }

        public static void UpdatePwd(CDEContext contex)
        {
            var adminData = contex.AdminData.FirstOrDefault();

            adminData.Password = "CdeAdminPwd2018".GetHashString();

            contex.SaveChanges();
        }
    }
}
