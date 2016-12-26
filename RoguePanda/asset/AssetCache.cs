using RoguePanda.manager;
using System;
using System.Collections.Generic;
using System.IO;

namespace RoguePanda.asset {
    internal class AssetCache {
        private static readonly Lazy<AssetCache> cache =
            new Lazy<AssetCache>(() => new AssetCache());

        private static AssetCache Instance {
            get {
                return cache.Value;
            }
        }

        private IDictionary<string, IAsset> _cache;

        private AssetCache() {
            _cache = new Dictionary<string, IAsset>();
            loadAssets();
        }

        private void loadAssets() {
            string[] dirs = ConfigManager.Config.AssetDirectories;
            foreach (string dir in dirs) {
                loadAssetsInDirectory(dir);
            }
            Console.WriteLine($"{_cache.Count} assets loaded!");
        }

        private void loadAssetsInDirectory(string dir) {
            string[] fns = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            string[] ignorePatterns = ConfigManager.Config.AssetIgnoreExtensions;
            foreach (string fn in fns) {
                if (isValidFileName(fn, ignorePatterns)) {
                    IAsset newAsset = AssetTypeMapper.getAsset(fn);
                    if (!_cache.ContainsKey(newAsset.canonicalName)) {
                        _cache.Add(newAsset.canonicalName, newAsset);
                    }
                }
            }
        }

        private bool isValidFileName(string fn, string[] ignorePatterns) {
            foreach (string ext in ignorePatterns) {
                if (!fn.EndsWith(ext)) {
                    return false;
                }
            }
            return true;
        }

        public static IAsset cachedAsset(string assetName) {
            if (Instance._cache.ContainsKey(assetName)) {
                return Instance._cache[assetName];
            } else {
                string msg = $"Asset with name \"{assetName}\" was not found in AssetCache.";
                throw new AssetNotFoundException(msg);
            }
        }
    }
}
