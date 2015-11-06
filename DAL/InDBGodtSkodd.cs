using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface InDBGodtSkodd
    {

        List<AdminUser> GetAdminUsers();
        AdminUser GetAdminUser(int id);
        int GetAdminIdInDB(AdminUser adminUser);
        bool AdminUserInDb(AdminUser inputUser);
        bool CreateAdminUser(AdminUser adminUser);
        bool EditAdminUser(int id, AdminUser adminUser);
        bool DeleteAdminUser(int id);
        bool CreateUser(User user);
        bool UserInDb(LoginUser inputUser);
        //byte[] CreateHash(string inPassword);
        int GetUserIdInDB(LoginUser loginUser);
        bool EditUser(int id, User inputUser);
        bool DeleteUser(int id, User inputUser);
        bool DeleteUser(int id);
        User GetUser(int id);
        List<User> GetUsers();
        bool CreateProduct(Product product);
        bool EditProduct(int id, Product inputProduct);
        bool DeleteProduct(int id);
        List<Product> ListAllProducts();
        List<Product> ListProductsOfGender(String gender);
        List<Product> ListProductsOfType(String type);
        Product GetProduct(int id);
        bool CreateOrder(Order order);
        List<Order> GetOrders();
        List<Order> GetOrdersForUser(int userId);
        Order getOrder(int id);
        bool EditOrder(int id, Order input);
        bool DeleteOrder(int id);
        bool CreateOrderline(Orderline input);
        bool EditOrderline(int id, Orderline input);
        bool DeleteOrderline(int id);
        Orderline GetOrderline(int id);






    }
}
