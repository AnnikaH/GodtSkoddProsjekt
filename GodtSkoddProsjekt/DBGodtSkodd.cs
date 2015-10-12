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
        public static bool UserInDb(LoginUser inputUser)
        {
            //Function for checking if its the correct input for logging in (?)
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

        public bool EditUser(User inputUser)
        {
            //code to edit user
            //Function for checking if its the correct input for logging in (?)
            using (var db = new DBContext())
            {
                Users foundUser = db.Users.FirstOrDefault(b => b.UserName == inputUser.userName);
                if (foundUser != null)
                {
                    try
                    {
                        //edit user
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    
        public bool DeleteUser(User inputUser) //or id?
        {
            using (var db = new DBContext())
            {
                Users foundUser = db.Users.FirstOrDefault(b => b.UserName == inputUser.userName);
                if (foundUser != null)
                {
                    try
                    {
                        db.Users.Remove(foundUser);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
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
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditProduct(Product product)
        {
            //code to edit Product
            return false;
        }

        public bool DeleteProduct(Product product) //or id?
        {
            //Code to delete PRoduct
            return false;
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
    }
}