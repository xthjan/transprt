using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Web;

namespace Transprt.Utils {
    public class UtilAut {    
        public static string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["TransprtEntities"].ConnectionString;

        }      

        public static string GetUserId() {
            return HttpContext.Current.User.Identity.GetUserId();
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
            return "Usuarios";
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
