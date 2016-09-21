using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Transprt.Data.Identity {

    public class AppUser : IdentityUser {
        public AppUser() {
            CreationDate = DateTime.Now;
            //CreationUserName = AuthorizationUtils.GetUserName();
        }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string CreationUserName { get; set; }

        public DateTime? ModificationDate { get; set; }

        public string ModificationUserName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType) {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
