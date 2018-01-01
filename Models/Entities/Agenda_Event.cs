using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models.Entities
{
    public class Agenda_Event : Entity
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}