using Microsoft.AspNetCore.Mvc.Rendering;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Models
{
    public class UsuarioModel : BaseModel<Usuario>
    {
        public int IdEmpresaFiltro { get; set; }

        public SelectList Status { get; set; }
        public SelectList Perfis { get; set; }
        public UsuarioModel()
        {
            Filtro = new Usuario { Status = enumStatus.Todos }; 
            Detalhe = new Usuario { Status = enumStatus.Ativo };
        }
    }
}
