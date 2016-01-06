using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;


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
        public PrintForm(Image img)
        {
            InitializeComponent();

            _PrintImage = img;
             pictureBox1.Image = img;
             
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
                comboBoxNumOfCopies.SelectedItem = "1";
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

        public void Print(Image imgImage)
        {
            // Prevents execution of below statements if filename is not selected.
            if(imgImage == null)
             return;

            try
            {
                
                var pd = new PrintDocument();

                //Disable the printing document pop-up dialog shown during printing.
                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;


                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                pd.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                pd.PrintPage += (sndr, args) =>
                {
                    Image i = imgImage;

                    //Adjust the size of the image to the page to print the full image without loosing any part of the image.
                    Rectangle m = args.MarginBounds;

                    //Logic below maintains Aspect Ratio.
                    if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                    {
                        m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                    }
                    else
                    {
                        m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                    }
                    //Calculating optimal orientation.
                    pd.DefaultPageSettings.Landscape = m.Width > m.Height;
                    //Putting image in center of page.
                    m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
                    m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
                    args.Graphics.DrawImage(i, m);
                };


                pd.PrintController = new StandardPrintController();
                pd.Print();
                
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (comboBoxNumOfCopies.SelectedIndex == 0)
            {
                MessageBox.Show(this, "Please select number of copies.", "Print Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            var copies = Convert.ToInt32(comboBoxNumOfCopies.SelectedItem);
            for (var i = 0; i <= copies-1; i++)
            {
                Print(_PrintImage);
            }
            
            Hide();
            var mainForm=new MainForm();
            mainForm.Show();
        }

        private void PrintForm_Activated(object sender, EventArgs e)
        {
            formState.Maximize(this);
            btnPrint.Select();
        }

      
    }
}
