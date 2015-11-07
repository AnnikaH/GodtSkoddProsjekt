using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    public interface IBusinessLogic
    {
        bool DatabaseProductsFilled();
        bool CreateDatabaseContent();
        List<AdminUser> GetAdminUsers();
        AdminUser GetAdminUser(int id);
        int GetAdminIdInDB(AdminUser adminUser);
        bool AdminUserInDb(AdminUser adminUser);
        bool CreateAdminUser(AdminUser adminUser);
        bool EditAdminUser(int id, AdminUser adminUser);
        bool DeleteAdminUser(int id);
        User GetUser(int id);
        List<User> GetUsers();
        bool CreateUser(User user);
        bool EditUser(int id, User user);
        bool DeleteUser(int id);
        Product GetProduct(int productId);
        List<Product> GetProducts();
        bool CreateProduct(Product product);
        bool EditProduct(int id, Product product);
        bool DeleteProduct(int id);
        List<Order> GetOrders(int userId);
        Order GetOrder(int orderId);
        bool CreateOrder(Order order);
        bool EditOrder(int id, Order order);
        bool DeleteOrder(int id);
    }
}
