using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.FrameworkDAO.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
//using TGS.Framewok.DAO;

namespace SICCA.Domain.Entity.AuthData
{
    public class Permissao
    {
        [EntityFromSameQuery]
        public Usuario Usuario { get; set; }
       // [EntityFromSameQuery]
        //public Empresa Empresa { get; set; }
        //[EntityFromSameQuery]
        //public Sistema Sistema { get; set; }
        //[EntityFromSameQuery]
        //public Perfil Perfil { get; set; }
        //[EntityFromSameQuery]
        //public Acesso Acesso { get; set; }
    }
}
