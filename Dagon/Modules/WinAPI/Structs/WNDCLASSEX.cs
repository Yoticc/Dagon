namespace Dagon.WinAPI;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct WNDCLASSEX
{
    public int size;
    public int style;
    public nint fnWndProc;
    public int clsExtra;
    public int wndExtra;
    public nint hInstance;
    public nint hIcon;
    public nint hCursor;
    public nint hBackground;
    public string menuName;
    public string className;
    public nint hIconSm;
}