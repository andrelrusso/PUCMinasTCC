using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUCMinasTCC.Domain.Entity.AuthData
{
    public class AuthenticationResponse : IAuthenticationResponse
    {
        public string Token { get; set; }
        public Usuario User { get; set; }
        public bool Logged { get; set; }
        public IList<string> Errors { get; set; }
    }
}
