using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUCMinasTCC.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class AlterarSenhaModel : LoginModel
    {
        public string NovaSenha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
