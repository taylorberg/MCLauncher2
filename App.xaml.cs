using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using TaskDialogInterop;

namespace tman0.Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException_1(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            TaskDialogOptions o = new TaskDialogOptions
            {
                Title = "Error",
                MainInstruction = "An error has occurred.",
                ExpandedByDefault = true,
                Content = e.Exception.ToString(),
                FooterText = "Please report this error by sending an email to tman0@live.com with [MCL-EXCEPTION] in the subject line containing the stack trace above."
            };
            TaskDialog.Show(o);
        }
    }
}
