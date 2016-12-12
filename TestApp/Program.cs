using RoguePanda;

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            using (Game g = new Game()) {
                g.run();
            }
        }
    }
}