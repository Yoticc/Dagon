namespace Dagon.WinAPI;
public static unsafe class GDI32
{
    const string gdi = "gdi32";

    [DllImport(gdi)] public static extern
        pointer GetStockObject(StockObjects fnObject);

    [DllImport(gdi)] public static extern
        int ChoosePixelFormat(nint hdc, PixelFormatDescriptor* ppfd);

    [DllImport(gdi)] public static extern
        int DescribePixelFormat(nint hdc, int pixelFormat, uint bytes, PixelFormatDescriptor* ppfd);

    [DllImport(gdi)] public static extern
        bool SetPixelFormat(nint hdc, int format, PixelFormatDescriptor* ppfd);

    [DllImport(gdi)] public static extern
        bool SwapBuffers(nint hdc);
}