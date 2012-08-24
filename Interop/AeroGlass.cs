using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace tman0.Launcher.Interop
{
    static class AeroGlass
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        public static bool GlassBackground(this Window window)
        {
            if (Environment.OSVersion.Version.Major >= 6 && AeroGlass.DwmIsCompositionEnabled())
            {
                IntPtr mainWindowPtr = new WindowInteropHelper(window).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

                window.Background = Brushes.Transparent;

                MARGINS Margins = new MARGINS();
                Margins.cxLeftWidth = 1;
                Margins.cxRightWidth = -1;
                Margins.cyBottomHeight = -1;
                Margins.cyTopHeight = 1;

                int result = AeroGlass.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref Margins);
                return true;
            }
            else return false;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MARGINS
    {
        public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;
    }
}
