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
            throw new NotImplementedException();
        }

        public AdminUser GetAdminUser(int id)
        {
            throw new NotImplementedException();
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            throw new NotImplementedException();
        }

        public bool AdminUserInDb(AdminUser inputUser)
        {
            throw new NotImplementedException();
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            throw new NotImplementedException();
        }

        public bool EditAdminUser(int id, AdminUser adminUser)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAdminUser(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            if (id == 0)
            {
                var user = new User();
                user.id = 0;
                return user;
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
                    userName = "Test",
                    password = "Testing"
                    
                };
                return user;
            }

        }


        public List<User> getUsers()
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
                userName = "Test",
                password = "Testing"

            };

            userList.Add(user);
            userList.Add(user);
            userList.Add(user);

            return userList;
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
                var product = new Product();
                product.id = 0;
                return product;
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
            throw new NotImplementedException();
        }

        public bool EditOrder(int id, Order input)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrderline(Orderline input)
        {
            throw new NotImplementedException();
        }

        public bool EditOrderline(int id, Orderline input)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrderline(int id)
        {
            throw new NotImplementedException();
        }

        public Orderline GetOrderline(int id)
        {
            throw new NotImplementedException();
        }
    }
}