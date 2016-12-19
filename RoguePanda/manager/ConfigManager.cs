using System;
using Newtonsoft.Json;

namespace RoguePanda.manager {
    public sealed class ConfigManager {
        private const string CONFIG_FILE_PATH = "config/config.json";
        private static readonly Lazy<ConfigManager> config =
            new Lazy<ConfigManager>(() => new ConfigManager());

        private static ConfigManager Instance {
            get {
                return config.Value;
            }
        }

        private Config Configuration { get; set; }

        private ConfigManager() {
            string configJson = System.IO.File.ReadAllText(CONFIG_FILE_PATH);
            this.Configuration = JsonConvert.DeserializeObject<Config>(configJson);
        }

        public static Config Config {
            get {
                return ConfigManager.Config;
            }
        }

    }
}
