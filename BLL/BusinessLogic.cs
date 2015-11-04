using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class BusinessLogic
    {
        public bool DatabaseProductsFilled()
        {
            var dal = new DBGodtSkodd();
            List<Product> products = dal.ListAllProducts();

            if (products.Count == 0)
                return false;

            return true;
        }

        public bool CreateDatabaseContent()
        {
            var dal = new DBGodtSkodd();
            return dal.CreateDatabaseContent();
        }

        public List<AdminUser> GetAdminUsers()
        {
            var dal = new DBGodtSkodd();
            return dal.GetAdminUsers();
        }

        public int GetAdminIdInDB(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.GetAdminIdInDB(adminUser);
        }

        public bool AdminUserInDb(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.AdminUserInDb(adminUser);
        }

        public bool CreateAdminUser(AdminUser adminUser)
        {
            var dal = new DBGodtSkodd();
            return dal.CreateAdminUser(adminUser);
        }

/* ----------------- Fra Tor sitt eksempel (Lagdeling):

        public List<Kunde> hentAlle()
        {
            var KundeDAL = new KundeDAL();
            List<Kunde> alleKunder = KundeDAL.hentAlle();
            return alleKunder;
        }
        public bool settInn(Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.settInn(innKunde);
        }
        public bool endreKunde(int id, Kunde innKunde)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.endreKunde(id, innKunde);
        }
        public bool slettKunde(int slettId)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.slettKunde(slettId);
        }
        public Kunde hentEnKunde(int id)
        {
            var KundeDAL = new KundeDAL();
            return KundeDAL.hentEnKunde(id);
        }
        */
    }
}