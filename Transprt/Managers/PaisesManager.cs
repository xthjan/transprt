using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Managers.Base;

namespace Transprt.Managers {
    public class PaisesManager : BaseManager<PaisesManager> {
        public PaisesManager() {
        }

        public IEnumerable<SelectListItem> GetAllPaises() {
            using (TransprtEntities entity = new TransprtEntities()) {
                var paises = new List<SelectListItem>();
                paises = entity.Paises
                             .Where(pais => pais.activo)
                             .Select(pais => new SelectListItem() {
                                 Text = pais.nombre,
                                 Value = pais.id.ToString()
                             }).ToList();

                paises.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Seleccione País" });
                return paises;
            }
        }
        public List<SelectListItem> GetAllEstadosPorPais(int id) {
            var estados = new List<SelectListItem>();
            if (id != 0) {
                using (TransprtEntities entity = new TransprtEntities()) {
                    estados = entity.Estados.Where(c => c.Pais.id == id)
                    .Select(estado => new SelectListItem() {
                        Text = estado.nombre,
                        Value = estado.id.ToString()
                    }).ToList();
                }
            }
            estados.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Seleccione Estado" });
            return estados;
        }
        public List<SelectListItem> GetAllEstadosPorEstadoSeleccionado(int id) {
            var estados = new List<SelectListItem>();
            if (id != 0) {
                using (TransprtEntities entity = new TransprtEntities()) {
                    estados = entity.Estados.Where(estado => estado.Pais.Estados.Any(estadoInterno => estadoInterno.id == id))
                    .Select(estado => new SelectListItem() {
                        Text = estado.nombre,
                        Value = estado.id.ToString(),
                        Selected = estado.id == id
                    }).ToList();
                }
            }
            estados.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Seleccione Estado" });
            return estados;
        }
        public int GetIdPaisDeEstado(int estado) {
            using (TransprtEntities entity = new TransprtEntities()) {
                var estadoInterno = entity.Estados.Find(estado);
                var idPais = 0;
                if (estadoInterno != null) {
                    idPais = estadoInterno.Pais.id;
                }
                return idPais;               
            }
        }
    }
}
