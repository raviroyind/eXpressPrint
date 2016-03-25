using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using eXpressPrint.Classes;


namespace eXpressPrint
{
    public partial class PrintForm : XtraForm
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x02;
        private const UInt32 MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x04;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x10;
        FormState formState = new FormState();
        PictureBox picBoxView;
        Image _PrintImage;
        private string _fileName;
        public PrintForm(Image img,string fileName)
        {
            InitializeComponent();

            _PrintImage = img;
             pictureBox1.Image = img;
            _fileName = fileName;
        }


         
        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            picBox.Image = picImage;
            picBox.Location = new Point((picBox.Parent.ClientSize.Width / 2) - (picImage.Width / 2),
                                        (picBox.Parent.ClientSize.Height /2) - (picImage.Height / 2));
            picBox.Refresh();
            picBox.Update();
        }
        private void PrintForm_Load(object sender, EventArgs e)
        { 
            var inif = new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\config.ini");

            if (!string.IsNullOrEmpty(inif.Read("PRINT_OPT", "AUTO")))
            {
                if (inif.Read("PRINT_OPT", "AUTO").Equals("Y"))
                    PrintAndReturn(_PrintImage);
            }


            if (!string.IsNullOrEmpty(inif.Read("PRINT_SET", "COPIES")))
            {
                var arr = new ArrayList();
               
                arr.AddRange(inif.Read("PRINT_SET", "COPIES").Split(','));

                var sortedList = arr.Cast<string>().OrderBy(item => int.Parse(item));

                foreach (var item in sortedList)
                {
                    comboBoxNumOfCopies.Items.Add(item);
                }

                comboBoxNumOfCopies.Items.Insert(0,"--Select--");
                comboBoxNumOfCopies.SelectedIndex = 0;
            }
            
            formState.Maximize(this);
            txtNumberOfCopies.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm =new MainForm();
            mainForm.Show();
        }

        public void PrintAndReturn(Image imgImage)
        {
            if (_PrintImage.Width < _PrintImage.Height)
                PrintPotrait(_PrintImage);
            else
                Print(_PrintImage);
            

            Hide();
            var mainForm = new MainForm();
            mainForm.Show();
            return;
        }
       


        private static Image RotateImage(Image imgIn)
        {
            Image img = imgIn;
            //create an object that we can use to examine an image file
            //rotate the picture by 90 degrees and re-save the picture as a Jpeg
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return img;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (comboBoxNumOfCopies.SelectedIndex == 0 || comboBoxNumOfCopies.SelectedIndex==-1)
            {
                XtraMessageBox.Show(this,
                    "Please select number of copies.", "Print Settings",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Asterisk);
                   //MessageBox.Show(this, "Please select number of copies.", "Print Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (PrinterUtility.GetDefaultPrinters().Cast<ManagementBaseObject>().Any(printer => !printer.IsOnline()))
            {
                XtraMessageBox.Show(this,
                    "Printer is Offline or malfunctioned", "Printer status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            var copies = Convert.ToInt32(comboBoxNumOfCopies.SelectedItem);
            for (var i = 0; i <= copies-1; i++)
            {
                if (_PrintImage.Width < _PrintImage.Height)
                    PrintPotrait(_PrintImage);
                else
                    Print(_PrintImage);
            }
            
            Hide();
            var mainForm=new MainForm();
            mainForm.UpdateLabel("Image " + _fileName + ".jpg sent to printer.");
            mainForm.Show();
        }


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
            if ((double)_PrintImage.Width / (double)_PrintImage.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)_PrintImage.Height / (double)_PrintImage.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)_PrintImage.Width / (double)_PrintImage.Height * (double)m.Height);
            }
            //Calculating optimal orientation.
            //printDocumentImg.DefaultPageSettings.Landscape = m.Width > m.Height;
            //Putting image in center of page.
            //m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
            //m.X = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);

            e.Graphics.DrawImage(_PrintImage, m);
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
            if ((double)_PrintImage.Width / (double)_PrintImage.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)_PrintImage.Height / (double)_PrintImage.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)_PrintImage.Width / (double)_PrintImage.Height * (double)m.Height);
            }
            //Calculating optimal orientation.
            printDocumentPotraitImg.DefaultPageSettings.Landscape = m.Width > m.Height;
            //Putting image in center of page.
            m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
            m.X = (int)((((System.Drawing.Printing.PrintDocument)(sender)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
            e.Graphics.DrawImage(_PrintImage, m);

        }

        private void PrintForm_Activated(object sender, EventArgs e)
        {
            formState.Maximize(this);
            btnPrint.Select();
        }

        
    }
}
