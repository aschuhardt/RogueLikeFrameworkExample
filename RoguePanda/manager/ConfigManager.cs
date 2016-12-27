using System;
using Newtonsoft.Json;

namespace RoguePanda.manager {
    public class ConfigManager {
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
                return Instance.Configuration;
            }
        }

        public static void SaveConfig(Config newCfg) {
            string configJson = JsonConvert.SerializeObject(newCfg, Formatting.Indented);
            System.IO.File.WriteAllText(CONFIG_FILE_PATH, configJson);
            Instance.Configuration = newCfg;
        }
    }
}
