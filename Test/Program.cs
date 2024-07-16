using Dagon.WinAPI;
using Dagon;

unsafe class Program
{
    static void Main()
    {
        var window = new Window(
            title: "Dagon test window",
            location: (200, 200),
            size: (640, 360),
            visible: true,
            caption: true,
            closeButton: true);

        window.StartMessageLoop();
    }

    public static void msg(object text) => user32.MessageBox(0, text.ToString() ?? "", "", 0);
}   