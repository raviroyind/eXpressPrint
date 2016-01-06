using System.Runtime.InteropServices;
using System.Text;

namespace eXpressPrint
{
    public class INIFile 
    {
        private string filePath;
         
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key,
        string val,
        string filePath);
 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        string filePath);

        public INIFile(string filePath)
        {
            this.filePath = filePath;
        }
 
        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.filePath);
        }
 
        public string Read(string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            var i = GetPrivateProfileString(section, key, "", sb, 255, this.filePath);
            return sb.ToString();
        }
         
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }
}
