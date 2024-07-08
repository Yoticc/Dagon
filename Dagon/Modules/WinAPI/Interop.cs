namespace Dagon.WinAPI;
public static class Interop
{
    [DllImport("gdi32")]
    public static extern pointer GetStockObject(StockObjects fnObject);
}