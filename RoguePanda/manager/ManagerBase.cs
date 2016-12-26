namespace RoguePanda.manager {
    internal abstract class ManagerBase {
        public string errorMessage { get; protected set; }
        public abstract bool init();
        public abstract void run();
    }
}
