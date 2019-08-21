namespace Administration.DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdminDbContext : DbContext
    {
        public AdminDbContext()
            : base("name=AdminDbContext")
        {
        }

        public virtual DbSet<TBL_SYS_GROUP_USERS> TBL_SYS_GROUP_USERS { get; set; }
        public virtual DbSet<TBL_SYS_GROUPS> TBL_SYS_GROUPS { get; set; }
        public virtual DbSet<TBL_SYS_PAGES> TBL_SYS_PAGES { get; set; }
        public virtual DbSet<TBL_SYS_PARAMETERS> TBL_SYS_PARAMETERS { get; set; }
        public virtual DbSet<TBL_SYS_PERMISSIONS> TBL_SYS_PERMISSIONS { get; set; }
        public virtual DbSet<TBL_SYS_PROVINCES> TBL_SYS_PROVINCES { get; set; }
        public virtual DbSet<TBL_SYS_ROLES> TBL_SYS_ROLES { get; set; }
        public virtual DbSet<TBL_SYS_UNIT_USERS> TBL_SYS_UNIT_USERS { get; set; }
        public virtual DbSet<TBL_SYS_UNITS> TBL_SYS_UNITS { get; set; }
        public virtual DbSet<TBL_SYS_USERS> TBL_SYS_USERS { get; set; }
        public virtual DbSet<TBL_SYS_DIMDATE> TBL_SYS_DIMDATE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.DateFullName)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.QuarterName)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.MonthName)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.WeekDayName)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.IsWorkDayDescription)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_SYS_DIMDATE>()
                .Property(e => e.IsPublicHolidayDescription)
                .IsUnicode(false);
        }
    }
}
