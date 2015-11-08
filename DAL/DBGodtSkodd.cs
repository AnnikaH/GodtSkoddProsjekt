using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

using System.Diagnostics;
using System.IO;

namespace DAL
{
    public class DBGodtSkodd : InDBGodtSkodd
    {
        string errorLogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";

        public bool CreateDatabaseContent()
        {
            return createProducts();
        }

        public bool createDefaultAdmin()
        {
            try
            {
                AdminUser Default = new AdminUser();
                List<AdminUser> firstlist = GetAdminUsers();

                if (firstlist.Find(a => a.userName == "Admin") == null)
                {
                    Default.userName = "Admin";
                    Default.password = "12345678";
                    CreateAdminUser(Default);
                }
                
                DBContext db = new DBContext();

                List<Users> list = db.Users.ToList();
                Users theOne = list.Find(a => a.FirstName == "Test");

                if (theOne == null)
                {
                    User newUser = new User();
                    newUser.firstName = "Test";
                    newUser.lastName = "Testesten";
                    newUser.city = "Test";
                    newUser.email = "Test@test.test";
                    newUser.phoneNumber = "12345678";
                    newUser.address = "Testveien 1";
                    newUser.postalCode = "1234";
                    newUser.userName = "Test";
                    newUser.password = "12345678";
                    CreateUser(newUser);
                }
                
                return true;
            }
            catch(Exception e)
            {
                writeToLog(e);
                return false;
            }
        }
        
