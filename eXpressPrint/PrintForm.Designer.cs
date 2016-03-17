namespace eXpressPrint
{
    partial class PrintForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
            this.splitContainerPrint = new DevExpress.XtraEditors.SplitContainerControl();
            this.comboBoxNumOfCopies = new System.Windows.Forms.ComboBox();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumberOfCopies = new DevExpress.XtraEditors.TextEdit();
            this.lblNumberOfCopies = new DevExpress.XtraEditors.LabelControl();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrint)).BeginInit();
            this.splitContainerPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfCopies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerPrint
            // 
            this.splitContainerPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrint.Horizontal = false;
            this.splitContainerPrint.IsSplitterFixed = true;
            this.splitContainerPrint.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPrint.Name = "splitContainerPrint";
            this.splitContainerPrint.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.splitContainerPrint.Panel1.Controls.Add(this.comboBoxNumOfCopies);
            this.splitContainerPrint.Panel1.Controls.Add(this.btnPrint);
            this.splitContainerPrint.Panel1.Controls.Add(this.txtNumberOfCopies);
            this.splitContainerPrint.Panel1.Controls.Add(this.lblNumberOfCopies);
            this.splitContainerPrint.Panel1.Controls.Add(this.btnBack);
            this.splitContainerPrint.Panel1.Text = "Panel1";
            this.splitContainerPrint.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainerPrint.Panel2.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.splitContainerPrint.Panel2.Text = "Panel2";
            this.splitContainerPrint.Size = new System.Drawing.Size(1583, 953);
            this.splitContainerPrint.SplitterPosition = 84;
            this.splitContainerPrint.TabIndex = 0;
            this.splitContainerPrint.Text = "splitContainerControl1";
            // 
            // comboBoxNumOfCopies
            // 
            this.comboBoxNumOfCopies.FormattingEnabled = true;
            this.comboBoxNumOfCopies.Location = new System.Drawing.Point(689, 35);
            this.comboBoxNumOfCopies.Name = "comboBoxNumOfCopies";
            this.comboBoxNumOfCopies.Size = new System.Drawing.Size(92, 24);
            this.comboBoxNumOfCopies.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::eXpressPrint.Properties.Resources.print_32x32;
            this.btnPrint.Location = new System.Drawing.Point(806, 21);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(91, 49);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtNumberOfCopies
            // 
            this.txtNumberOfCopies.Location = new System.Drawing.Point(689, 35);
            this.txtNumberOfCopies.Name = "txtNumberOfCopies";
            this.txtNumberOfCopies.Size = new System.Drawing.Size(58, 22);
            this.txtNumberOfCopies.TabIndex = 2;
            this.txtNumberOfCopies.Visible = false;
            // 
            // lblNumberOfCopies
            // 
            this.lblNumberOfCopies.Location = new System.Drawing.Point(579, 38);
            this.lblNumberOfCopies.Name = "lblNumberOfCopies";
            this.lblNumberOfCopies.Size = new System.Drawing.Size(104, 16);
            this.lblNumberOfCopies.TabIndex = 1;
            this.lblNumberOfCopies.Text = "Number Of Copies";
            // 
            // btnBack
            // 
            this.btnBack.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.btnBack.Appearance.Options.UseFont = true;
            this.btnBack.Image = global::eXpressPrint.Properties.Resources.undo_32x32;
            this.btnBack.Location = new System.Drawing.Point(9, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(125, 59);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(206, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1196, 862);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // PrintForm
            // 
            this.AcceptButton = this.btnPrint;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1583, 953);
            this.Controls.Add(this.splitContainerPrint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Sharp Plus";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            //this.LookAndFeel.TouchUI = true;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.PrintForm_Activated);
            this.Load += new System.EventHandler(this.PrintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrint)).EndInit();
            this.splitContainerPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberOfCopies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerPrint;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.TextEdit txtNumberOfCopies;
        private DevExpress.XtraEditors.LabelControl lblNumberOfCopies;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox comboBoxNumOfCopies;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}