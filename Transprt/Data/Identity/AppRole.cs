using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transprt.Managers;

namespace Transprt.Data.Identity {
    public class AppRole : IdentityRole {

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        [Required]
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Creado Por")]
        public string CreationUserName { get; set; }

        public DateTime? ModificationDate { get; set; }

        [StringLength(128)]
        public string ModificationUserName { get; set; }
        public string GetUserCreador() {
            UsuarioManager usuarioManager = UsuarioManager.Instance;
            return usuarioManager.GetNombreCompletoById(CreationUserName);
        }
        [NotMapped]
        public bool Asignado { get; set; }
       
    }
}
