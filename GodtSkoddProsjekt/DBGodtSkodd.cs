using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GodtSkoddProsjekt.Models;

namespace GodtSkoddProsjekt
{
    public class DBGodtSkodd
    {
        public void test()
        {
            using (var db = new DBContext())
            {

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
                    postalCode = oneUsers.PostalCode,
                    city = oneUsers.City.PostalCode
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
            // For now: just listing the first 5 products

            using (var db = new DBContext())
            {
                try
                {
                    var temp = db.Products.ToList();
                    List<Product> output = new List<Product>();

                    for(int i = 0; i < 3; i++)
                    {
                        var oneProduct = new Product();

                        if(temp[i] != null)
                        {
                            oneProduct.name = temp[i].Name;
                            oneProduct.price = temp[i].Price;
                            oneProduct.color = temp[i].Color;
                            oneProduct.material = temp[i].Material;
                            oneProduct.brand = temp[i].Brand;
                            oneProduct.url = temp[i].Url;
                            oneProduct.gender = temp[i].Gender;
                            oneProduct.type = temp[i].Type;
                            output.Add(oneProduct);
                        }
                        else
                        {
                            return output;
                        }
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

        public List<Product> ListAllProducts()
        {
            using (var db = new DBContext())
            {
                try
                {
                    var temp = db.Products.ToList();
                    List<Product> output = new List<Product>();
                    foreach (var product in temp)
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

        public List<Product> ListProductsOfType(String type)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var temp = db.Products.ToList();
                    List<Product> output = new List<Product>();
                    foreach (var product in temp)
                    {
                        if(product.Type == type)
                        {
                            var oneProduct = new Product();
                            product.Name = oneProduct.name;
                            product.Price = oneProduct.price;
                            product.Color = oneProduct.color;
                            product.Material = oneProduct.material;
                            product.Brand = oneProduct.brand;
                            product.Url = oneProduct.url;
                            product.Gender = oneProduct.gender;
                            product.Type = oneProduct.type;
                            output.Add(oneProduct);
                        }
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
    }
}