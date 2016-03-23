using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using eXpressPrint.Properties;

namespace eXpressPrint
{
    public partial class SettingsForm : SplashScreen
    {
        FormState _formState = new FormState();
        public SettingsForm()
        {
            InitializeComponent();
            for (var i = 1; i <= 50; i++)
            {
                checkedListBoxNumOfOptions.Items.Add(i.ToString());
            }
            LoadConfig();

            if (rdBtnDropBox.Checked)
            {
                groupDrop.Visible = true;
                groupLocal.Visible = false;
                rdBtnLocal.Checked = false;
            }
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion


        private void LoadConfig()
        {
            var inif = new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\config.ini");

            if (!string.IsNullOrEmpty(inif.Read("Path_Settings", "BG_Path")))
            {
                if (inif.Read("Path_Settings", "BG_Path").ToUpper().Equals("NO_IMAGE"))
                {
                    pictureBoxBackground.Image = Resources.noimage;
                    chkNoImage.Checked = true;
                    btnBackGroundImage.Enabled = false;
                }
                else
                {
                    pictureBoxBackground.Image = Image.FromFile(inif.Read("Path_Settings", "BG_Path"));
                    chkNoImage.Checked = false;
                    btnBackGroundImage.Enabled = true;
                }
            }


            if (!string.IsNullOrEmpty(inif.Read("Location_Settings", "Default_Location")))
            {
                if (Convert.ToString(inif.Read("Location_Settings", "Default_Location")).ToUpper().Equals("DBOX"))
                {
                    rdBtnDropBox.Checked = true;
                    groupDrop.Visible = true;
                    groupLocal.Visible = false;
                    rdBtnLocal.Checked = false;

                    if (!string.IsNullOrEmpty(inif.Read("DROP_SET", "Access_Token")))
                        txtAccessToken.EditValue = inif.Read("DROP_SET", "Access_Token");

                    if (!string.IsNullOrEmpty(inif.Read("DROP_SET", "Drop_Folder")))
                        txtDropBoxFolder.EditValue = inif.Read("DROP_SET", "Drop_Folder");

                    if (!string.IsNullOrEmpty(inif.Read("LOCAL_SET", "Local_Path")))
                        txtLocalPath.EditValue = inif.Read("LOCAL_SET", "Local_Path");

                }
                else if (Convert.ToString(inif.Read("Location_Settings", "Default_Location")).ToUpper().Equals("LOCAL"))
                {
                    rdBtnLocal.Checked = true;
                    groupLocal.Visible = true;
                    groupDrop.Visible = false;
                    rdBtnDropBox.Checked = false;

                    if (!string.IsNullOrEmpty(inif.Read("LOCAL_SET", "Local_Path")))
                        txtLocalPath.EditValue = inif.Read("LOCAL_SET", "Local_Path");

                    if (!string.IsNullOrEmpty(inif.Read("DROP_SET", "Access_Token")))
                        txtAccessToken.EditValue = inif.Read("DROP_SET", "Access_Token");

                    if (!string.IsNullOrEmpty(inif.Read("DROP_SET", "Drop_Folder")))
                        txtDropBoxFolder.EditValue = inif.Read("DROP_SET", "Drop_Folder");
                }

                if (!string.IsNullOrEmpty(inif.Read("PRINT_OPT", "AUTO")))
                {
                    if (inif.Read("PRINT_OPT", "AUTO").Equals("Y"))
                        chkAutoPrint.Checked = true;
                    else
                        chkAutoPrint.Checked = false;
                }
                     
            }


            if (!string.IsNullOrEmpty(inif.Read("PRINT_SET", "COPIES")))
            {

                var arr = new ArrayList();

                arr.AddRange(inif.Read("PRINT_SET", "COPIES").Split(','));

                var sortedList = arr.Cast<string>().OrderBy(item => int.Parse(item));

                foreach (var item in sortedList)
                {
                    checkedListBoxNumOfOptions.Items[item].CheckState = CheckState.Checked;
                }
 
            }
             
        }

        private void btnBackGroundImage_Click(object sender, EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog
            {
                Multiselect = false,
                Filter="Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
            })
            {if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxBackground.Image = Image.FromFile(openFileDlg.FileName);
                    pictureBoxBackground.ImageLocation = openFileDlg.FileName;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rdBtnDropBox.Checked ==false && rdBtnLocal.Checked == false)
            {
                XtraMessageBox.Show("Please select one of search location (DropBox or Local).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rdBtnDropBox.Checked)
            {
                if (string.IsNullOrEmpty((string) txtAccessToken.EditValue))
                {XtraMessageBox.Show("Please enter a value for Access Token.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (rdBtnLocal.Checked)
            {
                if (string.IsNullOrEmpty((string)txtLocalPath.EditValue))
                {
                    XtraMessageBox.Show("Please select a local folder for default search location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            SaveIniFile(SaveBgImage());
        }

        private string SaveBgImage()
        {
            if (chkNoImage.Checked)
                return "NO_IMAGE";

            var imgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\BGImages\\";
            var img = pictureBoxBackground.Image;

            img.Save(imgPath + GetUniqueFileName() + Path.GetExtension(pictureBoxBackground.ImageLocation));
            return imgPath + GetUniqueFileName() + Path.GetExtension(pictureBoxBackground.ImageLocation);
        }

        private static string GetUniqueFileName()
        {
            var rndRandom=new Random(13959);
            var returnPath= "bg"+ rndRandom.Next(int.MinValue, int.MaxValue).ToString() + DateTime.Now.Minute;
            return returnPath;
        }

        private void SaveIniFile(string bgImagePath)
        {
            var inif = new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\eXpressPrint\\config.ini");
            inif.Write("Path_Settings", "BG_Path", bgImagePath);

            inif.Write("Location_Settings", "Default_Location", rdBtnDropBox.Checked? "DBOX":"LOCAL");

            inif.Write("DROP_SET", "Access_Token",
                !string.IsNullOrEmpty(Convert.ToString(txtAccessToken.EditValue))
                    ? txtAccessToken.EditValue.ToString()
                    : "");

            inif.Write("DROP_SET", "Drop_Folder",
                !string.IsNullOrEmpty(Convert.ToString(txtDropBoxFolder.EditValue))
                    ? "/" + txtDropBoxFolder.EditValue
                    : "");

            inif.Write("LOCAL_SET", "Local_Path",
                !string.IsNullOrEmpty(Convert.ToString(txtLocalPath.EditValue)) ? txtLocalPath.EditValue.ToString() : "");

            var numberOfCopies = checkedListBoxNumOfOptions.CheckedItems.Cast<CheckedListBoxItem>().Aggregate(string.Empty, (current, item) => current + (item.Value.ToString() + ","));

            if (!string.IsNullOrEmpty(numberOfCopies))
                numberOfCopies = numberOfCopies.Substring(0, numberOfCopies.Length - 1);

            inif.Write("PRINT_SET", "COPIES",numberOfCopies);

            if (chkAutoPrint.Checked)
                inif.Write("PRINT_OPT", "AUTO", "Y");
            else
                inif.Write("PRINT_OPT", "AUTO", "N");
            
            XtraMessageBox.Show("Saved, please restart the application.", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm mainForm=new MainForm();
            mainForm.Update();
        }

        private void rdBtnDropBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnDropBox.Checked)
            {
                groupDrop.Visible = true;
                groupLocal.Visible = false;
                rdBtnLocal.Checked = false;
            }
        }

        private void rdBtnLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnLocal.Checked)
            {
                groupLocal.Visible = true;
                groupDrop.Visible = false;rdBtnDropBox.Checked = false;
            }
        }

        private void chkNoImage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoImage.Checked)
            {
                pictureBoxBackground.Image = Resources.noimage;
                btnBackGroundImage.Enabled = false;
            }
            else
            {
                btnBackGroundImage.Enabled = true;
            }
        }

        private void btnBrowseLocalFolder_Click(object sender, EventArgs e)
        {
            using(var folderBrowseDlgBox =new FolderBrowserDialog
            {
                RootFolder= Environment.SpecialFolder.Desktop
            }
                 ){
                if (folderBrowseDlgBox.ShowDialog() == DialogResult.OK)
                    txtLocalPath.EditValue = folderBrowseDlgBox.SelectedPath;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

         
         
    }
}