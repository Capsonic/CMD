namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricValuesToMetricYear : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Metric", "ComparatorMethodKey", "dbo.cat_ComparatorMethod");
            DropForeignKey("dbo.Metric", "BasisKey", "dbo.cat_MetricBasis");
            DropForeignKey("dbo.Metric", "FormatKey", "dbo.cat_MetricFormat");
            DropIndex("dbo.Metric", new[] { "FormatKey" });
            DropIndex("dbo.Metric", new[] { "BasisKey" });
            DropIndex("dbo.Metric", new[] { "ComparatorMethodKey" });
            AddColumn("dbo.MetricYear", "CurrentValue", c => c.Decimal(precision: 15, scale: 4));
            AddColumn("dbo.MetricYear", "GoalValue", c => c.Decimal(precision: 15, scale: 4));
            AddColumn("dbo.MetricYear", "AroundRangeValue", c => c.Decimal(precision: 15, scale: 4));
            AddColumn("dbo.MetricYear", "FormatKey", c => c.Int());
            AddColumn("dbo.MetricYear", "BasisKey", c => c.Int());
            AddColumn("dbo.MetricYear", "ComparatorMethodKey", c => c.Int());
            CreateIndex("dbo.MetricYear", "FormatKey");
            CreateIndex("dbo.MetricYear", "BasisKey");
            CreateIndex("dbo.MetricYear", "ComparatorMethodKey");
            AddForeignKey("dbo.MetricYear", "ComparatorMethodKey", "dbo.cat_ComparatorMethod", "ComparatorMethodKey");
            AddForeignKey("dbo.MetricYear", "BasisKey", "dbo.cat_MetricBasis", "MetricBasisKey");
            AddForeignKey("dbo.MetricYear", "FormatKey", "dbo.cat_MetricFormat", "MetricFormatKey");
            DropColumn("dbo.Metric", "CurrentValue");
            DropColumn("dbo.Metric", "GoalValue");
            DropColumn("dbo.Metric", "FormatKey");
            DropColumn("dbo.Metric", "BasisKey");
            DropColumn("dbo.Metric", "ComparatorMethodKey");
            DropColumn("dbo.MetricHistory", "GoalValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MetricHistory", "GoalValue", c => c.Decimal(precision: 15, scale: 4));
            AddColumn("dbo.Metric", "ComparatorMethodKey", c => c.Int());
            AddColumn("dbo.Metric", "BasisKey", c => c.Int());
            AddColumn("dbo.Metric", "FormatKey", c => c.Int());
            AddColumn("dbo.Metric", "GoalValue", c => c.Decimal(precision: 15, scale: 4));
            AddColumn("dbo.Metric", "CurrentValue", c => c.Decimal(precision: 15, scale: 4));
            DropForeignKey("dbo.MetricYear", "FormatKey", "dbo.cat_MetricFormat");
            DropForeignKey("dbo.MetricYear", "BasisKey", "dbo.cat_MetricBasis");
            DropForeignKey("dbo.MetricYear", "ComparatorMethodKey", "dbo.cat_ComparatorMethod");
            DropIndex("dbo.MetricYear", new[] { "ComparatorMethodKey" });
            DropIndex("dbo.MetricYear", new[] { "BasisKey" });
            DropIndex("dbo.MetricYear", new[] { "FormatKey" });
            DropColumn("dbo.MetricYear", "ComparatorMethodKey");
            DropColumn("dbo.MetricYear", "BasisKey");
            DropColumn("dbo.MetricYear", "FormatKey");
            DropColumn("dbo.MetricYear", "AroundRangeValue");
            DropColumn("dbo.MetricYear", "GoalValue");
            DropColumn("dbo.MetricYear", "CurrentValue");
            CreateIndex("dbo.Metric", "ComparatorMethodKey");
            CreateIndex("dbo.Metric", "BasisKey");
            CreateIndex("dbo.Metric", "FormatKey");
            AddForeignKey("dbo.Metric", "FormatKey", "dbo.cat_MetricFormat", "MetricFormatKey");
            AddForeignKey("dbo.Metric", "BasisKey", "dbo.cat_MetricBasis", "MetricBasisKey");
            AddForeignKey("dbo.Metric", "ComparatorMethodKey", "dbo.cat_ComparatorMethod", "ComparatorMethodKey");
        }
    }
}
