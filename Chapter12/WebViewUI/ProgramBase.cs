using System;
using SpiderEye;

#if Linux
using SpiderEye.Linux;
#elif OSX
using SpiderEye.Mac;
#elif Windows
using SpiderEye.Windows;
#endif

using System.Runtime.InteropServices;


namespace WebViewUI
{
    public abstract class ProgramBase
    {
        protected static void Init()
		{

			#if Linux
					LinuxApplication.Init();
			#elif OSX
					MacApplication.Init();
			#elif Windows
					WindowsApplication.Init();
			#endif
		}

		protected static void Run()
        {
            // this creates a new window with default values
            using (var window = new Window())
            {
                              
                window.Icon = AppIcon.FromFile("icon", ".");

                SpiderEye.Size ws = new Size(400, 300);
                window.Size = ws;

                // this relates to the path defined in the .csproj file
                Application.ContentProvider = new EmbeddedContentProvider("App");

                // runs the application and opens the window with the given page loaded
                Application.Run(window, "index.html");
            }
        }
    }
}
