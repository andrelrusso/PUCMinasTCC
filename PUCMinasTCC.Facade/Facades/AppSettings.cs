using Microsoft.Extensions.Configuration;
using PUCMinasTCC.Facade.Interfaces;
using PUCMinasTCC.Util.Util;

namespace PUCMinasTCC.Facade.Facades
{
    public class AppSettings : IAppSettings
    {
        public const string SYSTEM_CODE = "SystemCode";

        public AppSettings(IConfiguration configuration)
        {
            KeyCrypto = CryptoUtils.ReadKey();
            //IdSistema = configuration
        }
        public string KeyCrypto { get; set; }
        public long IdSistema { get; set; }
    }
}
