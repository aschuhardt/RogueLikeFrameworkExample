using System;
using System.IO;

namespace RoguePanda.asset {
    internal abstract class AssetBase : IAsset {
        protected string _fileName;
        protected string _canonicalName;
        protected string _filePath;
        private Guid _id;
        private bool _isValidFile;

        public AssetType assetType {
            get {
                return getAssetType();
            }
        }

        public string canonicalName {
            get {
                return _canonicalName;
            }
        }

        public string fileName {
            get {
                return _fileName;
            }
        }

        public string filePath {
            get {
                return _filePath;
            }
        }

        public Guid id {
            get {
                return _id;
            }
        }

        public FileStream fileStream {
            get {
                if (_isValidFile) {
                    FileStream fs = File.OpenRead(_filePath);
                    return fs;
                } else {
                    string msg = $"Attempted to access file stream of invalid asset file: \"{_filePath}\".";
                    throw new InvalidAssetStreamAccessException(msg);
                }
            }
        }

        public AssetBase(string filePath) {
            if (File.Exists(filePath)) {
                _isValidFile = true;
                _filePath = filePath;
                _fileName = Path.GetFileName(_filePath);
                _canonicalName = Path.GetFileNameWithoutExtension(_filePath);
                _id = Guid.NewGuid();
            } else {
                _isValidFile = false;
                string msg = $"Could not find asset \"{filePath}\"";
                throw new AssetFileNotFoundException(msg);
            }
        }

        public override int GetHashCode() {
            return _id.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj is AssetBase) {
                return ((AssetBase)obj).id.Equals(_id);
            } else {
                return false;
            }
        }

        protected abstract AssetType getAssetType();

    }
}
