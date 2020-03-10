using PUCMinasTCC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUCMinasTCC.Domain.Entity.AuthData
{
    public interface IAuthData
    {
        /// <summary>
        /// Identificação do usuário
        /// </summary>
        object UserIdentity { get; set; }

        /// <summary>
        /// Chave de validação do usuário
        /// Ex.: Senha, CPF, Biometria
        /// </summary>
        object KeyContent { get; set; }

        /// <summary>
        /// Código do sistema solicitante
        /// </summary>
        long SystemCode { get; set; }

        /// <summary>
        /// Tipo de login solicitado
        /// </summary>
        enumLoginType LoginType { get; set; }

    }
}
