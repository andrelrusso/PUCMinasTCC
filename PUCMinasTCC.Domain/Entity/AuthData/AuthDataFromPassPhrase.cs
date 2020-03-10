using System;
using System.Collections.Generic;
using System.Text;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Domain.Entity.AuthData
{
    public class AuthDataFromPassPhrase : AuthData
    {
        public AuthDataFromPassPhrase()
        {
            base.LoginType = enumLoginType.PassPhrase;
        }
    }

}
