namespace PUCMinasTCC.Models
{
    public class AlterarSenhaModel : LoginModel
    {
        public string NovaSenha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
