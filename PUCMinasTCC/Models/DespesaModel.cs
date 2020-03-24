using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class DespesaModel : BaseModel<Despesa>
    {
        public int IdEmpresaFiltro { get; set; }

        public SelectList Status { get; set; }
        public DespesaModel()
        {
            Filtro = new Despesa {}; 
        }
    }
}
