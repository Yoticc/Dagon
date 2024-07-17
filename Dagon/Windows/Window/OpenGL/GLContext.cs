using Dagon.WinAPI;
using OpenGL;
using static Dagon.WinAPI.PixelFormatDescriptor;
using static OpenGL.Enums;

namespace Dagon.OpenGL;
// from https://gist.github.com/nickrolfe/1127313ed1dbf80254b614a721b3ee9c
public unsafe class GLContext : IDisposable
{
    #region cctor
    static GLContext()
    {
        using var window = NativeWindow.Create();
        var hdc = window.HDC;
        var pixelFormatDescriptor = new PixelFormatDescriptor
        {
            Size = (short)sizeof(PixelFormatDescriptor),
            Version = 1,
            PixelType = PixelTypeEnum.RGBA,
            Flags = FlagsEnum.DrawToBitmap | FlagsEnum.SupportOpenGL | FlagsEnum.DoubleBuffer,
            ColorBits = 32,
            AlphaBits = 8,
            LayerType = LayerTypeEnum.MainPlane,
            DepthBits = 24,
            StencilBits = 8
        };
        var pixelFormat = GDI32.ChoosePixelFormat(hdc, &pixelFormatDescriptor);
        GDI32.SetPixelFormat(hdc, pixelFormat, &pixelFormatDescriptor);
        var context = GL.CreateContext(hdc);
        GL.MakeCurrent(hdc, context);

        ContextInterface = new();

        GL.MakeCurrent(hdc, 0);
        GL.DeleteContext(context);
    }
    #endregion

    public GLContext(nint hdc)
    {
        this.hdc = hdc;
        NativeContext = CreateContext();
    }

    public readonly static ExOpenGLContext ContextInterface;
    public readonly nint NativeContext;
    readonly nint hdc;

    nint CreateContext()
    {
        const int
            DRAW_TO_WINDOW_ARB = 0x2001,
            ACCELERATION_ARB = 0x2003,
            SUPPORT_OPENGL_ARB = 0x2010,
            DOUBLE_BUFFER_ARB = 0x2011,
            PIXEL_TYPE_ARB = 0x2013,
            COLOR_BITS_ARB = 0x2014,
            DEPTH_BITS_ARB = 0x2022,
            STENCIL_BITS_ARB = 0x2023,
            FULL_ACCELERATION_ARB = 0x2027,
            TYPE_RGBA_ARB = 0x202B;

        const int
            CONTEXT_MAJOR_VERSION_ARB = 0x2091,
            CONTEXT_MINOR_VERSION_ARB = 0x2092,
            CONTEXT_PROFILE_MASK_ARB = 0x9126,
            CONTEXT_CORE_PROFILE_BIT_ARB = 1;


        int[] pixelFormatAttributions = [
            DRAW_TO_WINDOW_ARB, 1,
            SUPPORT_OPENGL_ARB, 1,
            DOUBLE_BUFFER_ARB, 1,
            ACCELERATION_ARB, FULL_ACCELERATION_ARB,
            PIXEL_TYPE_ARB, TYPE_RGBA_ARB,
            COLOR_BITS_ARB, 32,
            DEPTH_BITS_ARB, 24,
            STENCIL_BITS_ARB, 8,
            0
        ];

        if (ContextInterface.ChoosePixelFormatARB(hdc, pixelFormatAttributions, [], 1, out int pixelFormat, out uint numFormats))
            FatalError("CreateContext->ContextInterface.ChoosePixelFormatARB");
        PixelFormatDescriptor pfd;
        GDI32.DescribePixelFormat(hdc, pixelFormat, (uint)sizeof(PixelFormatDescriptor), &pfd);
        if (!GDI32.SetPixelFormat(hdc, pixelFormat, &pfd))
            FatalError("CreateContext->SetPixelFormat");
        int[] gl33Attributes = [
            CONTEXT_MAJOR_VERSION_ARB, 3,
            CONTEXT_MINOR_VERSION_ARB, 3,
            CONTEXT_PROFILE_MASK_ARB,  CONTEXT_CORE_PROFILE_BIT_ARB,
            0
        ];
        var context = ContextInterface.CreateContextAttribsARB(hdc, 0, gl33Attributes);

        if (context == 0)
            FatalError($"CreateContext->ContextInterface.CreateContextAttribsARB");

        if (!GL.MakeCurrent(hdc, context))
            FatalError($"CreateContext->MakeCurrent");

        return context;
    }

    void FatalError(string description) => user32.MessageBox(0, $"Fatal error at GlContext->{description}", "OpenGL Error", 0);

    public void Dispose()
    {
        if (!GL.MakeCurrent(hdc, 0))
            FatalError($"Dispose->MakeCurrent");
        if (!GL.DeleteContext(NativeContext))
            FatalError($"Dipose->DeleteContext");
    }
}