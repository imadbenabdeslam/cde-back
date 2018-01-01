﻿using BackEnd.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BackEnd.Context.Mappings
{
    public class CoreEntityMap<T> : EntityTypeConfiguration<T>
        where T : Entity
    {
        /// <summary>
        /// Creates a Table Mapping for the Core Entity
        /// </summary>
        public CoreEntityMap()
        {
            HasKey(x => x.ID);
            Property(x => x.ID)
                .HasColumnName("ID");
            Property(x => x.DateCreated)
                .HasColumnName("DateCreated");
            Property(x => x.DateModified)
                .HasColumnName("DateModified");
        }
    }
}