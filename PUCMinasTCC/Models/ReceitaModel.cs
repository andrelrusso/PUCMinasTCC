using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class ReceitaModel : BaseModel<Receita>
    {
        public ReceitaModel()
        {
            Filtro = new Receita { };
            Detalhe = new Receita { };
        }
    }
}
