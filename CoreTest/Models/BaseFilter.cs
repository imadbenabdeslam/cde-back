using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Models
{
    public class BaseFilter
    {
        public int Page { get; set; }

        public int CountPerPage { get; set; }
    }
}
