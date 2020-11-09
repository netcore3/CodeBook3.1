using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTK
{
    internal class Game
    {
        private GameWindow window;

        public Game(GameWindow window)
        {
            // Here I'm just setting some variables and subscribing to events.
            this.window = window;
            window.RenderFrame += OnRenderFrame;
            window.Resize += OnWindowResize;

            // This sets the default color of the background of the application. The color it draws when there is nothing to draw.
            GL.ClearColor((float)System.Drawing.Color.LightBlue.R, (float)System.Drawing.Color.LightBlue.G, (float)System.Drawing.Color.LightBlue.B, (float)System.Drawing.Color.LightBlue.A);
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            // This clears any colors drawn in the last frame and gives up a blank canvas for the new frame.
            GL.Clear(ClearBufferMask.ColorBufferBit);

            /* I separated the drawing functions to make this program easier to understand.
             * the Draw event is handled by the instances of ImmediateArtist and VBOArtist that are initialized
             * in Program's Main function.
             */
            Draw?.Invoke(this, System.EventArgs.Empty);

            // Not a clue what this does.
            GL.Flush();

            /* In order to operate faster OpenGL keeps two buffers, one it's drawing on and one it'd displaying.
             * This function swaps those buffers, so that the one I just drew on is displayed, and the other one
             * can be used to draw the next frame.
             */
            window.SwapBuffers();
        }

        private void OnWindowResize(object sender, System.EventArgs e)
        {
            /* A lot of methods in this function are deprecated,
             * but so far I haven't been able to figure out what
             * the intended ways to do these things are.
             */

            // Here I define which region of the window OpenGL should draw into.
            // The origin for this lies at the top-left of the window.
            GL.Viewport(0, 0, window.Width, window.Height);

            /* Here I'm telling openGL that any matrix operations I'm about to perform
             * should be performed on the projection matrix.
             */
            GL.MatrixMode(MatrixMode.Projection);

            /* This re-loads an untransformed version of the projection matrix.
             * This is nessecary because, without this, any transformations I do would stack up,
             * and the results would be unpredictable.
             */
            GL.LoadIdentity();

            /* This multiplies the projection matrix with an ortographic matrix.
             * It also remaps the standard coordinate system, so that the origin is represented by
             * the values of (left,top) , and the outermost locations that can be drawn on
             * are represented by (right, bottom).
             * 
             * I've mapped it here so that the origin is at the top-left, and the outermost
             * coordinates ar at the edges of the window.
             */
            GL.Ortho(0d, window.Width, window.Height, 0d, -1d, 1d);

            // Tell OpenGL that any following transformations should happen to the ModelView matrix instead of the Projection matrix.
            GL.MatrixMode(MatrixMode.Modelview);
        }


        public event System.EventHandler Draw;
    }
}