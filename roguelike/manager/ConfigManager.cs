using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoguePanda.manager {
    public sealed class ConfigManager {
        private static readonly Lazy<ConfigManager> config =
            new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance {
            get {
                return config.Value;
            }
        }

        public Config Configuration { get; set; }

        private ConfigManager() {
            string configFilePath = "config/config.json";
            this.Configuration = JsonConvert.DeserializeObject<Config>(configFilePath);
        }
    }
}
