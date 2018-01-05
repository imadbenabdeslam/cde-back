using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Context
{
    public static class DbInitializer
    {
        public static void Initialize(CDEContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
