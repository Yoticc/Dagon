using Dagon.WinAPI;

unsafe class Program
{
    static void Main()
    {
        var hInstance = kernel32.GetModuleHandle((char*)0);

        var window = User32.CreateWindow(0, 32770, "TestApp window", WindowStyles.VISIBLE, 0, 0, 400, 400, 0, 0, hInstance, 0);

        user32.MessageBox(0, $"hInstance: {hInstance}, window: {window}, error: {kernel32.GetLastError()}", "", 0); // You need to stay in the thread
    }
}