# RoguePanda
A roguelike game framework leveraging SFML.NET.

# Summary
This is a small 2D game engine I put together for my own personal use.  I will be making occasional updates to it as I see fit, so be aware that its functionality might drastically change at some point.  

I intend for the usage of this engine to emphasize modularity and open-endedness.  My intention is that each module can focus on its own functionality, while not being concerned with the tedium of drawing and state transitioning.  Not much else past these basic functions are provided, and so the developer of each module is solely responsible for its implementation.

Please feel free to fork this repository or use it however you like.

# Setup
1. Build solution
2. Copy the "Config" and "Typeset" folders to root directory of new project, and set their contents to copy to the output directory.
4. Add a project reference to the library RoguePanda.dll
5. Set the "DefaultModule" value in config.json to match the fully-qualified name of the class you'd like to load on startup.
 - i.e. "MyApplication.modules.MainModule"

# Usage
In Program.cs, reference the RoguePanda namespace, then create a new Game object and call its .run() method.
 ```C#
using RoguePanda; 
namespace MyApplication {
    class Program {
          static void Main(string[] args) {
              using (Game g = new Game()) {
                  g.run();
              } 
          }
      }
  }
 ```
 In your default module (here we'll use "MyApplication.modules.MainModule.cs"), inherit the abstract "RoguePanda.modules.ModuleBase" object and implement its abstract routines.
 In ```getModuleState()``` return a string representation of the class's fully-qualified name.
 ```C# 
using System.Collections.Generic;
using RoguePanda.modules;
namespace MyApplication.modules {
  class Main : ModuleBase {
      protected override string getModuleState() {
          return "MyApplication.modules.Main";
      }

      protected override bool initModule(IList<object> parameters) {
          //do module initialization here
          return true;
      }

      protected override void runModule() {
          //this will be called on each frame
      }
  }
}
 ```
 
 
# Notes
  - There is a test application included with the solution that demonstrates a simple implementation utilizing this library, and a sample of some of its functionality.
  - The solution for this libary is set up to build against .NET 4.5.2; it may or may not be compatible with older and/or newer versions.  I can't guarantee compatibility, but do feel free to experiment.
  
# Resources
 Information about SFML.NET can be found here: http://www.sfml-dev.org/download/sfml.net/ 
 
 The SFML.NET GitHub repository is here: https://github.com/SFML/SFML.Net
