using BackEnd.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Context.Mappings
{
    public class Agenda_EventMap : CoreEntityMap<Agenda_Event>
    {
        public Agenda_EventMap()
        {
            Property(x => x.Title)
                .HasMaxLength(200);
            Property(x => x.Description)
                .HasMaxLength(1000);
            Property(x => x.Date)
                .HasMaxLength(50);
        }
    }
}