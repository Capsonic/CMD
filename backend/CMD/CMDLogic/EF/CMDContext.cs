namespace CMDLogic.EF
{
    using Reusable;
    using System.Data.Entity;

    public partial class CMDContext : DbContext
    {
        public CMDContext()
            : base("name=CMDContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;        
        }

        public virtual DbSet<cat_ComparatorMethod> cat_ComparatorMethod { get; set; }
        public virtual DbSet<cat_MetricBasis> cat_MetricBasis { get; set; }
        public virtual DbSet<cat_MetricFormat> cat_MetricFormat { get; set; }
        public virtual DbSet<Dashboard> Dashboards { get; set; }
        public virtual DbSet<Gant> Gants { get; set; }
        public virtual DbSet<Initiative> Initiatives { get; set; }
        public virtual DbSet<Metric> Metrics { get; set; }
        public virtual DbSet<MetricYear> MetricYears { get; set; }
        public virtual DbSet<MetricHistory> MetricHistorys { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Sort> Sorts { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Gridster> Gridsters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cat_MetricBasis>()
                .HasMany(e => e.MetricYears)
                .WithOptional(e => e.cat_MetricBasis)
                .HasForeignKey(e => e.BasisKey);

            modelBuilder.Entity<cat_MetricFormat>()
                .HasMany(e => e.MetricYears)
                .WithOptional(e => e.cat_MetricFormat)
                .HasForeignKey(e => e.FormatKey);

            modelBuilder.Entity<Dashboard>()
                .HasMany(e => e.Departments)
                .WithMany(e => e.Dashboards)
                .Map(m => m.ToTable("cross_Dashboard_Department").MapLeftKey("DashboardKey").MapRightKey("DepartmentKey"));

            modelBuilder.Entity<Gant>()
                .Property(e => e.GantData)
                .IsUnicode(false);

            modelBuilder.Entity<Initiative>()
                .Property(e => e.ProgressValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Initiative>()
                .HasMany(e => e.Gants)
                .WithRequired(e => e.Initiative)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Initiative>()
                .HasMany(e => e.Departments)
                .WithMany(e => e.Initiatives)
                .Map(m => m.ToTable("cross_Department_Initiative").MapLeftKey("InitiativeKey").MapRightKey("DepartmentKey"));

            modelBuilder.Entity<MetricYear>()
                .Property(e => e.CurrentValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MetricYear>()
                .Property(e => e.GoalValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MetricYear>()
                .Property(e => e.AroundRangeValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Metric>()
                .HasMany(e => e.Departments)
                .WithMany(e => e.Metrics)
                .Map(m => m.ToTable("cross_Department_Metric").MapLeftKey("MetricKey").MapRightKey("DepartmentKey"));

            modelBuilder.Entity<MetricHistory>()
                .Property(e => e.CurrentValue)
                .HasPrecision(15, 4);

            #region Reusable
            modelBuilder.Entity<User>()
                .Property(e => e.Identicon64)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Sorts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Sort_User_ID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Gridsters)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Gridster_User_ID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tracks)
                .WithOptional(e => e.User_LastEditedBy)
                .HasForeignKey(e => e.User_LastEditedByKey);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tracks1)
                .WithOptional(e => e.User_RemovedBy)
                .HasForeignKey(e => e.User_RemovedByKey);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tracks2)
                .WithOptional(e => e.User_AssignedTo)
                .HasForeignKey(e => e.User_AssignedToKey);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tracks3)
                .WithOptional(e => e.User_AssignedBy)
                .HasForeignKey(e => e.User_AssignedByKey);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tracks4)
                .WithRequired(e => e.User_CreatedBy)
                .HasForeignKey(e => e.User_CreatedByKey);

            #endregion
        }
    }
}
