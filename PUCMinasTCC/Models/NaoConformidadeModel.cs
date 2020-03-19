using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;

namespace PUCMinasTCC.Models
{
    public class NaoConformidadeModel : BaseModel<NaoConformidade>
    {
        //public int IdNaoConformidade { get; set; }
     
        public SelectList Status { get; set; }
        public NaoConformidadeModel()
        {
            Filtro = new NaoConformidade { };
            Detalhe = new NaoConformidade { };
        }
    }
}
