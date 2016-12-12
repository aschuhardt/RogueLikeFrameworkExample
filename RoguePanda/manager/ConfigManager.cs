using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoguePanda.manager {
    public sealed class ConfigManager {
        private const string CONFIG_FILE_PATH = "config/config.json";
        private static readonly Lazy<ConfigManager> config =
            new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance {
            get {
                return config.Value;
            }
        }

        public Config Configuration { get; set; }

        private ConfigManager() {
            string configJson = System.IO.File.ReadAllText(CONFIG_FILE_PATH);
            this.Configuration = JsonConvert.DeserializeObject<Config>(configJson);
        }
    }
}
