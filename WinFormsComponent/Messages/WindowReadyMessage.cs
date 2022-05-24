using System;

namespace WinFormsComponent.Messages
{
    public sealed class WindowReadyMessage
    {
        public IntPtr WindowHandle { get; }
        public float DpiScaling { get; }

        public WindowReadyMessage(IntPtr handle, float dpiScaling)
        {
            WindowHandle = handle;
            DpiScaling = dpiScaling;
        }
    }
}
