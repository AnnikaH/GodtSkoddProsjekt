using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface InDBGodtSkodd
    {

        List<AdminUser> GetAdminUsers();
        AdminUser GetAdminUser(int id);
        int GetAdminIdInDB(AdminUser adminUser);
        bool AdminUserInDb(AdminUser inputUser);
        bool CreateAdminUser(AdminUser adminUser);
        bool EditAdminUser(int id, AdminUser adminUser);
        bool DeleteAdminUser(int id);
        bool CreateUser(User user);
        bool UserInDb(LoginUser inputUser);

        //byte[] CreateHash(string inPassword);






    }
}
