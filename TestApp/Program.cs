using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePanda;
using System.Reflection;

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            Assembly a = Assembly.GetExecutingAssembly();

            string s = a.FullName;

            AssemblyName t = a.GetName();


            Game g = new Game();
            g.run();
        }
    }
}