        public void writeToLog(Exception e)
        {
            string errorMessage = e.Message.ToString() + " in " + e.TargetSite.ToString() + e.StackTrace.ToString();

            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string today = "" + day + "." + month + "." + year;
            string nowHour = DateTime.Now.Hour.ToString();
            string nowMinute = DateTime.Now.Minute.ToString();
            string todayFile = @"\Log " + today + ".txt";

            if (File.Exists(errorLogPath + todayFile))
            {
                using (StreamWriter outputFile = new StreamWriter("" + errorLogPath + todayFile, true))
                {
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + errorMessage);
                }
            }
            else
            {
                if (!Directory.Exists(errorLogPath))
                {
                    Directory.CreateDirectory(errorLogPath);
                }
                using (StreamWriter outputFile = new StreamWriter("" + errorLogPath + todayFile))
                {
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + errorMessage);
                }
            }
        }

        // ----------------------------- AdminUser ---------------------------------

        public List<AdminUser> GetAdminUsers()
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbAdminUsers = db.AdminUsers.ToList();
                    List<AdminUser> output = new List<AdminUser>();
                    foreach (var adminUser in dbAdminUsers)
                    {
                        var oneUser = new AdminUser();
                        oneUser.id = adminUser.ID;
                        oneUser.userName = adminUser.UserName;
                        oneUser.password = null;
                        output.Add(oneUser);
                    }
                    return output;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public AdminUser GetAdminUser(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    AdminUser returnAdminUser = new AdminUser();
                    AdminUsers foundAdminUser = db.AdminUsers.Find(id);

                    if (foundAdminUser == null)
                        return null;

                    returnAdminUser.id = foundAdminUser.ID;
                    returnAdminUser.userName = foundAdminUser.UserName;
                    returnAdminUser.password = null;
                    return returnAdminUser;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            using (var db = new DBContext())
            {
                try
                {
                    byte[] passwordDB = CreateHash(adminUser.password);
                    AdminUsers foundAdminUser = db.AdminUsers.FirstOrDefault(
                        a => a.Password == passwordDB && a.UserName == adminUser.userName);

                    if (foundAdminUser == null)
                    {
                        return -1;
                    }
                    else
                    {
                        return foundAdminUser.ID;
                    }
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return -1;
                }
            }
        }

        public bool AdminUserInDb(AdminUser inputUser)
        {
            //Function for checking if its the correct input for logging in
            using (var db = new DBContext())
            {
                try
                {
                    byte[] passwordDB = CreateHash(inputUser.password);
                    AdminUsers foundUser = db.AdminUsers.FirstOrDefault(
                        a => a.Password == passwordDB && a.UserName == inputUser.userName);

                    if (foundUser == null)
                        return false;
                    else
                        return true;

                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            // Adding a new row in the database table AdminUsers for this AdminUser:
            var newUser = new AdminUsers()
            {
                UserName = adminUser.userName,
                Password = CreateHash(adminUser.password)
            };

            using (var db = new DBContext())
            {
                try
                {
                    // Adding the new AdminUsers-row in the database:
                    db.AdminUsers.Add(newUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {

                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool EditAdminUser(int id, AdminUser adminUser)
        {
            using (var db = new DBContext())
            {
                try
                {
                    AdminUsers changeUser = db.AdminUsers.Find(id);
                    changeUser.UserName = adminUser.userName;
                    changeUser.Password = CreateHash(adminUser.password);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool DeleteAdminUser(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    AdminUsers delAdminUser = db.AdminUsers.Find(id);
                    db.AdminUsers.Remove(delAdminUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        //------------------------------------------- USERS ------------------------------------------
        public bool CreateUser(User user)
        {
            // Adding a new row in the database table Users for this User:
            var newUser = new Users()
            {
                FirstName = user.firstName,
                LastName = user.lastName,
                Address = user.address,
                Email = user.email,
                PhoneNumber = user.phoneNumber,
                PostalCode = user.postalCode,
                UserName = user.userName,
                Password = CreateHash(user.password)
            };

            using (var db = new DBContext())
            {
                try
                {
                    var postalCodeExists = db.Cities.Find(user.postalCode);

                    // We also need to add the user's city in the database if it doesn't exist:
                    if (postalCodeExists == null)
                    {
                        var newCity = new Cities()
                        {
                            PostalCode = user.postalCode,
                            City = user.city
                        };
                        newUser.City = newCity;
                    }

                    // Adding the new Users-row in the database:
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        private static byte[] CreateHash(string inPassword)
        {
            //Hash function to hash a password and return the hash
            byte[] input, output;
            var algorythm = System.Security.Cryptography.SHA256.Create();
            input = System.Text.Encoding.ASCII.GetBytes(inPassword);
            output = algorythm.ComputeHash(input);
            return output;
        }

        public bool UserInDb(LoginUser inputUser)
        {
            //Function for checking if its the correct input for logging in
            using (var db = new DBContext())
            {
                try
                {
                    byte[] passwordDB = CreateHash(inputUser.password);
                    Users foundUser = db.Users.FirstOrDefault(
                        b => b.Password == passwordDB && b.UserName == inputUser.userName);
                    if (foundUser == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        // Alternative: get the ID for the User in the database
        public int GetUserIdInDB(LoginUser loginUser)
        {
            //Function for checking if its the correct input for logging in
            //and returning the corresponding UserID in the database (not LoginUser, but Users)

            using (var db = new DBContext())
            {
                try
                {
                    byte[] passwordDB = CreateHash(loginUser.password);
                    Users foundUser = db.Users.FirstOrDefault(
                        b => b.Password == passwordDB && b.UserName == loginUser.userName);
                    if (foundUser == null)
                    {
                        return -1;
                    }
                    else
                    {
                        return foundUser.ID;
                    }
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return -1;
                }
            }
        }

        public bool EditUser(int id, User inputUser)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Users changeUser = db.Users.Find(id);
                    changeUser.FirstName = inputUser.firstName;
                    changeUser.LastName = inputUser.lastName;
                    changeUser.Address = inputUser.address;
                    changeUser.Email = inputUser.email;
                    changeUser.PhoneNumber = inputUser.phoneNumber;
                    changeUser.PostalCode = inputUser.postalCode;
                    changeUser.UserName = inputUser.userName;
                    changeUser.Password = CreateHash(inputUser.password);

                    if (changeUser.PostalCode != inputUser.postalCode)
                    {
                        Cities postalCodeExists = db.Cities.FirstOrDefault(p => p.PostalCode == inputUser.postalCode);
                        if (postalCodeExists == null)
                        {
                            var newCity = new Cities()
                            {
                                PostalCode = inputUser.postalCode,
                                City = inputUser.city
                            };
                            db.Cities.Add(newCity);
                        }
                        else
                        {
                            changeUser.PostalCode = inputUser.postalCode;
                        }
                        db.SaveChanges();
                        return true;
                    };
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool DeleteUser(int id, User inputUser)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Users delUser = db.Users.Find(id);
                    db.Users.Remove(delUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool DeleteUser(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Users delUser = db.Users.Find(id);
                    db.Users.Remove(delUser);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public User GetUser(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var oneUsers = db.Users.Find(id);

                    if (oneUsers == null)
                    {
                        return null;
                    }
                    else
                    {
                        var output = new User()
                        {
                            id = oneUsers.ID,
                            firstName = oneUsers.FirstName,
                            lastName = oneUsers.LastName,
                            address = oneUsers.Address,
                            email = oneUsers.Email,
                            phoneNumber = oneUsers.PhoneNumber,
                            postalCode = oneUsers.PostalCode,
                            city = oneUsers.City.City,
                            userName = oneUsers.UserName
                        };
                        return output;
                    }
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public List<User> GetUsers()
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbUsers = db.Users.ToList();
                    List<User> output = new List<User>();
                    foreach (var user in dbUsers)
                    {
                        var oneUser = new User();
                        oneUser.id = user.ID;
                        oneUser.firstName = user.FirstName;
                        oneUser.lastName = user.LastName;
                        oneUser.address = user.Address;
                        oneUser.email = user.Email;
                        oneUser.phoneNumber = user.PhoneNumber;
                        oneUser.postalCode = user.PostalCode;
                        oneUser.city = user.City.City;
                        oneUser.userName = user.UserName;
                        output.Add(oneUser);
                    }
                    return output;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        //------------------------------------------ PRODUCTS ---------------------------------

        public bool CreateProduct(Product product)
        {
            // Adding a new row in the database table Product for this Product:
            var newProduct = new Products()
            {
                Name = product.name,
                Price = product.price,
                Size = product.size,
                Color = product.color,
                Material = product.material,
                Brand = product.brand,
                Url = product.url,
                Gender = product.gender, // Women, Men, Girls, Boys
                Type = product.type
            };

            using (var db = new DBContext())
            {
                try
                {
                    // Adding the new Product-row in the database:
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool EditProduct(int id, Product inputProduct)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Products editProduct = db.Products.Find(id);
                    editProduct.Name = inputProduct.name;
                    editProduct.Price = inputProduct.price;
                    editProduct.Color = inputProduct.color;
                    editProduct.Material = inputProduct.material;
                    editProduct.Brand = inputProduct.brand;
                    editProduct.Gender = inputProduct.gender;
                    editProduct.Size = inputProduct.size;
                    editProduct.Type = inputProduct.type;
                    editProduct.Url = inputProduct.url;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Products delProduct = db.Products.Find(id);
                    db.Products.Remove(delProduct);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }


        public List<Product> ListAllProducts()
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbProducts = db.Products.ToList();
                    List<Product> output = new List<Product>();
                    foreach (var product in dbProducts)
                    {
                        var oneProduct = new Product();
                        oneProduct.id = product.ID;
                        oneProduct.name = product.Name;
                        oneProduct.price = product.Price;
                        oneProduct.size = product.Size;
                        oneProduct.color = product.Color;
                        oneProduct.material = product.Material;
                        oneProduct.brand = product.Brand;
                        oneProduct.url = product.Url;
                        oneProduct.gender = product.Gender;
                        oneProduct.type = product.Type;
                        output.Add(oneProduct);
                    }
                    return output;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public List<Product> ListProductsOfGender(String gender)    // F.ex. Men
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbProducts = db.Products.ToList();

                    List<Product> outputProducts = new List<Product>();
                    foreach (var product in dbProducts)
                    {
                        if (product.Gender.Equals(gender))
                        {
                            var oneProduct = new Product();
                            oneProduct.id = product.ID;
                            oneProduct.name = product.Name;
                            oneProduct.price = product.Price;
                            oneProduct.size = product.Size;
                            oneProduct.color = product.Color;
                            oneProduct.material = product.Material;
                            oneProduct.brand = product.Brand;
                            oneProduct.url = product.Url;
                            oneProduct.gender = product.Gender;
                            oneProduct.type = product.Type;
                            outputProducts.Add(oneProduct);
                        }
                    }
                    return outputProducts;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public List<Product> ListProductsOfType(String type)    // f.ex. Boots
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbProducts = db.Products.ToList();
                    List<Product> outputProducts = new List<Product>();
                    foreach (var product in dbProducts)
                    {
                        if (product.Type.Equals(type))
                        {
                            var oneProduct = new Product();
                            oneProduct.id = product.ID;
                            oneProduct.name = product.Name;
                            oneProduct.price = product.Price;
                            oneProduct.size = product.Size;
                            oneProduct.color = product.Color;
                            oneProduct.material = product.Material;
                            oneProduct.brand = product.Brand;
                            oneProduct.url = product.Url;
                            oneProduct.gender = product.Gender;
                            oneProduct.type = product.Type;
                            outputProducts.Add(oneProduct);
                        }
                    }
                    return outputProducts;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public Product GetProduct(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var oneProducts = db.Products.Find(id);

                    if (oneProducts == null)
                    {
                        return null;
                    }
                    else
                    {
                        var output = new Product()
                        {
                            id = oneProducts.ID,
                            name = oneProducts.Name,
                            price = oneProducts.Price,
                            material = oneProducts.Material,
                            brand = oneProducts.Brand,
                            type = oneProducts.Type,
                            gender = oneProducts.Gender,
                            size = oneProducts.Size,
                            color = oneProducts.Color,
                            url = oneProducts.Url
                        };
                        return output;
                    }
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }

            }
        }

        // --------------------------------------------- ORDERS -------------------------------

        public bool CreateOrder(Order order)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var newOrder = new Orders()
                    {
                        UserID = order.userID,
                        Date = order.date,
                        Orderlines = new List<Orderlines>(),
                        User = db.Users.Find(order.userID)
                    };

                    foreach (var orderLine in order.orderlines)
                    {
                        var newOrderLine = new Orderlines()
                        {
                            ID = orderLine.id,
                            ProductID = orderLine.productId,
                            OrderID = newOrder.ID,
                            Quantity = orderLine.quantity
                        };
                        newOrder.Orderlines.Add(newOrderLine);
                    }
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }
        
        public List<Order> GetOrders()
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbOrders = db.Orders.ToList();
                    List<Order> output = new List<Order>();
                    foreach (var order in dbOrders)
                    {
                        var oneOrder = new Order();
                        oneOrder.id = order.ID;
                        oneOrder.date = order.Date;
                        oneOrder.userID = order.UserID;
                        oneOrder.orderlines = new List<Orderline>();

                        foreach (var orderline in order.Orderlines)
                        {
                            var newOrderLine = new Orderline();
                            newOrderLine.productId = orderline.ProductID;
                            newOrderLine.quantity = orderline.Quantity;
                            newOrderLine.orderID = oneOrder.id;
                            oneOrder.orderlines.Add(newOrderLine);
                        }

                        output.Add(oneOrder);
                    }
                    return output;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public List<Order> GetOrdersForUser(int userId)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var dbOrders = db.Orders.ToList();
                    List<Order> outputOrder = new List<Order>();
                    foreach (var order in dbOrders)
                    {
                        if (order.UserID == userId)
                        {
                            var oneOrder = new Order();
                            oneOrder.id = order.ID;
                            oneOrder.userID = order.UserID;
                            oneOrder.date = order.Date;
                            oneOrder.orderlines = new List<Orderline>();

                            foreach (var orderline in order.Orderlines)
                            {
                                var newOrderLine = new Orderline();
                                newOrderLine.id = orderline.ID;
                                newOrderLine.productId = orderline.ProductID;
                                newOrderLine.quantity = orderline.Quantity;
                                newOrderLine.orderID = oneOrder.id;
                                oneOrder.orderlines.Add(newOrderLine);
                            }
                            outputOrder.Add(oneOrder);
                        }
                    }

                    return outputOrder;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public Order getOrder(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Order returnOrder = new Order();
                    Orders foundOrder = db.Orders.Find(id);

                    if (foundOrder == null)
                        return null;

                    returnOrder.id = foundOrder.ID;
                    returnOrder.userID = foundOrder.UserID;
                    returnOrder.date = foundOrder.Date;
                    returnOrder.orderlines = new List<Orderline>();

                    foreach (var item in foundOrder.Orderlines)
                    {
                        Orderline newOrderLine = new Orderline();
                        newOrderLine.id = item.ID;
                        newOrderLine.orderID = item.OrderID;
                        newOrderLine.productId = item.ProductID;
                        newOrderLine.quantity = item.Quantity;
                        returnOrder.orderlines.Add(newOrderLine);
                    }
                    return returnOrder;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }

        public bool EditOrder(int id, Order input)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Orders changeOrder = db.Orders.Find(id);
                    changeOrder.UserID = input.userID;
                    changeOrder.Date = input.date;
         
                    // nødvendig?:
                    changeOrder.User = db.Users.Find(input.userID);

                    foreach(var item in input.orderlines)
                    {
                        foreach(var itemDb in changeOrder.Orderlines)
                        {
                            if (item.id == itemDb.ID)
                            {
                                // orderID har ikke blitt satt (er 0) av en eller annen grunn, så setter den nå:
                                item.orderID = itemDb.OrderID;

                                EditOrderline(itemDb.ID, item);
                            }
                        }
                    }

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);
                    
                    return false;
                }
            }
        }

        public bool DeleteOrder(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Orders delOrder = db.Orders.Find(id);

                    // Remove-metoden tar seg av dette:
                    /*foreach (var orderline in delOrder.Orderlines)
                    {
                        DeleteOrderline(orderline.ID);
                    }*/

                    db.Orders.Remove(delOrder);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }


        //----------------------------- Orderlines -----------------------------

        public bool CreateOrderline(Orderline input)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var newOrderline = new Orderlines()
                    {
                        OrderID = input.orderID,
                        ProductID = input.productId,
                        Quantity = input.quantity,
                        // nødvendig?:
                        Order = db.Orders.Find(input.orderID),
                        Product = db.Products.Find(input.productId)
                    };
                    
                    db.Orderlines.Add(newOrderline);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool EditOrderline(int id, Orderline input)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Orderlines changeOrderline = db.Orderlines.Find(id);
                    
                    changeOrderline.OrderID = input.orderID;
                    changeOrderline.ProductID = input.productId;
                    changeOrderline.Quantity = input.quantity;

                    // nødvendig?
                    changeOrderline.Order = db.Orders.Find(input.orderID);
                    changeOrderline.Product = db.Products.Find(input.productId);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public bool DeleteOrderline(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Orderlines delOrderline = db.Orderlines.Find(id);
                    db.Orderlines.Remove(delOrderline);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return false;
                }
            }
        }

        public Orderline GetOrderline(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Orderline returnOrderline = new Orderline();
                    Orderlines foundOrderline = db.Orderlines.Find(id);

                    if (foundOrderline == null)
                        return null;

                    returnOrderline.id = foundOrderline.ID;
                    returnOrderline.orderID = foundOrderline.OrderID;
                    returnOrderline.productId = foundOrderline.ProductID;
                    returnOrderline.quantity = foundOrderline.Quantity;
                    return returnOrderline;
                }
                catch (Exception e)
                {
                    writeToLog(e);

                    return null;
                }
            }
        }
        private Product CreateNewProduct(String n, double p, int s, String c, String m, String b, String u, String g, String t)
        {
            Product product = new Product();

            product.name = n;
            product.price = p;
            product.size = s;
            product.color = c;
            product.material = m;
            product.brand = b;
            product.url = u;
            product.gender = g;
            product.type = t;

            return product;
        }
        public bool createProducts()
        {
            try
            {
                DBGodtSkodd dbGodtSkodd = new DBGodtSkodd();
                dbGodtSkodd.CreateProduct(CreateNewProduct("Frosk regnstøvler", 290, 30, "Grønn", "Gummi", "Amazon", "/Content/Images/Boys/Boots/2.jpg", "Boys", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Viking regnstøvler", 230, 30, "Blå", "Gummi", "Viking", "/Content/Images/Boys/Boots/3.jpg", "Boys", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Hunter regnstøvler", 800, 30, "Blå/Grå", "Gummi", "Hunter", "/Content/Images/Boys/Boots/4.jpg", "Boys", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Byggmester Bob", 450, 30, "Blå", "Gummi", "BobBuilder", "/Content/Images/Boys/Boots/5.jpg", "Boys", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Traktor regnstøvler", 200, 30, "Rød", "Gummi", "Amazon", "/Content/Images/Boys/Boots/1.jpg", "Boys", "Boots"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 370, 30, "Hvit/Svart", "Skinn", "AliExpress", "/Content/Images/Boys/DressShoes/1.jpg", "Boys", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 350, 30, "Svart/Sølv", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/2.jpg", "Boys", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 500, 30, "Svart", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/3.jpg", "Boys", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 500, 30, "Svart", "Skinn", "Japanned", "/Content/Images/Boys/DressShoes/4.jpg", "Boys", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 390, 30, "Svart", "Skinn", "ToysRUs", "/Content/Images/Boys/DressShoes/5.jpg", "Boys", "DressShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Sommersandaler", 390, 30, "Hvit", "Mesh", "Uovo", "/Content/Images/Boys/Sandals/1.jpg", "Boys", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Baby gåsko", 550, 30, "Hvit/Blå", "Skinn", "Caroch", "/Content/Images/Boys/Sandals/2.jpg", "Boys", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Mikke Mus", 150, 30, "Blå", "Mesh", "Disney", "/Content/Images/Boys/Sandals/3.jpg", "Boys", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Relix sandaler", 590, 30, "Svart", "Mesh", "Skechers", "/Content/Images/Boys/Sandals/4.jpg", "Boys", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Coga Sport", 350, 30, "Blå", "Mesh", "Coga", "/Content/Images/Boys/Sandals/5.jpg", "Boys", "Sandals"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Joggesko", 350, 30, "Blå", "Mesh", "Jtengda", "/Content/Images/Boys/Sneakers/1.jpg", "Boys", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Air Cushion", 390, 30, "Blå", "Mesh", "KanlBear", "/Content/Images/Boys/Sneakers/2.jpg", "Boys", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Totoho Lite", 300, 30, "Svart", "Mesh", "Totoho", "/Content/Images/Boys/Sneakers/3.jpg", "Boys", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Y-8 joggesko", 490, 30, "Svart", "Mesh", "Miqixz", "/Content/Images/Boys/Sneakers/4.jpg", "Boys", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Panther fotballsko", 500, 30, "Grønn/Gul", "Mesh", "TieBao", "/Content/Images/Boys/Sneakers/5.jpg", "Boys", "Sneakers"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 450, 30, "Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/1.jpg", "Boys", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 500, 30, "Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/2.jpg", "Boys", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("PU skinnstøvler", 130, 30, "Grønn", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/3.jpg", "Boys", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Hongda CowHide", 380, 30, "Grønn", "Skinn", "Hongda", "/Content/Images/Boys/WinterShoes/4.jpg", "Boys", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vinter utesko", 150, 30, "Grønn", "Gummi", "AliExpress", "/Content/Images/Boys/WinterShoes/5.jpg", "Boys", "WinterShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Dora støvler", 500, 29, "Rosa", "Gummi", "Dora The Explorer", "/Content/Images/Girls/Boots/1.jpg", "Girls", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Minni Mus", 390, 29, "Rød", "Gummi", "Disney", "/Content/Images/Girls/Boots/2.jpg", "Girls", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Frozen", 500, 29, "Grønn", "Gummi", "Disney", "/Content/Images/Girls/Boots/3.jpg", "Girls", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Kanin", 290, 29, "Rosa", "Gummi", "LostLands", "/Content/Images/Girls/Boots/4.jpg", "Girls", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Hello Kitty", 450, 29, "Rosa", "Gummi", "Hello Kitty", "/Content/Images/Girls/Boots/5.jpg", "Girls", "Boots"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 600, 29, "Rosa", "Skinn", "The Bay", "/Content/Images/Girls/DressShoes/1.jpg", "Girls", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 150, 29, "Gull", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/2.jpg", "Girls", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersko", 590, 29, "Hvit", "Skinn", "LM83", "/Content/Images/Girls/DressShoes/3.jpg", "Girls", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Baby pensko", 100, 29, "Sølv", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/4.jpg", "Girls", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 450, 29, "Svart", "Skinn", "JoyLand", "/Content/Images/Girls/DressShoes/5.jpg", "Girls", "DressShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersandaler", 390, 29, "Rød", "Skinn", "The Bay", "/Content/Images/Girls/Sandals/1.jpg", "Girls", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersandaler", 150, 29, "Hvit", "Syntetisk Skinn", "Monsoon", "/Content/Images/Girls/Sandals/2.jpg", "Girls", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Princess BowTie", 450, 29, "Rosa", "Skinn", "DCamp", "/Content/Images/Girls/Sandals/3.jpg", "Girls", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Barnesandaler", 550, 29, "Grå", "Mesh", "Gzzhl", "/Content/Images/Girls/Sandals/4.jpg", "Girls", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Jewelled sandaler", 290, 29, "Rosa", "Syntetisk Skinn", "AliCDN", "/Content/Images/Girls/Sandals/5.jpg", "Girls", "Sandals"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Dansesko", 500, 29, "Lilla", "Mesh", "Uovo", "/Content/Images/Girls/Sneakers/1.jpg", "Girls", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Originals ZX", 700, 29, "Lilla", "Mesh", "Adidas", "/Content/Images/Girls/Sneakers/2.jpg", "Girls", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Eclipse løpesko", 490, 29, "Rosa", "Mesh", "Eclipse", "/Content/Images/Girls/Sneakers/3.jpg", "Girls", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Outdoor Sport", 250, 29, "Rosa", "Mesh", "AliExpress", "/Content/Images/Girls/Sneakers/4.jpg", "Girls", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("GT 1000-4", 550, 29, "Rosa", "Mesh", "Asics", "/Content/Images/Girls/Sneakers/5.jpg", "Girls", "Sneakers"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Lobbing Ball", 100, 29, "Rosa", "Plush", "AliExpress", "/Content/Images/Girls/WinterShoes/1.jpg", "Girls", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Candy Thermal", 300, 29, "Lilla", "Plush", "Coga", "/Content/Images/Girls/WinterShoes/2.jpg", "Girls", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 450, 29, "Rosa", "Buffalo Hide", "AliExpress", "/Content/Images/Girls/WinterShoes/3.jpg", "Girls", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Rabbit Bowknot", 250, 29, "Rosa", "Skinn", "AliExpress", "/Content/Images/Girls/WinterShoes/4.jpg", "Girls", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Juicy Couture", 2000, 29, "Rosa", "Velour", "Juicy", "/Content/Images/Girls/WinterShoes/5.jpg", "Girls", "WinterShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Stål-Shank", 200, 44, "Svart", "Gummi", "Walmart", "/Content/Images/Men/Boots/1.jpg", "Men", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Male Martin", 390, 44, "Svart", "Gummi", "Martin", "/Content/Images/Men/Boots/2.jpg", "Men", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Tretorn Strala", 600, 44, "Svart", "Gummi", "Tretorn", "/Content/Images/Men/Boots/3.jpg", "Men", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Economy Plain", 180, 44, "Svart", "Gummi", "Tingley", "/Content/Images/Men/Boots/4.jpg", "Men", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Hot Designers", 390, 44, "Svart", "Gummi", "Warrior", "/Content/Images/Men/Boots/5.jpg", "Men", "Boots"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Bata", 8990, 44, "Svart", "Skinn", "Bata", "/Content/Images/Men/DressShoes/1.jpg", "Men", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Sir Corbett", 6000, 44, "Svart", "Syntetisk Skinn", "Sir Corbett", "/Content/Images/Men/DressShoes/2.jpg", "Men", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Fellini", 3249, 44, "Brun", "Skinn", "Fellini", "/Content/Images/Men/DressShoes/3.jpg", "Men", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Michael Toschi", 5950, 44, "Svart", "Leather", "Michael Toschi", "/Content/Images/Men/DressShoes/4.jpg", "Men", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Roxxii", 8990, 44, "Svart", "Faux Patent Skinn", "Roxxii", "/Content/Images/Men/DressShoes/5.jpg", "Men", "DressShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Orvis", 990, 44, "Brun", "Skinn", "Orvis", "/Content/Images/Men/Sandals/1.jpg", "Men", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Sandaler", 859, 44, "Brun", "Faux Skinn", "Bacca Bucci", "/Content/Images/Men/Sandals/2.jpg", "Men", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Docker Marin", 750, 44, "Brun", "Skinn", "Docker Marin", "/Content/Images/Men/Sandals/3.jpg", "Men", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Supreme Equipt", 300, 44, "Svart", "Mesh", "Sketchers", "/Content/Images/Men/Sandals/4.jpg", "Men", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Grønne sandaler", 1500, 44, "Grønn", "Faux Skinn", "Weinbrenner", "/Content/Images/Men/Sandals/5.jpg", "Men", "Sandals"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("TravelWalker", 650, 44, "Black", "Mesh", "Kroten", "/Content/Images/Men/Sneakers/1.jpg", "Men", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("GT 1000", 990, 44, "Svart/Grønn", "Mesh", "Asics", "/Content/Images/Men/Sneakers/2.jpg", "Men", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Trail", 790, 44, "Grå", "Mesh", "New Balance", "/Content/Images/Men/Sneakers/3.jpg", "Men", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Track", 990, 44, "Svart", "Mesh", "Adidas", "/Content/Images/Men/Sneakers/4.jpg", "Men", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Atletisk sko", 500, 44, "Hvit", "Mesh", "Man", "/Content/Images/Men/Sneakers/5.jpg", "Men", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("3.0 V5", 890, 44, "Blå", "Mesh", "Nike", "/Content/Images/Men/Sneakers/6.jpg", "Men", "Sneakers"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Erkek Boots", 2490, 44, "Brun", "Skinn", "Erkek", "/Content/Images/Men/WinterShoes/1.jpg", "Men", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Caribou Boots", 1300, 44, "Svart", "Nylon", "Sorel", "/Content/Images/Men/WinterShoes/2.jpg", "Men", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Classic Vintage", 950, 44, "Brun", "Skinn", "Doozoo", "/Content/Images/Men/WinterShoes/3.jpg", "Men", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Jobbsko", 400, 44, "Brun", "Cow Split", "AliExpress", "/Content/Images/Men/WinterShoes/4.jpg", "Men", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Shearling", 1050, 44, "Brun", "Skinn", "LL Bean", "/Content/Images/Men/WinterShoes/5.jpg", "Men", "WinterShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Regnstøvler", 790, 39, "Svart", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/1.jpg", "Women", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Ownshoe", 500, 39, "Svart/Hvit", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/2.jpg", "Women", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Salvatore Ferragamo", 3600, 39, "Svart", "Gummi", "Salvatore Ferragamo", "/Content/Images/Women/Boots/3.jpg", "Women", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Hunter støvler", 1250, 39, "Svart", "Gummi", "Hunter", "/Content/Images/Women/Boots/4.jpg", "Women", "Boots"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Dasko Vail", 1150, 39, "Blå", "Gummi", "Dasko Vail", "/Content/Images/Women/Boots/5.jpg", "Women", "Boots"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Rebeca", 8490, 39, "Blå", "Suede", "Jessica Simpson", "/Content/Images/Women/DressShoes/1.jpg", "Women", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Ruched saueskinn", 980, 39, "Svart", "Saueskinn", "Ruched", "/Content/Images/Women/DressShoes/2.jpg", "Women", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Quirkin", 500, 39, "Hvit", "Syntetisk Skinn", "Quirkin", "/Content/Images/Women/DressShoes/3.jpg", "Women", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsko", 750, 39, "Svart", "Suede", "Europe American Express", "/Content/Images/Women/DressShoes/4.jpg", "Women", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsandaler", 1100, 39, "Svart", "Suede", "Traditionals", "/Content/Images/Women/DressShoes/5.jpg", "Women", "DressShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 1500, 39, "Rosa", "Suede", "WUFashion", "/Content/Images/Women/DressShoes/6.jpg", "Women", "DressShoes"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsandaler", 850, 39, "Brun", "Skinn", "Laurent", "/Content/Images/Women/Sandals/1.jpg", "Women", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Åpne sandaler", 200, 39, "Hvit", "Syntetisk Skinn", "Ahuh", "/Content/Images/Women/Sandals/2.jpg", "Women", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Broderte sandaler", 250, 39, "Rød", "Vev", "Polyvore", "/Content/Images/Women/Sandals/3.jpg", "Women", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Gladiatorsandaler", 300, 39, "Brun", "Syntetisk Skinn", "The Bay", "/Content/Images/Women/Sandals/4.jpg", "Women", "Sandals"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Rungsted sandaler", 1000, 39, "Svart", "Skinn", "Ecco", "/Content/Images/Women/Sandals/5.jpg", "Women", "Sandals"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Atletisk sko", 350, 39, "Blå/Rosa", "Mesh", "Skechers", "/Content/Images/Women/Sneakers/1.jpg", "Women", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Gel Kayano 18", 950, 39, "Svart", "Mesh", "Asics", "/Content/Images/Women/Sneakers/2.jpg", "Women", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Puma", 690, 39, "Grå", "Mesh", "Puma", "/Content/Images/Women/Sneakers/3.jpg", "Women", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Riaze", 790, 39, "Svart", "Mesh", "Puma", "/Content/Images/Women/Sneakers/4.jpg", "Women", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("XR Mission", 900, 39, "Blå", "Mesh", "Salomon", "/Content/Images/Women/Sneakers/5.jpg", "Women", "Sneakers"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Nike Free Run", 1200, 39, "Rosa", "Mesh", "Nike", "/Content/Images/Women/Sneakers/6.jpg", "Women", "Sneakers"));

                dbGodtSkodd.CreateProduct(CreateNewProduct("Caribou", 1300, 39, "Grå", "Nylon", "Sorel", "/Content/Images/Women/Sneakers/1.jpg", "Women", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 990, 39, "Brun", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/2.jpg", "Women", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 1250, 39, "Hvit", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/3.jpg", "Women", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Classic Short", 2300, 39, "Brun", "Skinn", "Uggs", "/Content/Images/Women/Sneakers/4.jpg", "Women", "WinterShoes"));
                dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 790, 39, "Brun", "Skinn", "FriendSkorner", "/Content/Images/Women/Sneakers/5.jpg", "Women", "WinterShoes"));
                return true;
            }
            catch (Exception e)
            {
                writeToLog(e);
                return false;
            }
        }
    }
}