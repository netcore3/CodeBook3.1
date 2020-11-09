using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace OpenTK
{
    class Program
    {
        public static GameWindow window;
        public static Game game;
        public static ImmediateArtist immediateArtist;
        public static VBOArtist vboArtist;
        
        static void Main(string[] args)
        {
            /* Creates a new GameWindow of 640 in length and 360 pixels in height.
             * From what I understand, this function is specific to OpenTK, and does
             * not exist in regular OpenGL. It creates a window in which openGL
             * can operate while my C# application tell it what to do.
             */
            window = new GameWindow(640, 360);
            
            // Creates an instance of the Game object, which I use to set up the OpenGL environment
            game = new Game(window);

            /* ImmediateArtist and VBOArtist are two object which just draw things in their own way.
             * I separated these into two separate objects to make it easier to distinguish what
             * code belongs to what method, so it's easier for me to learn to understand both.
             */
            immediateArtist = new ImmediateArtist();
            vboArtist = new VBOArtist();

            // starts the openGL application with an update once every 60th of a second.
            window.Run(1D / 60D);
        }
    }
}
