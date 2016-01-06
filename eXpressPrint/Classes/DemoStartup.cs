using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraSplashScreen;
using System.Drawing;

namespace eXpressPrint
{
    class DemoStartUp : IObserver<string>
    {
        void IObserver<string>.OnCompleted()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false, 1500, AppHelper.MainForm);
        }
        void IObserver<string>.OnNext(string status)
        {
            if (DevExpress.XtraSplashScreen.SplashScreenManager.Default == null)
            {
                 SplashScreenManager.ShowForm(AppHelper.MainForm, typeof(DevExpress.XtraSplashScreen.DemoSplashScreen), true, true);
                 SplashScreenManager.Default.SendCommand(DevExpress.XtraSplashScreen.DemoSplashScreenBase.SplashScreenCommand.UpdateLabelProductText, "DevExpress WinForms Controls");
                 SplashScreenManager.Default.SendCommand(DevExpress.XtraSplashScreen.DemoSplashScreenBase.SplashScreenCommand.UpdateLabelDemoText, status);
            }
            else
            {
                 SplashScreenManager.Default.SendCommand(DevExpress.XtraSplashScreen.DemoSplashScreenBase.SplashScreenCommand.UpdateLabel, status);
            }
        }
        void IObserver<string>.OnError(Exception error) { throw error; }
    }
    public class AppHelper
    {
        public static void ProcessStart(string name)
        {
            ProcessStart(name, string.Empty);
        }
        public static void ProcessStart(string name, string arguments)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = name;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.Verb = "Open";
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.Start();
        }
        public static Icon AppIcon
        {
            get { return DevExpress.Utils.ResourceImageHelper.CreateIconFromResourcesEx("DevExpress.OutlookInspiredApp.Win.Resources.AppIcon.ico", typeof(MainForm).Assembly); }
        }
        static Image img;
        public static Image AppImage
        {
            get
            {
                if (img == null)
                    img = AppIcon.ToBitmap();
                return img;
            }
        }
        static WeakReference wRef;
        public static MainForm MainForm
        {
            get { return (wRef != null) ? wRef.Target as MainForm : null; }
            set { wRef = new WeakReference(value); }
        }
        public static float GetDefaultSize() { return 8.25F; }
    }
}
