﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transprt.Data.Identity {
    public class AppRole : IdentityRole {

        public AppRole() {
            CreationDate = DateTime.Now;
            //CreationUserName = AuthorizationUtils.GetUserName();
        }

        [NotMapped]
        public string Url { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(128)]
        public string CreationUserName { get; set; }

        public DateTime? ModificationDate { get; set; }

        [StringLength(128)]
        public string ModificationUserName { get; set; }
    }
}
