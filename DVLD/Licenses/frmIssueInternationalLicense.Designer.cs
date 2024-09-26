namespace DVLD.Licenses
{
    partial class frmIssueInternationalLicense
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
            this.gbAppInfo = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblExpDate = new System.Windows.Forms.Label();
            this.lblLocalLicenseID = new System.Windows.Forms.Label();
            this.lblinterLicenseID = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblFees = new System.Windows.Forms.Label();
            this.lblissueDate = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblinterAppID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.gbAppInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(218, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "International License Application";
            // 
            // ctrlApplicationInfoWithFilter1
            // 
            this.ctrlApplicationInfoWithFilter1.Location = new System.Drawing.Point(-2, 55);
            this.ctrlApplicationInfoWithFilter1.Name = "ctrlApplicationInfoWithFilter1";
            this.ctrlApplicationInfoWithFilter1.Size = new System.Drawing.Size(806, 374);
            this.ctrlApplicationInfoWithFilter1.TabIndex = 1;
            this.ctrlApplicationInfoWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlApplicationInfoWithFilter1_OnLicenseSelected);
            // 
            // gbAppInfo
            // 
            this.gbAppInfo.Controls.Add(this.lblCreatedBy);
            this.gbAppInfo.Controls.Add(this.lblExpDate);
            this.gbAppInfo.Controls.Add(this.lblLocalLicenseID);
            this.gbAppInfo.Controls.Add(this.lblinterLicenseID);
            this.gbAppInfo.Controls.Add(this.label10);
            this.gbAppInfo.Controls.Add(this.label11);
            this.gbAppInfo.Controls.Add(this.label12);
            this.gbAppInfo.Controls.Add(this.label13);
            this.gbAppInfo.Controls.Add(this.lblFees);
            this.gbAppInfo.Controls.Add(this.lblissueDate);
            this.gbAppInfo.Controls.Add(this.lblAppDate);
            this.gbAppInfo.Controls.Add(this.lblinterAppID);
            this.gbAppInfo.Controls.Add(this.label5);
            this.gbAppInfo.Controls.Add(this.label4);
            this.gbAppInfo.Controls.Add(this.label3);
            this.gbAppInfo.Controls.Add(this.label2);
            this.gbAppInfo.Location = new System.Drawing.Point(12, 435);
            this.gbAppInfo.Name = "gbAppInfo";
            this.gbAppInfo.Size = new System.Drawing.Size(785, 113);
            this.gbAppInfo.TabIndex = 2;
            this.gbAppInfo.TabStop = false;
            this.gbAppInfo.Text = "Application Info";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(541, 91);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(25, 13);
            this.lblCreatedBy.TabIndex = 15;
            this.lblCreatedBy.Text = "???";
            // 
            // lblExpDate
            // 
            this.lblExpDate.AutoSize = true;
            this.lblExpDate.Location = new System.Drawing.Point(541, 66);
            this.lblExpDate.Name = "lblExpDate";
            this.lblExpDate.Size = new System.Drawing.Size(25, 13);
            this.lblExpDate.TabIndex = 14;
            this.lblExpDate.Text = "???";
            // 
            // lblLocalLicenseID
            // 
            this.lblLocalLicenseID.AutoSize = true;
            this.lblLocalLicenseID.Location = new System.Drawing.Point(541, 41);
            this.lblLocalLicenseID.Name = "lblLocalLicenseID";
            this.lblLocalLicenseID.Size = new System.Drawing.Size(25, 13);
            this.lblLocalLicenseID.TabIndex = 13;
            this.lblLocalLicenseID.Text = "???";
            // 
            // lblinterLicenseID
            // 
            this.lblinterLicenseID.AutoSize = true;
            this.lblinterLicenseID.Location = new System.Drawing.Point(541, 16);
            this.lblinterLicenseID.Name = "lblinterLicenseID";
            this.lblinterLicenseID.Size = new System.Drawing.Size(25, 13);
            this.lblinterLicenseID.TabIndex = 12;
            this.lblinterLicenseID.Text = "???";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(435, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Created By";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(435, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Expiration Date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(435, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Local License ID";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(435, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "I.License ID";
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Location = new System.Drawing.Point(172, 91);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(25, 13);
            this.lblFees.TabIndex = 7;
            this.lblFees.Text = "???";
            // 
            // lblissueDate
            // 
            this.lblissueDate.AutoSize = true;
            this.lblissueDate.Location = new System.Drawing.Point(172, 66);
            this.lblissueDate.Name = "lblissueDate";
            this.lblissueDate.Size = new System.Drawing.Size(25, 13);
            this.lblissueDate.TabIndex = 6;
            this.lblissueDate.Text = "???";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Location = new System.Drawing.Point(172, 41);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(25, 13);
            this.lblAppDate.TabIndex = 5;
            this.lblAppDate.Text = "???";
            // 
            // lblinterAppID
            // 
            this.lblinterAppID.AutoSize = true;
            this.lblinterAppID.Location = new System.Drawing.Point(172, 16);
            this.lblinterAppID.Name = "lblinterAppID";
            this.lblinterAppID.Size = new System.Drawing.Size(25, 13);
            this.lblinterAppID.TabIndex = 4;
            this.lblinterAppID.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Fees";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Issue Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Application Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "I.L.App.ID";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(533, 562);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.Location = new System.Drawing.Point(636, 562);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(75, 23);
            this.btnIssue.TabIndex = 4;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Location = new System.Drawing.Point(28, 567);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(109, 13);
            this.llShowLicenseHistory.TabIndex = 5;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Location = new System.Drawing.Point(160, 567);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(95, 13);
            this.llShowLicenseInfo.TabIndex = 6;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // frmIssueInternationalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 592);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbAppInfo);
            this.Controls.Add(this.ctrlApplicationInfoWithFilter1);
            this.Controls.Add(this.label1);
            this.Name = "frmIssueInternationalLicense";
            this.Text = "frmIssueInternationalLicense";
            this.Load += new System.EventHandler(this.frmIssueInternationalLicense_Load);
            this.gbAppInfo.ResumeLayout(false);
            this.gbAppInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Applications.ctrlApplicationInfoWithFilter ctrlApplicationInfoWithFilter1;
        private System.Windows.Forms.GroupBox gbAppInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblExpDate;
        private System.Windows.Forms.Label lblLocalLicenseID;
        private System.Windows.Forms.Label lblinterLicenseID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label lblissueDate;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblinterAppID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
    }
}