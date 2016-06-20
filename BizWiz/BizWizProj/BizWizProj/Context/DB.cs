using BizWizProj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BizWizProj.Context
{
    public class DB:DbContext
    {
        public DB()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DB>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public DbSet<ModelShift> ModelShifts { get; set; }
        public DbSet<ClosedShift> ShiftHistory { get; set; }
        public DbSet<OpenShift> ShiftInProgress { get; set; }
        public DbSet<BizUser> BizUsers { get; set; }
        public DbSet<StockItem> Stocks { get; set; }
        public DbSet<SystemNotices> Notices { get; set; }
    }
}