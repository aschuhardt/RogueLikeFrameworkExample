namespace RoguePanda {
    public sealed class Config {
        public int AntialiasingLevel { get; set; }
        public int BitDepth { get; set; }
        public int DefaultWindowHeight { get; set; }
        public int DefaultWindowWidth { get; set; }
        public int FontBackgroundHeightMod { get; set; }
        public int FontBackgroundVerticalPositionMod { get; set; }
        public int FontHeight { get; set; }
        public string FontPath { get; set; }
        public int FontWidth { get; set; }
        public string LineDelimiter { get; set; }
        public int StencilDepth { get; set; }
        public string WindowTitle { get; set; }
        public string DefaultModule { get; set; }
        public bool SmoothSprites { get; set; }
        public string[] AssetDirectories { get; set; }
        public string[] AssetIgnoreExtensions { get; set; }
    }
}
