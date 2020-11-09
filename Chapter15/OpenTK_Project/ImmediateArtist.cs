using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace OpenTK
{
    /* This class represents an object which draws a yellow square to the screen using the
     * deprecated 'immediate mode'.
     * From what I gather, this method is no longer recommended because it only pushes a single
     * instruction to the GPU. The GPU can finish this instruction way quicker than the CPU can
     * get to it's next instruction, so it has to wait, which introduces delay.
     * The correct method sends it a bunch of instructions packaged into one, so the GPU doesn't have
     * to wait for the CPU to do it's thing.
     */
    class ImmediateArtist
    {
        public ImmediateArtist()
        {
            // Subscribes itself to Game's Draw event.
            Program.game.Draw += OnDraw;
        }

        private void OnDraw(object sender, EventArgs e)
        {
            // When the draw event is raised, this method is called to draw the square.
            DrawSquare(30,30,100,150, Color.Yellow);
        }

        private void DrawSquare(float x, float y, float width, float height, Color color)
        {
            // First set the color I'm going to be using to draw everything that follows.
            GL.Color3(color.R,color.G,color.B);

            // Tell OpenGL that the following vertices are supposed to create a quad.
            GL.Begin(PrimitiveType.Quads);

            // Pass 4 vertices that represent the square.
            GL.Vertex2(x, y);
            GL.Vertex2(x + width, y);
            GL.Vertex2(x + width, y + height);
            GL.Vertex2(x, y + height);

            // Tell OpenGL that I'm done telling it what to draw, and it can go do it's thing.
            GL.End();
        }
    }
}
