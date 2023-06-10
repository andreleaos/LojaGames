using GameStore.Domain.ConfigApp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Config
{
    public class ConfigParameters : IConfigParameters
    {
        private readonly IConfiguration _configuration;

        public ConfigParameters(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public void EnableConnectionLocal(bool enabled)
        {
            GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB = enabled;
        }

        public void SetGeneralConfig()
        {
            var connectionParameter = _configuration["FeatureFlags:enable_connection_local_db"];
            var enableConnectionLocal = Boolean.Parse(connectionParameter);
            GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB = enableConnectionLocal;
        }
    }
}
