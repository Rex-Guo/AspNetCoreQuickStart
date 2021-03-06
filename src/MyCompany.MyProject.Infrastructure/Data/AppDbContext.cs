﻿using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Application.Entities.DemoAggregate;

namespace MyCompany.MyProject
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTime _dateTime;

        public AppDbContext(DbContextOptions options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Demo> Demo { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateBasicInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateBasicInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string typeName = typeof(IEntityTypeConfiguration<>).Name;
            var assembly = Assembly.GetExecutingAssembly();

            assembly.GetTypes()
                    .Where(w => w.GetTypeInfo().ImplementedInterfaces.Any(a => a.Name == typeName))
                    .ToList()
                    .ForEach(item =>
                    {
                        dynamic instance = item.FullName;
                        modelBuilder.ApplyConfiguration(assembly.CreateInstance(instance));
                    });
        }

        private void UpdateBasicInfo()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            Parallel.ForEach(entries, entry =>
            {
                if (entry.Entity is EntityBase auditableEntity)
                {
                    auditableEntity.UpdateBasicInfo(_dateTime);
                }
            });
        }
    }
}
