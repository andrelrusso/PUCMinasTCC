using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class NaoConformidadeModel : BaseModel<NaoConformidade>
    {
        //public int IdNaoConformidade { get; set; }
     
        public SelectList OrigemNc { get; set; }
        public SelectList Status { get; set; }
        public NaoConformidadeModel()
        {
            Filtro = new NaoConformidade { Status = enumStatus.Todos , OrigemNc = enumOrigemNC.Todas };
            Detalhe = new NaoConformidade { Status = enumStatus.Ativo };
        }
    }
}
