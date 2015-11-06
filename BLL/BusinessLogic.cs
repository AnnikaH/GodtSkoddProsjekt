using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class BusinessLogic : IBusinessLogic
    {
        private InDBGodtSkodd dal;

        public BusinessLogic()
        {
            dal = new DBGodtSkodd();
        }

        public BusinessLogic(InDBGodtSkodd stub)
        {
            dal = stub;
        }

        public bool DatabaseProductsFilled()
        {
            List<Product> products = dal.ListAllProducts();

            if (products.Count == 0)
                return false;

            return true;
        }

        public bool CreateDatabaseContent()
        {
            return dal.CreateDatabaseContent();
        }

        // ---------------------------- AdminUser -------------------------------

        public List<AdminUser> GetAdminUsers()
        {
            return dal.GetAdminUsers();
        }

        public AdminUser GetAdminUser(int id)
        {
            return dal.GetAdminUser(id);
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            return dal.GetAdminIdInDB(adminUser);
        }

        public bool AdminUserInDb(AdminUser adminUser)
        {
            return dal.AdminUserInDb(adminUser);
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            return dal.CreateAdminUser(adminUser);
        }

        public bool EditAdminUser(int id, AdminUser adminUser)
        {
            return dal.EditAdminUser(id, adminUser);
        }

        public bool DeleteAdminUser(int id)
        {
            return dal.DeleteAdminUser(id);
        }

        // -------------------------------- User -------------------------

        public User GetUser(int id)
        {
            return dal.GetUser(id);
        }

        public List<User> GetUsers()
        {
            return dal.GetUsers();
        }

        public bool CreateUser(User user)
        {
            return dal.CreateUser(user);
        }

        public bool EditUser(int id, User user)
        {
            return dal.EditUser(id, user);
        }

        public bool DeleteUser(int id)
        {
            return dal.DeleteUser(id);
        }

        // ------------------------------- Product --------------------------

        public Product GetProduct(int productId)
        {
            return dal.GetProduct(productId);
        }

        public List<Product> GetProducts()
        {
            return dal.ListAllProducts();
        }

        public bool CreateProduct(Product product)
        {
            return dal.CreateProduct(product);
        }

        public bool EditProduct(int id, Product product)
        {
            return dal.EditProduct(id, product);
        }

        public bool DeleteProduct(int id)
        {
            return dal.DeleteProduct(id);
        }

        // ----------------------------- Order and Orderline ----------------------------

        public List<Order> GetOrders(int userId)  // GetOrders from User ID
        {
            return dal.GetOrdersForUser(userId);
        }

        public Order GetOrder(int orderId)
        {
            return dal.getOrder(orderId);
        }

        public bool CreateOrder(Order order)
        {
            return dal.CreateOrder(order);
        }

        public bool EditOrder(int id, Order order)
        {
            return dal.EditOrder(id, order);
        }

        public bool DeleteOrder(int id)
        {
            return dal.DeleteOrder(id);
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