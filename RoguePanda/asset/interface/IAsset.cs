using System;
using System.IO;

namespace RoguePanda.asset {
    internal enum AssetType {
        Sprite,
        Audio
    }

    internal interface IAsset {
        AssetType assetType { get; }
        string filePath { get; }
        string fileName { get; }
        string canonicalName { get; }
        Guid id { get; }
        FileStream fileStream { get; }
    }
}
