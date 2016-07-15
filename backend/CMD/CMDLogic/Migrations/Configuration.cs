namespace CMDLogic.Migrations
{
    using EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CMDContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            #region Catalogs
            context.cat_ComparatorMethod.AddOrUpdate(
                new cat_ComparatorMethod() { ComparatorMethodKey = 1, Value = "Greater Than" },
                new cat_ComparatorMethod() { ComparatorMethodKey = 2, Value = "Less Than" },
                new cat_ComparatorMethod() { ComparatorMethodKey = 3, Value = "Around Than" }
            );

            context.cat_MetricFormat.AddOrUpdate(
                new cat_MetricFormat() { MetricFormatKey = 1, Value = "Numeric" },
                new cat_MetricFormat() { MetricFormatKey = 2, Value = "Currency" },
                new cat_MetricFormat() { MetricFormatKey = 3, Value = "Percentage" }
            );


            #endregion


        }
    }
}
