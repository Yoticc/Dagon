using Dagon.WinAPI;
using Dagon;
using Dagon.OpenGL;
using OpenGL;
using static OpenGL.Enums;
using System.Drawing;


struct MSG
{
    public nint hwnd;
    public WindowMessage message;
    public long wParam;
    public long lParam;
    public int time;
    public long pt;
    public int lPrivate;
};

unsafe class Program
{
    static void Main()
    {
        kernel32.LoadLibrary("opengl32");

        using var window = new Window(
            title: "Dagon test window",
            location: (200, 200),
            size: (640, 360),
            visible: true,
            caption: true,
            closeButton: true);

        using var workspace = new WindowWorkspace(window);

        bool running = true;
        while (running)
        {
            MSG msg;
            while (user32.PeekMessageA(&msg, 0, 0, 0, 1))
            {
                if (msg.message == WindowMessage.Quit)                
                    running = false;

                user32.TranslateMessage(&msg);
                user32.DispatchMessageA(&msg);
            }

            GL.ClearColor(0.6f, 0.7f, 1.0f, 1.0f);
            GL.Clear(0x4000 | 0x100);

            GL.Begin(Mode.Lines);
            GL.Vertex2d(0, 0);
            GL.Vertex2d(220, 220);
            GL.End();

            GL.Begin(Mode.Lines);
            GL.Vertex2d(0.1, 0.1);
            GL.Vertex2d(0.9, 0.9);
            GL.End();

            GL.SwapBuffers(window.NativeWindow.HDC);
        }


        //window.StartMessageLoop(workspace);
        /*
        threadwhile(() => Cycle(workspace));

        */
    }

    public static void msg(object text) => user32.MessageBox(0, text.ToString() ?? "", "", 0);
}   