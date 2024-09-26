namespace DVLD.Licenses
{
    partial class frmShowDriverLicensesHistory
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
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlPersonLicenseHistory1 = new DVLD.Licenses.ctrlPersonLicenseHistory();
            this.ctrlPersonInfoWithFilter1 = new DVLD.People.Controls.ctrlPersonInfoWithFilter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(275, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "License History";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(627, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlPersonLicenseHistory1
            // 
            this.ctrlPersonLicenseHistory1.AutoSize = true;
            this.ctrlPersonLicenseHistory1.Location = new System.Drawing.Point(21, 355);
            this.ctrlPersonLicenseHistory1.Name = "ctrlPersonLicenseHistory1";
            this.ctrlPersonLicenseHistory1.Size = new System.Drawing.Size(501, 204);
            this.ctrlPersonLicenseHistory1.TabIndex = 2;
            // 
            // ctrlPersonInfoWithFilter1
            // 
            this.ctrlPersonInfoWithFilter1.FilterEnable = true;
            this.ctrlPersonInfoWithFilter1.Location = new System.Drawing.Point(1, 39);
            this.ctrlPersonInfoWithFilter1.Name = "ctrlPersonInfoWithFilter1";
            this.ctrlPersonInfoWithFilter1.ShowAdd = true;
            this.ctrlPersonInfoWithFilter1.Size = new System.Drawing.Size(701, 310);
            this.ctrlPersonInfoWithFilter1.TabIndex = 1;
            // 
            // frmShowDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 594);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlPersonLicenseHistory1);
            this.Controls.Add(this.ctrlPersonInfoWithFilter1);
            this.Controls.Add(this.label1);
            this.Name = "frmShowDriverLicensesHistory";
            this.Text = "frmShowDriverLicensesHistory";
            this.Load += new System.EventHandler(this.frmShowDriverLicensesHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private People.Controls.ctrlPersonInfoWithFilter ctrlPersonInfoWithFilter1;
        private ctrlPersonLicenseHistory ctrlPersonLicenseHistory1;
        private System.Windows.Forms.Button button1;
    }
}