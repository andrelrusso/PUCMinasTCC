using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class ReceitaModel : BaseModel<Receita>
    {
        //public int IdDespesaFiltro { get; set; }

        //public string MesAnoFiltro { get; set; }
        //public int IdNaoConformidadeFiltro { get; set; }
        public ReceitaModel()
        {
            Filtro = new Receita { };
            Detalhe = new Receita { };
        }
    }
}
