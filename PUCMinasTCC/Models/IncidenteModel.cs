using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using System.Collections.Generic;

namespace PUCMinasTCC.Models
{
    public class IncidenteModel : BaseModel<Incidente>
    {
        public int IdNaoConformidadeFiltro { get; set; }
        public IList<NaoConformidade> NaoConformidades { get; set; }
        public SelectList Estado { get; set; }
        public IncidenteModel()
        {
            Filtro = new Incidente { };
            Detalhe = new Incidente { };
        }
    }
}
