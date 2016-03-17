using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Management;

namespace eXpressPrint.Classes
{
    public static class PrinterUtility
    {
        public static bool IsOnline(this ManagementBaseObject printer)
        {
            var status = printer["PrinterStatus"];
            var workOffline = (bool)printer["WorkOffline"];
            if (workOffline) return false;

            int statusAsInteger = Int32.Parse(status.ToString());
            switch (statusAsInteger)
            {
                case 3: //Idle
                case 4: //Printing
                case 5: //Warming up
                case 6: //Stopped printing
                    return true;
                default:
                    return false;
            }
        }

        public static ManagementObjectCollection GetDefaultPrinters()
        {
            var printerSearcher =
              new ManagementObjectSearcher(
                "SELECT * FROM Win32_Printer where Default = true"
              );
            return printerSearcher.Get();
        }
    }
}
