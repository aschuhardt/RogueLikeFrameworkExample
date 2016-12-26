namespace RoguePanda.entity {
    public enum AudioEntityType {
        Sound,
        Music
    }

    public interface IAudioEntity {
        string assetID { get; set; }
        float volume { get; set; }
        float pitch { get; set; }
        bool loop { get; set; }
        string tag { get; set; }
        AudioEntityType audioType { get; }
    }
}
