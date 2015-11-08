using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryStub : DAL.InDBGodtSkodd
    {


        //------------------------------------------ Admin ---------------------------------

        public List<AdminUser> GetAdminUsers()
        {
            var adminList = new List<AdminUser>();
            var adminUser = new AdminUser()
            {
                id = 1,
                userName = "Test",
                password = "Testing1"
            };

            adminList.Add(adminUser);
            adminList.Add(adminUser);
            adminList.Add(adminUser);

            return adminList;
        }

        public AdminUser GetAdminUser(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                var adminUser = new AdminUser()
                {
                    id = 1,
                    userName = "Test",
                    password = "Testing1"

                };
                return adminUser;
            }
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            if (adminUser.userName == "")
            {
                var aUser = new AdminUser();
                aUser.id = -1; 
                return aUser.id;

            }

            else
            {
                var aUser = new AdminUser()
                {
                    id = 1,
                    userName = "Test",
                    password = "Testing1"

                };
                return aUser.id;

            }
        }

        public bool AdminUserInDb(AdminUser inputUser)
        {
            if (inputUser.userName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            if(adminUser.userName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EditAdminUser(int id, AdminUser adminUser)
        {
            if(adminUser.userName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteAdminUser(int id)
        {
            if(id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //------------------------------------------ Users ---------------------------------

        public bool CreateUser(User user)
        {
            if (user.firstName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

         

    public bool UserInDb(LoginUser inputUser)
        {
            //Function for checking if its the correct input for logging in
            if(inputUser.userName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        // Alternative: get the ID for the User in the database
        public int GetUserIdInDB(LoginUser loginUser)
        {
            //Function for checking if its the correct input for logging in
            //and returning the corresponding UserID in the database (not LoginUser, but Users)

            if(loginUser.userName == "")
            {
                var user = new User();
                user.id = 0;
                return user.id;

            }

            else
            {
                var user = new User()
                {
                    id = 1,
                    firstName = "Test",
                    lastName = "Testen",
                    address = "Testveien 1",
                    postalCode = "1234",
                    city = "Test",
                    email= "test@test.test",
                    phoneNumber = "12345678", 
                    userName = "Test",
                    password = "Testing"

                };
                return user.id;

            }

        }

        public bool EditUser(int id, User inputUser)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteUser(int id, User inputUser)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteUser(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<User> GetUsers()
        {
            var userList = new List<User>();
            var user = new User()
            {
                id = 1,
                firstName = "Test",
                lastName = "Testen",
                address = "Testveien 1",
                postalCode = "1234",
                city = "Test",
                email = "test@test.test",
                phoneNumber = "12345678",
                userName = "Test",
                password = "Testing1"

            };

            userList.Add(user);
            userList.Add(user);
            userList.Add(user);

            return userList;
        }

        public User GetUser(int id)
        {
            if (id == -1)
            {
                return null;
            }
            else
            {
                var user = new User()
                {
                    id = 1,
                    firstName = "Test",
                    lastName = "Testen",
                    address = "Testveien 1",
                    postalCode= "1234",
                    city = "Test",
                    email = "test@test.test",
                    phoneNumber = "12345678",
                    userName = "Test",
                    password = "Testing1"
                    
                };
                return user;
            }

        }

        //------------------------------------------ PRODUCTS ---------------------------------

        public bool CreateProduct(Product product)
        {
            // Adding a new row in the database table Product for this Product:
            if (product.name == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EditProduct(int id, Product inputProduct)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public List<Product> ListAllProducts()
        {
            var productList = new List<Product>();
            var product = new Product()
            {
                id = 1,
                name = "Shoe",
                price = 499,
                size = 45,
                color = "Black",
                material = "Mesh",
                brand = "Brand",
                url = "/Here",
                gender = "Men",
                type = "Sneakers"
                

            };
            productList.Add(product);
            productList.Add(product);
            productList.Add(product);
            return productList;
        }

        public List<Product> ListProductsOfGender(String gender)    // F.ex. Men
        {
            var productList = new List<Product>();
            var product = new Product()
            {
                id = 1,
                name = "Shoe",
                price = 499,
                size = 45,
                color = "Black",
                material = "Mesh",
                brand = "Brand",
                url = "/Here",
                gender = "Men",
                type = "Boots"


            };

            
            if(product.gender == gender)
            {
                productList.Add(product);
                productList.Add(product);
                productList.Add(product);

            }
            return productList;
        }

        public List<Product> ListProductsOfType(String type)    // f.ex. Boots
        {
            var productList = new List<Product>();
            var product = new Product()
            {
                id = 1,
                name = "Shoe",
                price = 499,
                size = 45,
                color = "Black",
                material = "Mesh",
                brand = "Brand",
                url = "/Here",
                gender = "Men",
                type = "Boots"


            };


            if (product.type == type)
            {
                productList.Add(product);
                productList.Add(product);
                productList.Add(product);

            }
            return productList;
        }

        public Product GetProduct(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                var product = new Product()
                {
                    id = 1,
                    name = "Shoe",
                    price = 499,
                    size = 45,
                    color = "Black",
                    material = "Mesh",
                    brand = "Brand",
                    url = "/Here",
                    gender = "Men",
                    type = "Boots"
                };
                return product;
            }
        }

        // --------------------------------------------- ORDERS -------------------------------

        public bool CreateOrder(Order order)
        {
            if (order.id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public List<Order> GetOrders()
        {
            //Returns all orders. (for statistics?)
            DateTime dt = new DateTime();
            var orderLines = new List<Orderline>();
            var orderline = new Orderline()
            {
                id = 1,
                orderID = 1,
                productId = 1,
                quantity = 1
            };
            orderLines.Add(orderline);
            orderLines.Add(orderline);
            orderLines.Add(orderline);

            var orderList = new List<Order>();
            var order = new Order()
            {
                id = 1,
                userID = 1,
                date = dt,
                orderlines = orderLines

            };
            orderList.Add(order);
            orderList.Add(order);
            orderList.Add(order);

            return orderList;

        }

        public List<Order> GetOrdersForUser(int userId)
        {

            DateTime dt = new DateTime();
            var orderLines = new List<Orderline>();
            var orderline = new Orderline()
            {
                id = 1,
                orderID = 1,
                productId = 1,
                quantity = 1
            };
            orderLines.Add(orderline);
            orderLines.Add(orderline);
            orderLines.Add(orderline);


            var orderList = new List<Order>();
            var order = new Order()
            {
                id = 1,
                userID = 1,
                date = dt,
                orderlines = orderLines

            };

            if(order.userID == userId)
            { 
                orderList.Add(order);
                orderList.Add(order);
                orderList.Add(order);
            }
            return orderList;
        }

        public Order getOrder(int id)
        {
            DateTime dt = new DateTime();
            var orderLines = new List<Orderline>();
            if (id == 0)
            {
                var order = new Order();
                order.id = 0;
                return order;
            }
            else
            {
                var order = new Order()
                {
                    id = 1,
                    userID = 1,
                    date = dt,
                    orderlines = orderLines


                };
                return order;
            }
        }

        public bool EditOrder(int id, Order input)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteOrder(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CreateOrderline(Orderline input)
        {
            if(input.id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EditOrderline(int id, Orderline input)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteOrderline(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Orderline GetOrderline(int id)
        {
            
            
            if (id == 0)
            {
                var orderLine = new Orderline();
                orderLine.id = 0;
                return orderLine;
            }
            else
            {
                var orderLine = new Orderline()
                {
                    id = 1,
                    orderID = 1,
                    productId = 1,
                    quantity = 1
                };
                return orderLine;
            }


        }

        public bool CreateDatabaseContent()
        {
            return true;
        }

        public bool createDefaultAdmin()
        {
            return true;
        }
    }
}