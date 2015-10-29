using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DAL
{
    // Class Cities:
    public class Cities
    {
        [Key]
        public String PostalCode { get; set; }
        public String City { get; set; }

        public virtual List<Users> Users { get; set; }
    }

    // Class Products:
    public class Products
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public String Color { get; set; }
        public String Material { get; set; }
        public String Brand { get; set; }
        public String Url { get; set; }
        public String Gender { get; set; }  // Women, Men, Boys, Girls
        public String Type { get; set; }    // Boots, DressShoes, Sandals, Sneakers, WinterShoes

        public virtual List<Orderlines> Orderlines { get; set; }
    }

    // Class Users:
    public class Users
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String PostalCode { get; set; }  // foreign key from Cities
        public String UserName { get; set; }
        public byte[] Password { get; set; }

        public virtual Cities City { get; set; }
        public virtual List<Orders> Orders { get; set; }
    }

    // Class Orders:
    public class Orders
    {
        public int ID { get; set; }
        public int UserID { get; set; }  // foreign key from Users
        public DateTime Date { get; set; }

        public virtual Users User { get; set; }
        public virtual List<Orderlines> Orderlines { get; set; }    // Orderlines has OrderID as foreign key
    }

    // Class Orderlines:
    public class Orderlines
    {
        public int ID { get; set; }
        public int OrderID { get; set; }    // foreign key from Orders
        public int ProductID { get; set; }  // foreign key from Products
        public int Quantity { get; set; }
 
        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }

    // Class AdminUsers:
    public class AdminUsers
    {
        public int ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class DBContext : DbContext
    {
        public DBContext() : base("name=GodtSkodd")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orderlines> Orderlines { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Cities>().HasKey(c => c.PostalCode);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}