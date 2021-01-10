using System.Diagnostics;
using DataLibraryNew2.DataAccess;
using DataLibraryNew2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibraryNew2.BusinessLogic
{
    public class UserProcessor
    {
        public static string GetRoleIdByUserId(string userId)
        {
            string sql = @"SELECT RoleId
                           FROM dbo.AspNetUserRoles
                           WHERE UserId = '" + userId + "';";

            string connectionName = "DefaultConnection";

            return SqlDataAccess.Query<string>(sql, connectionName);
        }

        public static string GetUserByUserId(string userId)
        {
            string sql = @"SELECT UserName
                           FROM dbo.AspNetUsers
                           WHERE Id = '" + userId + "';";

            string connectionName = "DefaultConnection";

            return SqlDataAccess.Query<string>(sql, connectionName);
        }
    }
}
