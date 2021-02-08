using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YazilimVarlikYonetimSistemi.Models.Model;

namespace YazilimVarlikYonetimSistemi.Models.DataContext
{
    public class YazilimVarlikYonetimSistemiContext:DbContext
    {
        public YazilimVarlikYonetimSistemiContext():base("YazilimVarlikYonetimSistemi")
        {

        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Dependent> Dependent { get; set; }
        public DbSet<Infrastructure> Infrastructure { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Usage> Usage { get; set; }
        public DbSet<User> User { get; set; }
    }
}