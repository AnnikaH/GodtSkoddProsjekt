using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class RepositoryStub
    {


        public bool CreateUser(User user)
        {
            return false;
        }

        /*  private static byte[] CreateHash(string inPassword)
          {
              //Hash function to hash a password and return the hash
              byte[] input, output;
              var algorythm = System.Security.Cryptography.SHA256.Create();
              input = System.Text.Encoding.ASCII.GetBytes(inPassword);
              output = algorythm.ComputeHash(input);
              return output;
    }*/

    public bool UserInDb(LoginUser inputUser)
        {
            //Function for checking if its the correct input for logging in
            return false;
        }

        // Alternative: get the ID for the User in the database
        public int GetUserIdInDB(LoginUser loginUser)
        {
            //Function for checking if its the correct input for logging in
            //and returning the corresponding UserID in the database (not LoginUser, but Users)

            return 0;
        }

        public bool EditUser(int id, User inputUser)
        {
            return false;
        }

        public bool DeleteUser(int id, User inputUser)
        {
            return false;
        }

        public User GetUser(int id)
        {
            return null;
        }

        //------------------------------------------ PRODUCTS ---------------------------------

        public bool CreateProduct(Product product)
        {
            // Adding a new row in the database table Product for this Product:
            return false;
        }

        public bool EditProduct(int id, Product inputProduct)
        {
            return false;
        }

        public bool DeleteProduct(int id)
        {
            return false;
        }

        public List<Product> ListTopProducts()
        {
            // For now: just listing the first 9 products

            return null;
        }

        public List<Product> ListAllProducts()
        {
            return null;
        }

        public List<Product> ListProductsOfGender(String gender)    // F.ex. Men
        {
            return null;
        }

        public List<Product> ListProductsOfType(String type)    // f.ex. Boots
        {
            return null;
        }

        public Product GetProduct(int id)
        {
            return null;
        }

        // --------------------------------------------- ORDERS -------------------------------

        public bool CreateOrder(Order order)
        {

            return false;
        }


        public List<Order> GetOrders()
        {
            //Returns all orders. (for statistics?)

            return null;
        }

        public List<Order> GetOrdersForUser(int userId)
        {
            return null;
        }
    }
}