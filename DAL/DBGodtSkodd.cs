﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

using System.Diagnostics;
using System.IO;

namespace DAL
{
    public class DBGodtSkodd
    {
        string errorLogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs";

        public bool CreateDatabaseContent()
        {
            try
            {
                throw new InvalidDataException("This might be working");
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);
            }
            // Kopier kode for når man trykker på knappen i Admin/Index.cshtml - (bortsett fra å legge inn en ordre på bruker nr. 1..?

            return false;
        }

        public void writeToLog(string input)
        {
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
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + input);
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
                    outputFile.WriteLine("[" + nowHour + ":" + nowMinute + "] " + input);
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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);


                    var output = new List<AdminUser>();
                    return output;
                }
            }
        }

        public AdminUser GetAdminUser(int id)
        {
            // Fill in

            using (var db = new DBContext())
            {

                AdminUser returnAdminUser = new AdminUser();
                AdminUsers foundAdminUser = db.AdminUsers.Find(id);
                returnAdminUser.id = foundAdminUser.ID;
                returnAdminUser.userName = foundAdminUser.UserName;
                returnAdminUser.password = null;
                return returnAdminUser;
            }
        }



        public int GetAdminIdInDB(AdminUser adminUser)
        {
            using (var db = new DBContext())
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
        }

        public bool AdminUserInDb(AdminUser inputUser)
        {
            //Function for checking if its the correct input for logging in
            using (var db = new DBContext())
            {
                byte[] passwordDB = CreateHash(inputUser.password);
                AdminUsers foundUser = db.AdminUsers.FirstOrDefault(
                    a => a.Password == passwordDB && a.UserName == inputUser.userName);

                if (foundUser == null)
                    return false;
                else
                    return true;
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

            var db = new DBContext();

            try
            {
                // Adding the new AdminUsers-row in the database:
                db.AdminUsers.Add(newUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    return false;
                }
            }
        }

        public bool DeleteAdminUser(int id)
        {
            // Fill in

            // Write to log in catch

            return false;
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

            var db = new DBContext();

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
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
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
        }

        // Alternative: get the ID for the User in the database
        public int GetUserIdInDB(LoginUser loginUser)
        {
            //Function for checking if its the correct input for logging in
            //and returning the corresponding UserID in the database (not LoginUser, but Users)

            using (var db = new DBContext())
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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    return false;
                }
            }
        }

        public bool DeleteUser(int id, User inputUser)
        {
            var db = new DBContext();
            try
            {
                Users delUser = db.Users.Find(id);
                db.Users.Remove(delUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            var db = new DBContext();
            try
            {
                Users delUser = db.Users.Find(id);
                db.Users.Remove(delUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
            }
        }

        public User GetUser(int id)
        {
            var db = new DBContext();

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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    var output = new List<User>();
                    return output;
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

            var db = new DBContext();

            try
            {
                // Adding the new Product-row in the database:
                db.Products.Add(newProduct);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    return false;
                }
            }
        }

        public bool DeleteProduct(int id)
        {
            var db = new DBContext();
            try
            {
                Products delProduct = db.Products.Find(id);
                db.Products.Remove(delProduct);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
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
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    var output = new List<Product>();
                    return output;
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
                            oneProduct.name = product.Name;
                            oneProduct.price = product.Price;
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
                catch (Exception)
                {
                    var output = new List<Product>();
                    return output;
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
                            oneProduct.name = product.Name;
                            oneProduct.price = product.Price;
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
                catch (Exception)
                {
                    var output = new List<Product>();
                    return output;
                }
            }
        }

        public Product GetProduct(int id)
        {
            var db = new DBContext();

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

        // --------------------------------------------- ORDERS -------------------------------

        public bool CreateOrder(Order order)
        {
            var db = new DBContext();

            var newOrder = new Orders()
            {
                User = db.Users.Find(order.userID),
                UserID = order.userID,
                Date = order.date,
                Orderlines = new List<Orderlines>()
            };

            foreach (var orderLine in order.orderlines)
            {
                var newOrderLine = new Orderlines()
                {
                    ProductID = orderLine.productId,
                    OrderID = newOrder.ID,
                    Quantity = orderLine.quantity
                };
                newOrder.Orderlines.Add(newOrderLine);
            }


            try
            {
                db.Orders.Add(newOrder);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                writeToLog(errorMessage);

                return false;
            }
        }


        public List<Order> GetOrders()
        {
            //Returns all orders. (for statistics?)

            // Write to log in catch

            return null;
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
                                var NewOrderLine = new Orderline();
                                NewOrderLine.productId = orderline.ProductID;
                                NewOrderLine.quantity = orderline.Quantity;
                                NewOrderLine.orderID = oneOrder.id;
                                oneOrder.orderlines.Add(NewOrderLine);
                            }
                            outputOrder.Add(oneOrder);
                        }
                    }

                    return outputOrder;
                }
                catch (Exception e)
                {
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);

                    var output = new List<Order>();
                    var errorOrder = new Order();
                    errorOrder.id = 1337;
                    output.Add(errorOrder);
                    return output;
                }
            }
        }
        public Order getOrder(int id)
        {
            // Fill in
            using (var db = new DBContext())
            {

                Order returnOrder = new Order();
                Orders foundOrder = db.Orders.Find(id);
                returnOrder.id = foundOrder.ID;
                returnOrder.userID = foundOrder.UserID;
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
        }
        public bool EditOrder(int id, Order input)
        {
            /*using (var db = new DBContext())
            {
                try
                {
                    Orders changeOrder = db.Orders.Find(id);
                    changeOrder.UserID = input.userID;

                    foreach (var item in input.orderlines)
                    {
                        changeOrder.Orderlines.Find();
                    }




                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    string errorMessage = e.Message.ToString() + e.StackTrace.ToString();
                    writeToLog(errorMessage);


                    return false;
                }
            }*/
            return false;
        }
        public bool DeleteOrder(int id)
        {
            return false;
        }
    }
}