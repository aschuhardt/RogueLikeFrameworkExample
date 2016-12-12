namespace RoguePanda.manager {
    abstract class ManagerBase {
        public string errorMessage { get; protected set; }
        public abstract void run();
    }
}
