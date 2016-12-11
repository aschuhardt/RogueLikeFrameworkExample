namespace RoguePanda {
    /// <summary>
    /// Transfer object used to contain info about the state of the game's Window object.
    /// </summary>
    class VideoSettings {
        public uint width { get; set; }
        public uint height { get; set; }
        public uint aalevel { get; set; }
        public bool fullscreen { get; set; }
    }
}
