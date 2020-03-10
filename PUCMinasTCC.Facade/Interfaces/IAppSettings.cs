using System;
using System.Collections.Generic;
using System.Text;

namespace PUCMinasTCC.Facade.Interfaces
{
    public interface IAppSettings
    {
        string KeyCrypto { get; set; }
        long IdSistema { get; set; }
    }
}
