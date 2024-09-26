using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        clsDetainedLicense _DetainedLicense;
        clsApplication _ReleaseApplication = new clsApplication();
        int _licenseID = -1;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _licenseID = LicenseID;           
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            llShowLicenseInfo.Enabled = false;
            if (_licenseID == -1)
            {
                llShowLicensesHistory.Enabled = false;
                btnRelease.Enabled = false;
            }
            else
            {
                ctrlApplicationInfoWithFilter1.LicenseID = _licenseID;
                ctrlApplicationInfoWithFilter1.SearchForLicense();
                ctrlApplicationInfoWithFilter1.EnableFilter = false;
            }
        }

        private void ctrlApplicationInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            lblLicenseID.Text = LicenseID.ToString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.ReleaseDetainedLicense).Price.ToString();
            llShowLicensesHistory.Enabled = true;
            if(!clsDetainedLicense.IsLicenseDetained(LicenseID))
            {                
                btnRelease.Enabled = false;
                MessageBox.Show("This license is not detained", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _DetainedLicense = clsDetainedLicense.GetDetainedLicense(LicenseID);
                lblDetainID.Text = _DetainedLicense.DetainID.ToString();
                lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
                lblFineFees.Text = _DetainedLicense.FineFees.ToString();
                lblCreatedBy.Text = clsUser.Find(_DetainedLicense.CreatedBy).Username;
                lblTotalFees.Text = (Convert.ToDecimal(lblAppFees.Text) + _DetainedLicense.FineFees).ToString();
                btnRelease.Enabled = true;
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to release this license?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _ReleaseApplication.PersonID = clsDriver.FindByDriverID(ctrlApplicationInfoWithFilter1.DriverID).PersonID;
                _ReleaseApplication.Status = clsApplication.enStatus.New;
                _ReleaseApplication.LastStatusDate = DateTime.Now;
                _ReleaseApplication.Date = DateTime.Now;
                _ReleaseApplication.PaidFees = Convert.ToDecimal(lblAppFees.Text);
                _ReleaseApplication.TypeID = clsApplication.enAppType.ReleaseDetainedLicense;
                _ReleaseApplication.UserID = clsGlobleSettings.CurrentUser.UserID;

                if (_ReleaseApplication.SaveApplication())
                {
                    _DetainedLicense.ReleaseDate = DateTime.Now;
                    _DetainedLicense.ReleasedBy = clsGlobleSettings.CurrentUser.UserID;
                    _DetainedLicense.ReleaseAppID = _ReleaseApplication.ApplicationID;
                    _DetainedLicense.IsReleased = true;
                    if (_DetainedLicense.Save())
                    {
                        _ReleaseApplication.Status = clsApplication.enStatus.Completed;
                        _ReleaseApplication.SaveApplication(); // Update application status
                        lblReleaseAppID.Text = _ReleaseApplication.ApplicationID.ToString();
                        btnRelease.Enabled = false;
                        llShowLicenseInfo.Enabled = true;
                        MessageBox.Show("License Released", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Fail to release license", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Fail to save application", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void brnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm1 = new frmShowLicense(_ReleaseApplication.PersonID, _ReleaseApplication.ApplicationID);
            frm1.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(clsDriver.FindByDriverID(ctrlApplicationInfoWithFilter1.DriverID).PersonID);
            frm1.ShowDialog();
        }
    }
}
