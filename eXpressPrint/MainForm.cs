using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Dropbox.Api;

namespace eXpressPrint
{
    public partial class MainForm :  XtraForm
    {
        #region Properties...
        public enum SearchMode
        {
            DropBox,
            Local
        }

        FormState formState = new FormState();

        public  SearchMode Mode { get; set; }
        public static string AccessToken { get; set; }
        public static string DropBoxFolder { get; set; }
        public static string LocalFolder { get; set; }
        public static string Query { get; set; }
        public static Image BackGround { get; set; }
        public static Image PrintImage { get; set; }

        public const string AllowedExtensions = @".tif,.tiff,.gif,.jpeg,jpg,.jif,.jfif,.jp2,.jpx,.j2k,.j2c,.fpx,.pcd,.png,.pdf";
        #endregion Properties...

        #region Functions...
        public MainForm()
        {
            InitializeComponent();
            CheckFolders();
            LoadSettings();
        }

        private void LoadSettings()
        {
            var inif = new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\config.ini");
            switch (inif.Read("Location_Settings", "Default_Location").ToUpper())
            {
                case "DBOX":
                    Mode = SearchMode.DropBox;
                    AccessToken = inif.Read("DROP_SET", "Access_Token");
                    DropBoxFolder = inif.Read("DROP_SET", "Drop_Folder").Trim()==""
                        ? "": inif.Read("DROP_SET", "Drop_Folder");
                    break;
                case "LOCAL":
                    Mode = SearchMode.Local;
                    LocalFolder = inif.Read("LOCAL_SET", "Local_Path");
                    break;
                default:
                     Mode = SearchMode.DropBox;
                    AccessToken = inif.Read("DROP_SET", "Access_Token");
                    DropBoxFolder = inif.Read("DROP_SET", "Drop_Folder").Trim()==""
                        ? "": inif.Read("DROP_SET", "Drop_Folder");
                        break;
            }

            if (!string.IsNullOrEmpty(inif.Read("Path_Settings", "BG_Path")))
            {
                if (inif.Read("Path_Settings", "BG_Path").Trim().ToUpper()!="NO_IMAGE")
                    GeneratePictureBox(Image.FromFile(inif.Read("Path_Settings", "BG_Path")));
            }

            txtPhotoId.Select();
        }

        private void GeneratePictureBox(Image imgImage)
        {
            var pBox = new PictureBox{
                Image=imgImage,
                SizeMode=PictureBoxSizeMode.AutoSize,
                Dock = DockStyle.Fill
            };
            
            this.splitContainerControl1.Panel2.Controls.Clear();
            this.splitContainerControl1.Panel2.Controls.Add(pBox);
            this.splitContainerControl1.Panel2.Update();
        }

        private static void CheckFolders()
        {
            var appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\";
            if (!Directory.Exists(appPath))
            {
                try
                { Directory.CreateDirectory(appPath); }
                catch (Exception ex)
                { }
            }

            var appImg = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\BGImages\\";
            if (!Directory.Exists(appImg))
            {
                try
                { Directory.CreateDirectory(appImg); }
                catch (Exception ex)
                { }
            }

        }

        private  void GetLocalSearch()
        {
            SplashScreenManager.ShowForm(ActiveForm, typeof(WaitForm1), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription("Searching...");
             
            DirectoryInfo dInfo = new DirectoryInfo(LocalFolder);

            FileInfo[] fInfoArray =   dInfo.GetFiles(txtPhotoId.EditValue + ".*", SearchOption.AllDirectories);

            if (fInfoArray != null)
            {
                foreach (var file in fInfoArray)
                {
                  var searchFile=  txtPhotoId.EditValue.ToString().ToLower() + file.Extension;
                    if (file.Name.ToLower().Equals(searchFile.ToLower()))
                    {
                        var image = Image.FromFile(file.FullName);
                        PrintImage = image;
                    }
                }
            }
            
            SplashScreenManager.CloseForm(false);
        }

        static async Task Run()
        {
            SplashScreenManager.ShowForm(ActiveForm, typeof(WaitForm1), true, true, false);
            SplashScreenManager.Default.SetWaitFormDescription("Searching...");

            using (var dbx = new DropboxClient(AccessToken))
            {
                var list = await dbx.Files.ListFolderAsync(DropBoxFolder);

                foreach (var item in list.Entries.Where(item => item.Name.ToLower().Contains(Query.ToLower())))
                {
                    var searchFor = string.IsNullOrEmpty(DropBoxFolder) ? "/" + item.Name : DropBoxFolder + "/" + item.Name;
                    var file = await dbx.Files.DownloadAsync(searchFor);
                    var filebypes = file.GetContentAsByteArrayAsync();
                    var image = Image.FromStream(new MemoryStream(filebypes.Result));

                    PrintImage = image;
                }
            }

            SplashScreenManager.CloseForm(false);
        }

        private async Task Download(DropboxClient dbx, string folder, string file)
        {
            var imx = await dbx.Files.DownloadAsync(folder + file);
        }

        #endregion Functions...

        #region Events...
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            formState.Maximize(this);
        }
         
        private void btnSettings_Click(object sender, EventArgs e)
        {
            var frmSettingsForm=new SettingsForm();
            frmSettingsForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtPhotoId.EditValue)))
            {
                MessageBox.Show(this, "Please enter a valid photo id.","Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhotoId.Select();
                return;
            }


            PrintImage = null;
            Query = (string)txtPhotoId.EditValue;

            switch (this.Mode){
                case SearchMode.DropBox:
                    var task = Task.Run((Func<Task>)MainForm.Run);
                        task.Wait();
                    break;
                case SearchMode.Local:
                    GetLocalSearch();
                    break;
                default:
                    var taskdefault = Task.Run((Func<Task>)MainForm.Run);
                    taskdefault.Wait();
                    break;
                     
            }


            if (PrintImage != null)
            {
                var frmPrintForm = new PrintForm(PrintImage);
                frmPrintForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(this, "Sorry no image found for Id - \"" + txtPhotoId.EditValue + "\"", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Events...

        private void MainForm_Activated(object sender, EventArgs e)
        {
            txtPhotoId.Select();
            txtPhotoId.Focus();
        }
    }
}