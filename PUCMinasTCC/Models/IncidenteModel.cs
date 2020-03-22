using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace PUCMinasTCC.Models
{
    public class IncidenteModel : BaseModel<Incidente>
    {
        public int IdNaoConformidadeFiltro { get; set; }

        [DisplayName("Não Conformidade")]
        public IList<NaoConformidade> NaoConformidades { get; set; }
        public SelectList Estado { get; set; }
        public IncidenteModel()
        {
            Filtro = new Incidente { EstadoIncidente = enumEstadoIncidente.Todos};
            Detalhe = new Incidente { };

            //Filtro = new NaoConformidade { Status = enumStatus.Todos, OrigemNc = enumOrigemNC.Todas };
            //Detalhe = new NaoConformidade { Status = enumStatus.Ativo };
        }
    }
}
