using Dagon.WinAPI;
using Dagon.Windows;

unsafe class Program
{
    static void Main()
    {
        var window = NativeWindow.Create();
        window.Title = "Some Title";
        msg(window.Title);

        msg($"hInstance: window: {window.Handle}, error: {kernel32.GetLastError()}"); // It need to stay in the thread       
    }

    [NativeFunc]
    public static long WndProc(nint hWnd, WindowMessage msg, long wParam, nint lParam)
    {
        Console.WriteLine($"{hWnd}, {msg}, {wParam}, {lParam}");

        return user32.DefWindowProc(hWnd, (uint)msg, (ulong)wParam, lParam);
    }

    public static void msg(object text) => user32.MessageBox(0, text.ToString()??"", "", 0);
}