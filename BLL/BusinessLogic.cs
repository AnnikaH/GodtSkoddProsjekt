using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class BusinessLogic
    {
        public bool DatabaseProductsFilled()
        {
            var dal = new DBGodtSkodd();
            List<Product> products = dal.ListAllProducts();

            if (products.Count == 0)
                return false;

            return true;
        }

        public bool CreateDatabaseContent()
        {
            var dal = new DBGodtSkodd();
            return dal.CreateDatabaseContent();
        }

        // ---------------------------- AdminUser -------------------------------

        public List<AdminUser> GetAdminUsers()
        {
            var dal = new DBGodtSkodd();
            return dal.GetAdminUsers();
        }

        public AdminUser GetAdminUser(int id) {
            var dal = new DBGodtSkodd();
            return dal.GetAdminUser(id);
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.GetAdminIdInDB(adminUser);
        }

        public bool AdminUserInDb(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.AdminUserInDb(adminUser);
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.CreateAdminUser(adminUser);
        }

        public bool EditAdminUser(int id, AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.EditAdminUser(id, adminUser);
        }

        public bool DeleteAdminUser(int id)
        {
            var dal = new DBGodtSkodd();
            return dal.DeleteAdminUser(id);
        }

        // -------------------------------- User -------------------------

        public User GetUser(int id)
        {
            var dal = new DBGodtSkodd();
            return dal.GetUser(id);
        }

        public List<User> GetUsers()
        {
            var dal = new DBGodtSkodd();
            return dal.GetUsers();
        }

        public bool CreateUser(User user)
        {
            var dal = new DBGodtSkodd();
            return dal.CreateUser(user);
        }

        public bool EditUser(int id, User user)
        {
            var dal = new DBGodtSkodd();
            return dal.EditUser(id, user);
        }

        public bool DeleteUser(int id)
        {
            var dal = new DBGodtSkodd();
            return dal.DeleteUser(id);
        }

        // ------------------------------- Product --------------------------

        public Product GetProduct(int productId)
        {
            var dal = new DBGodtSkodd();
            return dal.GetProduct(productId);
        }

        public List<Product> GetProducts()
        {
            var dal = new DBGodtSkodd();
            return dal.ListAllProducts();
        }

        public bool CreateProduct(Product product)
        {
            var dal = new DBGodtSkodd();
            return dal.CreateProduct(product);
        }

        public bool EditProduct(int id, Product product)
        {
            var dal = new DBGodtSkodd();
            return dal.EditProduct(id, product);
        }

        public bool DeleteProduct(int id)
        {
            var dal = new DBGodtSkodd();
            return dal.DeleteProduct(id);
        }

/* ----------------- Fra Tor sitt eksempel (Lagdeling):

        public List<Kunde> hentAlle()
        {
            var KundeDAL = new KundeDAL();
            List<Kunde> alleKunder = KundeDAL.hentAlle();
            return alleKunder;
        }
        public bool settInn(Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.settInn(innKunde);
        }
        public bool endreKunde(int id, Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.endreKunde(id, innKunde);
        }
        public bool slettKunde(int slettId)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.slettKunde(slettId);
        }
        public Kunde hentEnKunde(int id)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.hentEnKunde(id);
        }
        */
    }
}