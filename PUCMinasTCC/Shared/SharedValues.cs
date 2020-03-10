using Microsoft.AspNetCore.Http;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Utils;

namespace PUCMinasTCC
{
    public static class SharedValues
    {
        public const string MSG_ERROR = "MSG_ERROR";
        public const string MSG_SUCCESS = "MSG_SUCCESS";
        public const string EMPRESA_ATIVA = "EMPRESA_ATIVA";
        public const string USUARIO_LOGADO = "USUARIO_LOGADO";

        public static string Title = "SQG";
        public static ISession Session { get; set; }

        public static string ErrorMessage
        {
            get => Session?.GetString(MSG_ERROR);
            set => Session?.SetString(MSG_ERROR, value ?? string.Empty);
        }

        public static string SuccessMessage
        {
            get => Session?.GetString(MSG_SUCCESS);
            set => Session?.SetString(MSG_SUCCESS, value ?? string.Empty);
        }

        public static Usuario UsuarioLogado
        {
            get => Session?.Get<Usuario>(USUARIO_LOGADO);
            set => Session?.Set(USUARIO_LOGADO, value);
        }
    }
}
