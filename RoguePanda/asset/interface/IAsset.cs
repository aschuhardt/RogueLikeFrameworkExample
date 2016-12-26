using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.asset {
    internal enum AssetType {
        Sprite
    }

    internal interface IAsset {
        AssetType assetType { get; }
        string filePath { get; }
        string fileName { get; }
        string canonicalName { get; }
        Guid id { get; }
        MemoryStream fileStream { get; }
    }
}
