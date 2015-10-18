using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GodtSkoddProsjekt.Models;

using System.Diagnostics;

namespace GodtSkoddProsjekt
{
    public class DBGodtSkodd
    {

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
            catch (Exception)
            {
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
                catch
                {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
                catch
                {
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
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> ListTopProducts()
        {
            // For now: just listing the first 9 products

            using (var db = new DBContext())
            {
                try
                {
                    var dbProducts = db.Products.ToList();
                    List<Product> outputProducts = new List<Product>();

                    for(int i = 0; i < 9; i++)
                    {
                        var oneProduct = new Product();

                        if(dbProducts[i] != null)
                        {
                            oneProduct.name = dbProducts[i].Name;
                            oneProduct.price = dbProducts[i].Price;
                            oneProduct.color = dbProducts[i].Color;
                            oneProduct.material = dbProducts[i].Material;
                            oneProduct.brand = dbProducts[i].Brand;
                            oneProduct.url = dbProducts[i].Url;
                            oneProduct.gender = dbProducts[i].Gender;
                            oneProduct.type = dbProducts[i].Type;
                            outputProducts.Add(oneProduct);
                        }
                        else
                        {
                            return outputProducts;
                        }
                    }
                    return outputProducts;
                }
                catch (Exception)
                {
                    var outputProducts = new List<Product>();
                    return outputProducts;
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
                        oneProduct.name= product.Name;
                        oneProduct.price= product.Price;
                        oneProduct.color= product.Color;
                        oneProduct.material= product.Material;
                        oneProduct.brand=product.Brand;
                        oneProduct.url= product.Url;
                        oneProduct.gender= product.Gender;
                        oneProduct.type=product.Type;
                        output.Add(oneProduct);
                    }
                    return output;
                }
                catch (Exception)
                {
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
                        if(product.Type.Equals(type))
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
            catch (Exception)
            {
                return false;
            }
        }


        public List<Order> GetOrders()
        {
            //Returns all orders. (for statistics?)

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

                            foreach(var orderline in order.Orderlines)
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
                catch (Exception)
                {
                    var output = new List<Order>();
                    var errorOrder = new Order();
                    errorOrder.id = 1337;
                    output.Add(errorOrder);
                    return output;
                }
            }
        }
    }
}