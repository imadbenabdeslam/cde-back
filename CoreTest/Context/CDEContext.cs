using CoreTest.Context.Mappings;
using CoreTest.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTest.Context
{
    public class CDEContext : DbContext
    {
        public CDEContext(DbContextOptions<CDEContext> options)
            : base(options)
        {

        }

        public DbSet<AgendaEvent> AgendaEvents { get; set; }
        public DbSet<AdminData> AdminData { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context.
        /// The default implementation of this method does nothing, but it can be overridden
        /// in a derived class such that the model can be further configured before it
        /// is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendaEventMap());
            modelBuilder.ApplyConfiguration(new AdminDataMap());

            base.OnModelCreating(modelBuilder);
        }

        #region Common
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// This method is called when all changes need to be persisted
        /// This overridden method automatically fills the create and modified date
        /// and the users that performed these actions
        /// </summary>
        public override int SaveChanges()
        {
            SetDates();
            return base.SaveChanges();
        }

        private void SetDates()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).DateCreated = DateTime.Now.ToUniversalTime();
                }

                ((Entity)entity.Entity).DateModified = DateTime.Now.ToUniversalTime();
            }
        }

        /// <summary>
        /// Returns a System.Data.Entity.DbSet instance for access to entities
        /// of the given type in the context and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        public virtual DbSet<TEntity> EntitySet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        } 

        public void Rollback()
        {
            var changedEntries = this.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }
        #endregion
    }
}
