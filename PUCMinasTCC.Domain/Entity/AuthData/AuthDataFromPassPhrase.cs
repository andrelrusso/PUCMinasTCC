using System;
using System.Collections.Generic;
using System.Text;
using PUCMinasTCC.Domain.Enums;

namespace SICCA.Domain.Entity.AuthData
{
    public class AuthDataFromPassPhrase : PUCMinasTCC.Domain.Entity.AuthData.AuthData
    {
        public AuthDataFromPassPhrase()
        {
            base.LoginType = enumLoginType.PassPhrase;
        }
    }

}
