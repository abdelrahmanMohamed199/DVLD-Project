using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmDetainLicense : Form
    {
        clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
        clsLicense _License;
        clsDriver _Driver;
        public frmDetainLicense() 
        {
            InitializeComponent();
        }

        private void ctrlApplicationInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _License = clsLicense.FindByID(LicenseID);
            _Driver = clsDriver.FindByDriverID(_License.DriverID);
            lblLicenseID.Text = LicenseID.ToString();
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;
            llShowLicensesHistory.Enabled = true;

            if(clsDetainedLicense.IsLicenseDetained(LicenseID))
            {
                MessageBox.Show("This license is already detained", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFineFees.Enabled = false;
            }
            else if(!_License.IsActive)
            {
                MessageBox.Show("Your license is not active", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFineFees.Enabled = false;
            }
            else
            {
                btnDetain.Enabled = true;
                tbFineFees.Enabled = true;
                tbFineFees.Focus();
            }
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            llShowLicensesHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnDetain.Enabled = false;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(tbFineFees.Text == "")
            {
                MessageBox.Show("Enter fine fees", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(MessageBox.Show("Do you want to detain this license?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _DetainedLicense.LicenseID = _License.LicenseID;
                _DetainedLicense.DetainDate = DateTime.Now;
                _DetainedLicense.CreatedBy = clsGlobleSettings.CurrentUser.UserID;
                _DetainedLicense.IsReleased = false;
                _DetainedLicense.FineFees = Convert.ToDecimal(tbFineFees.Text);

                if(_DetainedLicense.Save())
                {
                    lblDetainID.Text = _DetainedLicense.DetainID.ToString();
                    llShowLicenseInfo.Enabled = true;
                    btnDetain.Enabled = false;
                    MessageBox.Show("Data saved successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fail to detain license", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm1 = new frmShowLicense(_Driver.PersonID, _License.ApplicationID);
            frm1.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_Driver.PersonID);
            frm1.ShowDialog();
        }
    }
}
