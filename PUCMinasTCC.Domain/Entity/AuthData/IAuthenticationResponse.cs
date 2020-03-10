using PUCMinasTCC.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUCMinasTCC.Domain.Entity.AuthData
{
    public interface IAuthenticationResponse
    {
        string Token { get; set; }
        Usuario User { get; set; }
        bool Logged { get; set; }
        IList<string> Errors { get; set; }
    }
}
