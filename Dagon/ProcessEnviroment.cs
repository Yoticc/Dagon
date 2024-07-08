namespace Dagon;
unsafe static class ProcessEnviroment
{
    public static readonly nint HInstance = (nint)kernel32.GetModuleHandle((char*)0);
}