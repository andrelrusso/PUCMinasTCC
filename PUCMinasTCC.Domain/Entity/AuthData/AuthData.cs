using System;
using System.Collections.Generic;
using System.Text;
using PUCMinasTCC.Domain.Enums;

namespace PUCMinasTCC.Domain.Entity.AuthData
{
    public class AuthData : IAuthData
    {
        public virtual object UserIdentity { get; set; }
        public virtual object KeyContent { get; set; }
        public virtual long SystemCode { get; set; }
        public virtual enumLoginType LoginType { get; set; }
    }
}
