using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackEnd.Context
{
    public class DBInitializer : CreateDatabaseIfNotExists<CDEContext>
    {
        protected override void Seed(CDEContext context)
        {
            SeedDb(context);

            base.Seed(context);
        }

        public void SeedDb(CDEContext context)
        {
            
        }
    }
}