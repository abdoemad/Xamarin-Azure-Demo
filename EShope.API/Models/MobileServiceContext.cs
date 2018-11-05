using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using EShope.API.DataObjects;
using System;

namespace EShope.API.Models
{
    public class EShopeMobileServiceContext : DbContext //EntityContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "Name=EShope_TableConnectionString";

        public EShopeMobileServiceContext() : base(connectionStringName)
        {
            Database.Log = s => WriteLog(s);
        }
        public void WriteLog(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        //public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            //modelBuilder.Configurations.Add(new Product.ProductConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders).WithRequired(o => o.User).HasForeignKey<Guid>(o => o.UserId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems).WithRequired(o => o.Order).HasForeignKey(o => o.OrderId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OrderItem>()
               .HasRequired(i => i.Product).WithOptional().WillCascadeOnDelete();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
