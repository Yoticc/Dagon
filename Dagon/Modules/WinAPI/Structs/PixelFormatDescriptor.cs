namespace Dagon.WinAPI;
public struct PixelFormatDescriptor
{
    public short Size;
    public short Version;
    public FlagsEnum Flags;
    public PixelTypeEnum PixelType;
    public byte ColorBits;
    public byte RedBits;
    public byte RedShift;
    public byte GreenBits;
    public byte GreenShift;
    public byte BlueBits;
    public byte BlueShift;
    public byte AlphaBits;
    public byte AlphaShift;
    public byte AccumBits;
    public byte AccumRedBits;
    public byte AccumGreenBits;
    public byte AccumBlueBits;
    public byte AccumAlphaBits;
    public byte DepthBits;
    public byte StencilBits;
    public byte AuxBuffers;
    public LayerTypeEnum LayerType;
    public byte Reserved;
    public int LayerMask;
    public int VisibleMask;
    public int DamageMask;

    public enum PixelTypeEnum : byte
    {
        RGBA,
        ColorIndex
    }

    public enum FlagsEnum : int
    {
        DrawToWindow = 0x00000004,
        DrawToBitmap = 0x00000008,
        SupportGDI = 0x00000010,
        SupportOpenGL = 0x00000020,
        GenericAccelerated = 0x00001000,
        GenericFormat = 0x00000040,
        NeedPalette = 0x00000080,
        NeedSystemPalette = 0x00000100,
        DoubleBuffer = 0x00000001,
        Stereo = 0x00000002,
        SwapLayersBuffers = 0x00000800
    }

    public enum LayerTypeEnum : sbyte
    {
        MainPlane = 0,
        OverlayPlane = 1,
        UnderlayPlane = -1
    }
}