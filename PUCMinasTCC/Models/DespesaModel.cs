using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class DespesaModel : BaseModel<Despesa>
    {
        //public int IdDespesaFiltro { get; set; }

        //public string MesAnoFiltro { get; set; }
        //public int IdNaoConformidadeFiltro { get; set; }
        public DespesaModel()
        {
            Filtro = new Despesa { };
            Detalhe = new Despesa { };
        }
    }
}
