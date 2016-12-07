namespace roguelike {
    /// <summary>
    /// Used to represent the various states that the game can have.
    /// Each state should correspond to a module, and can be mapped to one by StateMapper.
    /// </summary>
    enum State {
        MainMenu,
        Test,
        Options,
        Play,
        About,
        NameEntry
    }
}
