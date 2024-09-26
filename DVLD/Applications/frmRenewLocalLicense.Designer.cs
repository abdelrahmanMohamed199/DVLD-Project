namespace DVLD.Applications
{
    partial class frmRenewLocalLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlApplicationInfoWithFilter1 = new DVLD.Applications.ctrlApplicationInfoWithFilter();
            this.gbNewLicenseAppInfo = new System.Windows.Forms.GroupBox();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblExpDate = new System.Windows.Forms.Label();
            this.lblOldLicenseID = new System.Windows.Forms.Label();
            this.lblRenewedLicenseID = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblLicenseFees = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.lblissueDate = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblRenewAppID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.llShowLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.gbNewLicenseAppInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(306, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Renew License Application";
            // 
            // ctrlApplicationInfoWithFilter1
            // 
            this.ctrlApplicationInfoWithFilter1.Location = new System.Drawing.Point(0, 29);
            this.ctrlApplicationInfoWithFilter1.Name = "ctrlApplicationInfoWithFilter1";
            this.ctrlApplicationInfoWithFilter1.Size = new System.Drawing.Size(806, 368);
            this.ctrlApplicationInfoWithFilter1.TabIndex = 1;
            this.ctrlApplicationInfoWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlApplicationInfoWithFilter1_OnLicenseSelected);
            // 
            // gbNewLicenseAppInfo
            // 
            this.gbNewLicenseAppInfo.Controls.Add(this.lblTotalFees);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblCreatedBy);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblExpDate);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblOldLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblRenewedLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.tbNotes);
            this.gbNewLicenseAppInfo.Controls.Add(this.label15);
            this.gbNewLicenseAppInfo.Controls.Add(this.label16);
            this.gbNewLicenseAppInfo.Controls.Add(this.label17);
            this.gbNewLicenseAppInfo.Controls.Add(this.label18);
            this.gbNewLicenseAppInfo.Controls.Add(this.label19);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblLicenseFees);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblAppFees);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblissueDate);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblAppDate);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblRenewAppID);
            this.gbNewLicenseAppInfo.Controls.Add(this.label7);
            this.gbNewLicenseAppInfo.Controls.Add(this.label6);
            this.gbNewLicenseAppInfo.Controls.Add(this.label5);
            this.gbNewLicenseAppInfo.Controls.Add(this.label4);
            this.gbNewLicenseAppInfo.Controls.Add(this.label3);
            this.gbNewLicenseAppInfo.Controls.Add(this.label2);
            this.gbNewLicenseAppInfo.Location = new System.Drawing.Point(12, 403);
            this.gbNewLicenseAppInfo.Name = "gbNewLicenseAppInfo";
            this.gbNewLicenseAppInfo.Size = new System.Drawing.Size(794, 184);
            this.gbNewLicenseAppInfo.TabIndex = 2;
            this.gbNewLicenseAppInfo.TabStop = false;
            this.gbNewLicenseAppInfo.Text = "New License Application Info";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Location = new System.Drawing.Point(509, 117);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(25, 13);
            this.lblTotalFees.TabIndex = 31;
            this.lblTotalFees.Text = "???";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(509, 94);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(25, 13);
            this.lblCreatedBy.TabIndex = 30;
            this.lblCreatedBy.Text = "???";
            // 
            // lblExpDate
            // 
            this.lblExpDate.AutoSize = true;
            this.lblExpDate.Location = new System.Drawing.Point(509, 71);
            this.lblExpDate.Name = "lblExpDate";
            this.lblExpDate.Size = new System.Drawing.Size(25, 13);
            this.lblExpDate.TabIndex = 29;
            this.lblExpDate.Text = "???";
            // 
            // lblOldLicenseID
            // 
            this.lblOldLicenseID.AutoSize = true;
            this.lblOldLicenseID.Location = new System.Drawing.Point(509, 48);
            this.lblOldLicenseID.Name = "lblOldLicenseID";
            this.lblOldLicenseID.Size = new System.Drawing.Size(25, 13);
            this.lblOldLicenseID.TabIndex = 28;
            this.lblOldLicenseID.Text = "???";
            // 
            // lblRenewedLicenseID
            // 
            this.lblRenewedLicenseID.AutoSize = true;
            this.lblRenewedLicenseID.Location = new System.Drawing.Point(509, 25);
            this.lblRenewedLicenseID.Name = "lblRenewedLicenseID";
            this.lblRenewedLicenseID.Size = new System.Drawing.Size(25, 13);
            this.lblRenewedLicenseID.TabIndex = 27;
            this.lblRenewedLicenseID.Text = "???";
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(133, 140);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(607, 38);
            this.tbNotes.TabIndex = 26;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(387, 117);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Total Fees";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(387, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Created By";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(387, 71);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "Expiration Date";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(387, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Old License ID";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(387, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(107, 13);
            this.label19.TabIndex = 15;
            this.label19.Text = "Renewed License ID";
            // 
            // lblLicenseFees
            // 
            this.lblLicenseFees.AutoSize = true;
            this.lblLicenseFees.Location = new System.Drawing.Point(172, 117);
            this.lblLicenseFees.Name = "lblLicenseFees";
            this.lblLicenseFees.Size = new System.Drawing.Size(25, 13);
            this.lblLicenseFees.TabIndex = 13;
            this.lblLicenseFees.Text = "???";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.Location = new System.Drawing.Point(172, 94);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(25, 13);
            this.lblAppFees.TabIndex = 12;
            this.lblAppFees.Text = "???";
            // 
            // lblissueDate
            // 
            this.lblissueDate.AutoSize = true;
            this.lblissueDate.Location = new System.Drawing.Point(172, 71);
            this.lblissueDate.Name = "lblissueDate";
            this.lblissueDate.Size = new System.Drawing.Size(25, 13);
            this.lblissueDate.TabIndex = 11;
            this.lblissueDate.Text = "???";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Location = new System.Drawing.Point(172, 48);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(25, 13);
            this.lblAppDate.TabIndex = 10;
            this.lblAppDate.Text = "???";
            // 
            // lblRenewAppID
            // 
            this.lblRenewAppID.AutoSize = true;
            this.lblRenewAppID.Location = new System.Drawing.Point(172, 25);
            this.lblRenewAppID.Name = "lblRenewAppID";
            this.lblRenewAppID.Size = new System.Drawing.Size(25, 13);
            this.lblRenewAppID.TabIndex = 9;
            this.lblRenewAppID.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Notes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "License Fees";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Application Fees";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Issue Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Application Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Renew License App ID";
            // 
            // btnRenew
            // 
            this.btnRenew.Location = new System.Drawing.Point(671, 594);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(75, 23);
            this.btnRenew.TabIndex = 3;
            this.btnRenew.Text = "Renew";
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(584, 594);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // llShowLicensesHistory
            // 
            this.llShowLicensesHistory.AutoSize = true;
            this.llShowLicensesHistory.Location = new System.Drawing.Point(41, 604);
            this.llShowLicensesHistory.Name = "llShowLicensesHistory";
            this.llShowLicensesHistory.Size = new System.Drawing.Size(114, 13);
            this.llShowLicensesHistory.TabIndex = 5;
            this.llShowLicensesHistory.TabStop = true;
            this.llShowLicensesHistory.Text = "Show Licenses History";
            this.llShowLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicensesHistory_LinkClicked);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Location = new System.Drawing.Point(173, 604);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(120, 13);
            this.llShowLicenseInfo.TabIndex = 6;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show New License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // frmRenewLocalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 623);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicensesHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.gbNewLicenseAppInfo);
            this.Controls.Add(this.ctrlApplicationInfoWithFilter1);
            this.Controls.Add(this.label1);
            this.Name = "frmRenewLocalLicense";
            this.Text = "frmRenewLocalLicense";
            this.Load += new System.EventHandler(this.frmRenewLocalLicense_Load);
            this.gbNewLicenseAppInfo.ResumeLayout(false);
            this.gbNewLicenseAppInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlApplicationInfoWithFilter ctrlApplicationInfoWithFilter1;
        private System.Windows.Forms.GroupBox gbNewLicenseAppInfo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblLicenseFees;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.Label lblissueDate;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblRenewAppID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel llShowLicensesHistory;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblExpDate;
        private System.Windows.Forms.Label lblOldLicenseID;
        private System.Windows.Forms.Label lblRenewedLicenseID;
    }
}