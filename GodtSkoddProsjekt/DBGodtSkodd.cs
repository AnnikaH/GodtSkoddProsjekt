﻿using System;
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

        public bool createUser(User user)
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
                Password = createHash(user.password)
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
        private static byte[] createHash(string inPassword)
        {
            byte[] input, output;
            var algorythm = System.Security.Cryptography.SHA256.Create();
            input = System.Text.Encoding.ASCII.GetBytes(inPassword);
            output = algorythm.ComputeHash(input);
            return output;
        }
        private static bool userInDB(User inputUser)
        {
            using (var db = new DBContext())
            {
                byte[] passordDB = createHash(inputUser.password);
                Users foundUser = db.Users.FirstOrDefault(
                    b => b.Password == passordDB && b.UserName == inputUser.userName);
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
    }
}