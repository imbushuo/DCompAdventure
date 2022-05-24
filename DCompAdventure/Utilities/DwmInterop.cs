using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DCompAdventure.Utilities
{
    public static class DwmInterop
    {
        public enum DWMWINDOWATTRIBUTE : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_PASSIVE_UPDATE_MODE,
            DWMWA_USE_HOSTBACKDROPBRUSH,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR,
            DWMWA_CAPTION_COLOR,
            DWMWA_TEXT_COLOR,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            DWMWA_LAST
        };

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int pvAttr, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int pvAttr, int attrSize);

        /// <summary>
        /// Enable immersive dark mode flag for the application window. We don't care about downlevel Windows 10
        /// so using 20H1+ flag isn't a main concern here.
        /// </summary>
        public static bool SetImmersiveDarkMode(IntPtr handle)
        {
            var flag = 1;
            return DwmSetWindowAttribute(handle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref flag, sizeof(int)) == 0;
        }

        public static void SetWindowCloak(IntPtr handle)
        {
            var flag = 1;
            int result = DwmSetWindowAttribute(handle, DWMWINDOWATTRIBUTE.DWMWA_CLOAK, ref flag, sizeof(int));
            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }
    }
}
