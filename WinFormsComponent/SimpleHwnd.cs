using CommunityToolkit.Mvvm.Messaging;
using PInvoke;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsComponent.Messages;

namespace WinFormsComponent
{
    public partial class SimpleHwnd : Form
    {
        public SimpleHwnd()
        {
            InitializeComponent();

            this.Shown += OnHwndShown;
        }

        private void OnHwndShown(object sender, EventArgs e)
        {
            using (var graphics = this.CreateGraphics())
            {
                StrongReferenceMessenger.Default.Send(new WindowReadyMessage(this.Handle, graphics.DpiX / 96.0f));
            }
        }

        private void SimpleHwnd_Load(object sender, EventArgs e)
        {
            // Set DWM window attribute so it is not visible (if visual debug is not requested)
            DwmInterop.SetWindowCloak(this.Handle);

            // Hide the window from Alt+Tab / Win+Tab
            var ret = User32.SetWindowLongPtr(this.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE,
                new IntPtr(User32.GetWindowLong(this.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE) | (int)User32.WindowStylesEx.WS_EX_TOOLWINDOW));
            if (ret.ToInt64() == 0)
            {
                var err = Marshal.GetLastWin32Error();
                if (err != 0) throw new Win32Exception(err);
            }
        }
    }
}
