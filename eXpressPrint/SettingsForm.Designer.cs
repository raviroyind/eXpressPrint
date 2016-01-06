namespace eXpressPrint
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dlgBoxBackground = new System.Windows.Forms.OpenFileDialog();
            this.btnBackGroundImage = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.rdBtnDropBox = new System.Windows.Forms.RadioButton();
            this.rdBtnLocal = new System.Windows.Forms.RadioButton();
            this.groupDrop = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDropBoxFolder = new DevExpress.XtraEditors.TextEdit();
            this.lblAcessToken = new DevExpress.XtraEditors.LabelControl();
            this.txtAccessToken = new DevExpress.XtraEditors.TextEdit();
            this.groupLocal = new DevExpress.XtraEditors.GroupControl();
            this.txtLocalPath = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseLocalFolder = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dlgBoxLocalFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.chkNoImage = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxNumOfOptions = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupDrop)).BeginInit();
            this.groupDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDropBoxFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccessToken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupLocal)).BeginInit();
            this.groupLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNoImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxNumOfOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(588, 527);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(102, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Copyright © 2015";
            // 
            // btnBackGroundImage
            // 
            this.btnBackGroundImage.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBackGroundImage.Location = new System.Drawing.Point(131, 86);
            this.btnBackGroundImage.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            this.btnBackGroundImage.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnBackGroundImage.Name = "btnBackGroundImage";
            this.btnBackGroundImage.Size = new System.Drawing.Size(60, 32);
            this.btnBackGroundImage.TabIndex = 7;
            this.btnBackGroundImage.Text = "Change";
            this.btnBackGroundImage.Click += new System.EventHandler(this.btnBackGroundImage_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Image = global::eXpressPrint.Properties.Resources.save_32x32;
            this.btnSave.Location = new System.Drawing.Point(287, 501);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 42);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 271);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 16);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Search Location";
            // 
            // rdBtnDropBox
            // 
            this.rdBtnDropBox.AutoSize = true;
            this.rdBtnDropBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rdBtnDropBox.Checked = true;
            this.rdBtnDropBox.Location = new System.Drawing.Point(134, 271);
            this.rdBtnDropBox.Name = "rdBtnDropBox";
            this.rdBtnDropBox.Size = new System.Drawing.Size(82, 21);
            this.rdBtnDropBox.TabIndex = 10;
            this.rdBtnDropBox.TabStop = true;
            this.rdBtnDropBox.Text = "Dropbox";
            this.rdBtnDropBox.UseVisualStyleBackColor = false;
            this.rdBtnDropBox.CheckedChanged += new System.EventHandler(this.rdBtnDropBox_CheckedChanged);
            // 
            // rdBtnLocal
            // 
            this.rdBtnLocal.AutoSize = true;
            this.rdBtnLocal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rdBtnLocal.Location = new System.Drawing.Point(255, 271);
            this.rdBtnLocal.Name = "rdBtnLocal";
            this.rdBtnLocal.Size = new System.Drawing.Size(63, 21);
            this.rdBtnLocal.TabIndex = 11;
            this.rdBtnLocal.Text = "Local";
            this.rdBtnLocal.UseVisualStyleBackColor = false;
            this.rdBtnLocal.CheckedChanged += new System.EventHandler(this.rdBtnLocal_CheckedChanged);
            // 
            // groupDrop
            // 
            this.groupDrop.Controls.Add(this.labelControl5);
            this.groupDrop.Controls.Add(this.labelControl4);
            this.groupDrop.Controls.Add(this.txtDropBoxFolder);
            this.groupDrop.Controls.Add(this.lblAcessToken);
            this.groupDrop.Controls.Add(this.txtAccessToken);
            this.groupDrop.Location = new System.Drawing.Point(50, 316);
            this.groupDrop.Name = "groupDrop";
            this.groupDrop.Size = new System.Drawing.Size(576, 138);
            this.groupDrop.TabIndex = 12;
            this.groupDrop.Text = "Dropbox Settings";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(342, 96);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 16);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "(if applicable)";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 96);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 16);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Folder";
            // 
            // txtDropBoxFolder
            // 
            this.txtDropBoxFolder.Location = new System.Drawing.Point(99, 93);
            this.txtDropBoxFolder.Name = "txtDropBoxFolder";
            this.txtDropBoxFolder.Size = new System.Drawing.Size(237, 22);
            this.txtDropBoxFolder.TabIndex = 11;
            // 
            // lblAcessToken
            // 
            this.lblAcessToken.Location = new System.Drawing.Point(13, 43);
            this.lblAcessToken.Name = "lblAcessToken";
            this.lblAcessToken.Size = new System.Drawing.Size(72, 16);
            this.lblAcessToken.TabIndex = 10;
            this.lblAcessToken.Text = "Acess Token";
            // 
            // txtAccessToken
            // 
            this.txtAccessToken.Location = new System.Drawing.Point(99, 40);
            this.txtAccessToken.Name = "txtAccessToken";
            this.txtAccessToken.Size = new System.Drawing.Size(436, 22);
            this.txtAccessToken.TabIndex = 0;
            // 
            // groupLocal
            // 
            this.groupLocal.Controls.Add(this.txtLocalPath);
            this.groupLocal.Controls.Add(this.btnBrowseLocalFolder);
            this.groupLocal.Controls.Add(this.labelControl6);
            this.groupLocal.Location = new System.Drawing.Point(50, 316);
            this.groupLocal.Name = "groupLocal";
            this.groupLocal.Size = new System.Drawing.Size(576, 120);
            this.groupLocal.TabIndex = 13;
            this.groupLocal.Text = "Local Settings";
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(219, 54);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(339, 22);
            this.txtLocalPath.TabIndex = 13;
            // 
            // btnBrowseLocalFolder
            // 
            this.btnBrowseLocalFolder.Image = global::eXpressPrint.Properties.Resources.loadfrom_32x32;
            this.btnBrowseLocalFolder.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBrowseLocalFolder.Location = new System.Drawing.Point(140, 48);
            this.btnBrowseLocalFolder.Name = "btnBrowseLocalFolder";
            this.btnBrowseLocalFolder.Size = new System.Drawing.Size(72, 37);
            this.btnBrowseLocalFolder.TabIndex = 12;
            this.btnBrowseLocalFolder.Click += new System.EventHandler(this.btnBrowseLocalFolder_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 59);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(104, 16);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Select Folder Path";
            // 
            // pictureBoxBackground
            // 
            this.pictureBoxBackground.Location = new System.Drawing.Point(12, 86);
            this.pictureBoxBackground.Name = "pictureBoxBackground";
            this.pictureBoxBackground.Size = new System.Drawing.Size(113, 103);
            this.pictureBoxBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBackground.TabIndex = 14;
            this.pictureBoxBackground.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Image = global::eXpressPrint.Properties.Resources.close_32x32;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(670, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 42);
            this.btnClose.TabIndex = 15;
            this.btnClose.Tag = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(106, 16);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Background Image";
            // 
            // chkNoImage
            // 
            this.chkNoImage.Location = new System.Drawing.Point(134, 138);
            this.chkNoImage.Name = "chkNoImage";
            this.chkNoImage.Properties.Caption = "Blank";
            this.chkNoImage.Size = new System.Drawing.Size(75, 20);
            this.chkNoImage.TabIndex = 17;
            this.chkNoImage.CheckedChanged += new System.EventHandler(this.chkNoImage_CheckedChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl7.Location = new System.Drawing.Point(340, 86);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(107, 16);
            this.labelControl7.TabIndex = 18;
            this.labelControl7.Text = "# of Copies Option";
            // 
            // checkedListBoxNumOfOptions
            // 
            this.checkedListBoxNumOfOptions.Location = new System.Drawing.Point(453, 86);
            this.checkedListBoxNumOfOptions.LookAndFeel.SkinName = "Metropolis";
            this.checkedListBoxNumOfOptions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.checkedListBoxNumOfOptions.LookAndFeel.TouchUI = true;
            this.checkedListBoxNumOfOptions.Name = "checkedListBoxNumOfOptions";
            this.checkedListBoxNumOfOptions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.checkedListBoxNumOfOptions.Size = new System.Drawing.Size(173, 206);
            this.checkedListBoxNumOfOptions.TabIndex = 20;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe Marker", 14F);
            this.labelControl8.Location = new System.Drawing.Point(322, 12);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(68, 26);
            this.labelControl8.TabIndex = 21;
            this.labelControl8.Text = "Settings";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(722, 566);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.checkedListBoxNumOfOptions);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.chkNoImage);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBoxBackground);
            this.Controls.Add(this.groupLocal);
            this.Controls.Add(this.rdBtnLocal);
            this.Controls.Add(this.rdBtnDropBox);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBackGroundImage);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.groupDrop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupDrop)).EndInit();
            this.groupDrop.ResumeLayout(false);
            this.groupDrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDropBoxFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccessToken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupLocal)).EndInit();
            this.groupLocal.ResumeLayout(false);
            this.groupLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNoImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxNumOfOptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.OpenFileDialog dlgBoxBackground;
        private DevExpress.XtraEditors.SimpleButton btnBackGroundImage;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.RadioButton rdBtnDropBox;
        private System.Windows.Forms.RadioButton rdBtnLocal;
        private DevExpress.XtraEditors.GroupControl groupDrop;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDropBoxFolder;
        private DevExpress.XtraEditors.LabelControl lblAcessToken;
        private DevExpress.XtraEditors.TextEdit txtAccessToken;
        private DevExpress.XtraEditors.GroupControl groupLocal;
        private DevExpress.XtraEditors.SimpleButton btnBrowseLocalFolder;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.FolderBrowserDialog dlgBoxLocalFolder;
        private DevExpress.XtraEditors.TextEdit txtLocalPath;
        private System.Windows.Forms.PictureBox pictureBoxBackground;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkNoImage;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxNumOfOptions;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}
