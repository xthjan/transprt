using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transprt.Utils {
    public class UtilAut {    
        public static string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["TransprtEntities"].ConnectionString;

        }      

        #region Tables For Models

        #region IdentityModels

        public static string GetTableNameWithSchema(string tableName) {
            return GetSchema() + "." + tableName;
        }
        public static string GetSchema() {
            return "dbo";
        }

        public static string GetTableUsers() {
            return "UsuariosAuth";
        }
        public static string GetTableRoles() {
            return "Roles";
        }
        public static string GetTableClaims() {
            return "Claims";
        }
        public static string GetTableLogins() {
            return "Logins";
        }
        public static string GetTableUserRoles() {
            return "UsuariosRoles";
        }
        #endregion

        #endregion

    }
}
