namespace YazilimVarlikYonetimSistemi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<YazilimVarlikYonetimSistemi.Models.DataContext.YazilimVarlikYonetimSistemiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(YazilimVarlikYonetimSistemi.Models.DataContext.YazilimVarlikYonetimSistemiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
