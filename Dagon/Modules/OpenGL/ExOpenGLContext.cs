using OpenGL;

namespace Dagon.OpenGL;
public unsafe class ExOpenGLContext
{
    public ExOpenGLContext()
    {
        var name = "wglCreateContextAttribsARB\0"u8;
        wglCreateContextAttribsARB = GL.GetProcAddress(*(byte**)&name);
        name = "wglChoosePixelFormatARB\0"u8;
        wglChoosePixelFormatARB = GL.GetProcAddress(*(byte**)&name);
    }

    pointer wglCreateContextAttribsARB;
    public nint CreateContextAttribsARB(nint hdc, nint shareContext, int[] attributeList)
    {
        fixed (int* attributeListPtr = attributeList)
            return calli<nint, nint, nint, pointer>(wglCreateContextAttribsARB, hdc, shareContext, attributeListPtr);
    }

    pointer wglChoosePixelFormatARB;
    public bool ChoosePixelFormatARB(nint hdc, int[] iAttributeList, float[] fAttributeList, uint maxFormats, out int formats, out uint numFormats)
    {
        fixed (int* iAttributeListPtr = iAttributeList)
        fixed (float* fAttributeListPtr = fAttributeList)
        {
            int formats_ = 0;
            uint numFormats_ = 0;
            var result = calli<bool, nint, pointer, pointer, uint, pointer, pointer>(wglChoosePixelFormatARB, hdc, iAttributeListPtr, fAttributeListPtr, maxFormats, &formats_, &numFormats_);
            formats = formats_;
            numFormats = numFormats_;
            return result;
        }
    }
}