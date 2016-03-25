using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Dropbox.Api;
using System.Management;
using eXpressPrint.Classes;

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
            UpdateLabel("");

            if (string.IsNullOrEmpty(Convert.ToString(txtPhotoId.EditValue)))
            {
                XtraMessageBox.Show(this, "Please enter a valid photo id.", "Invalid Search",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Asterisk);
                 
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
                var inif = new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\config.ini");
                var mode = inif.Read("PRINT_OPT", "AUTO");

                if (mode.Equals("N"))
                {
                    var frmPrintForm = new PrintForm(PrintImage,txtPhotoId.EditValue.ToString());
                    frmPrintForm.Show();
                    this.Hide();
                }
                else
                {
                    if (PrintImage.Width < PrintImage.Height)
                        PrintPotrait(PrintImage);
                    else
                        Print(PrintImage);
                }
            }
            else
            {
                XtraMessageBox.Show(this,
                      "We are unable to find your photo at this time."+Environment.NewLine+
                       "Please email your event name and photo code to info@getphocial.com","Not Found", 
                      MessageBoxButtons.OK, 
                      MessageBoxIcon.Asterisk);

                txtPhotoId.EditValue = null;
                txtPhotoId.Focus();
            }
             
        }

        #endregion Events...

        private void MainForm_Activated(object sender, EventArgs e)
        {
            txtPhotoId.Select();
            txtPhotoId.Focus();
        }


        #region Print...
  
        public void Print(Image imgImage)
        {

            if (PrinterUtility.GetDefaultPrinters().Cast<ManagementBaseObject>().Any(printer => !printer.IsOnline()))
            {
                XtraMessageBox.Show(this,
                    "Printer is Offline or malfunctioned", "Printer status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            // Prevents execution of below statements if filename is not selected.
            if (imgImage == null)
                return;

            try
            {
                UpdateLabel("Image " + txtPhotoId.EditValue + ".jpg sent to printer.");
                UpdatePhotoId();
                this.BringToFront();
                this.Focus();
                //formState.Maximize(this);
                txtPhotoId.Focus();

                printDocumentImg = new PrintDocument();

                printDocumentImg.PrintPage += printDocumentImg_PrintPage; // the missing piece
                printDocumentImg.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printDocumentImg.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                //Disable the printing document pop-up dialog shown during printing.
                PrintController printController = new StandardPrintController();
                printDocumentImg.PrintController = printController;

                if (imgImage.Width > imgImage.Height)
                    imgImage = RotateImage(imgImage);


                printDocumentImg.PrintController = new StandardPrintController();
                printDocumentImg.Print();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Printer error");
            }
        }

        private void printDocumentImg_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Adjust the size of the image to the page to print the full image without loosing any part of the image.
            var m = e.MarginBounds;

            //Logic below maintains Aspect Ratio.
            if ((double)PrintImage.Width / (double)PrintImage.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)PrintImage.Height / (double)PrintImage.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)PrintImage.Width / (double)PrintImage.Height * (double)m.Height);
            }
            //Calculating optimal orientation.
            //printDocumentImg.DefaultPageSettings.Landscape = m.Width > m.Height;
            //Putting image in center of page.
            //m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
            //m.X = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);

            e.Graphics.DrawImage(PrintImage, m);
        }

        public void PrintPotrait(Image imgImage)
        {
            if (PrinterUtility.GetDefaultPrinters().Cast<ManagementBaseObject>().Any(printer => !printer.IsOnline()))
            {
                XtraMessageBox.Show(this,
                    "Printer is Offline or malfunctioned", "Printer status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            // Prevents execution of below statements if filename is not selected.
            if (imgImage == null)
                return;

            try
            {
                UpdateLabel("Image " + txtPhotoId.EditValue + ".jpg sent to printer.");
                UpdatePhotoId();
                this.BringToFront();
                this.Focus();
                formState.Maximize(this);
                txtPhotoId.Focus();


                printDocumentPotraitImg = new PrintDocument();

                printDocumentPotraitImg.PrintPage += printDocumentPotraitImg_PrintPage; // the missing piece
                printDocumentPotraitImg.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                printDocumentPotraitImg.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                //Disable the printing document pop-up dialog shown during printing.
                PrintController printController = new StandardPrintController();
                printDocumentPotraitImg.PrintController = printController;

                printDocumentPotraitImg.PrintController = new StandardPrintController();
                printDocumentPotraitImg.Print();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Printer error");
            }
        }
         
        private void printDocumentPotraitImg_PrintPage(object sender, PrintPageEventArgs e)
        {

            Rectangle m = e.MarginBounds;

            //Logic below maintains Aspect Ratio.
            if ((double)PrintImage.Width / (double)PrintImage.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)PrintImage.Height / (double)PrintImage.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)PrintImage.Width / (double)PrintImage.Height * (double)m.Height);
            }
            //Calculating optimal orientation.
            printDocumentPotraitImg.DefaultPageSettings.Landscape = m.Width > m.Height;
            //Putting image in center of page.
            m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
            m.X = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
            e.Graphics.DrawImage(PrintImage, m);
           
        }
         
        private static Image RotateImage(Image imgIn)
        {
            Image img = imgIn;
            //create an object that we can use to examine an image file
            //rotate the picture by 90 degrees and re-save the picture as a Jpeg
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return img;
        }

        public static Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public void UpdateLabel(string message)
        {
            if (this.lblMessage.InvokeRequired)
            {
                this.lblMessage.BeginInvoke((MethodInvoker)delegate () { this.lblMessage.Text = message; });
            }
            else
            {
                this.lblMessage.Text = message;
            }
        }
         
        private void UpdatePhotoId()
        {
            if (this.txtPhotoId.InvokeRequired)
            {
                this.txtPhotoId.BeginInvoke((MethodInvoker)delegate () { this.txtPhotoId.EditValue = null; });
            }
            else
            {
                this.txtPhotoId.EditValue = null;
            }
        }

        #endregion Print...

       
    }
}