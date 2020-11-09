using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace OpenTK
{
    /* This class represents an object which draws a red square to the screen, using
     * the proper VBO method.
     * I drew squares in both ways for educational purposes.
     */
    class VBOArtist
    {
        // An array which will contain Vector2's, which represent coordinates for OpenGL to place vertices at.
        private Vector2[] vertexBuffer;

        // I know this name is a bit unwieldy, but it helps me reinforce what this integer represents.
        private int vertexBufferObjectName;

        public VBOArtist()
        {
            // Subscribe to Game's draw event.
            Program.game.Draw += OnDraw;

            // I separated this out to make it clearer.
            PrepareBuffer();
        }

        private void PrepareBuffer()
        {
            // I put this in a separate method because the code is pretty simple and it makes this all
            // easier to read. Here i use it to initialize the vertexBuffer array.
            SetVertexBuffer(0, 0, 0, 0);

            /* This tells OpenGL to assign my vertexbuffer an integer to represent the buffer I'm about
             * to do operations on. It's a bit unclear to me why this is refered to as a 'name'. It's
             * and integer, I would expect it to be refered to as an 'index' instead.
             */
            vertexBufferObjectName = GL.GenBuffer();

            // This tells openGL that the buffer I'm working on is the ArrayBuffer.
            // It seems that OpenGL has a bunch of internal buffers, and arraybuffer is one of them.
            // I don't know what that buffer is for though.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectName);

            /* This puts the data in my vertexBuffer into the buffer created by OpenGL.
             * A few things I don't understand:
             * - Why do I need to tell OpenGL that I'm targeting BufferTarget.ArrayBuffer.
             *   Didn't I specify that already in the previous method?
             * - Why do I need to tell OpenGL how long the data is? I'm already passing the data,
             *   which nessecarily includes the length of the data. This just seems like an unnecessary parameter.
             * - Why is the length of the data represented by an IntPtr instead of just a regular int?
             */
            IntPtr dataLength = (IntPtr)(Vector2.SizeInBytes * vertexBuffer.Length);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, dataLength, vertexBuffer, BufferUsageHint.StaticDraw);

            // From what I understand, this binds ArrayBuffer back to some default.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void SetVertexBuffer(float x, float y, float width, float height)
        {
            vertexBuffer = new Vector2[4]
            {
                new Vector2(x,y),
                new Vector2(x+width,y),
                new Vector2(x+width,y+height),
                new Vector2(x, y+height)
            };
        }

        private void OnDraw(object sender, EventArgs e)
        {
            // This draws the square using the VBOs
            DrawSquare(160f, 30f, 100f, 150f, System.Drawing.Color.Red);
        }

        private void DrawSquare(float x, float y, float width, float height, System.Drawing.Color color)
        {
            // Here I'm just setting the vertex buffer to the values I want it to be.
            SetVertexBuffer(x, y, width, height);

            /* I gathered that 'Client' refers to any software that's run on the CPU and calls upon the GPU,
             * and that the server here would be the software that's running on the GPU, and is actually
             * handling all the requests that my software makes.
             * Based on this, I suppose this tells OpenGL that I'm about to send an array of vertices to the GPU.
            */
            GL.EnableClientState(ArrayCap.VertexArray);

            // The below is just a repeat of what I did in PrepareBuffer(). I don't know what it was nessecary to
            // do that earlier if I was going to do it again here.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjectName);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(Vector2.SizeInBytes * vertexBuffer.Length),
                vertexBuffer, BufferUsageHint.StaticDraw);

            // I'm guessing this tells OpenGL what the data that's supposed to represent vertices is going to look like.
            GL.VertexPointer(2, VertexPointerType.Float, Vector2.SizeInBytes, 0);

            // Setting the color to draw with.
            GL.Color3(color.R, color.G, color.B);

            // Pushing the draw calls to the GPU. Apparently I can specify which portion of the array I want to push.
            GL.DrawArrays(PrimitiveType.QuadStrip, 0, vertexBuffer.Length);
        }
    }
}
