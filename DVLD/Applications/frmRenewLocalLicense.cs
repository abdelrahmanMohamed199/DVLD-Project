using DVLD.Licenses;
using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmRenewLocalLicense : Form
    {
        clsLicense _License;
        clsApplication _OldApplication;
        clsApplication _RenewApplication = new clsApplication();
        clsLicenseClass _LicenseClass;
        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            btnRenew.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicensesHistory.Enabled = false;
        }

        private void ctrlApplicationInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _License = clsLicense.FindByID(LicenseID);
            _OldApplication = clsApplication.GetApplication(_License.ApplicationID);
            _LicenseClass = clsLicenseClass.Find(clsLicense.FindByID(LicenseID).LicenseClass);
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblissueDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.RenewLicense).Price.ToString();
            lblLicenseFees.Text = _LicenseClass.Fees.ToString();
            lblOldLicenseID.Text = LicenseID.ToString();
            lblExpDate.Text = DateTime.Now.AddYears(_LicenseClass.ValidityLength).ToShortDateString();
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;
            lblTotalFees.Text = (Convert.ToDecimal(lblAppFees.Text) + _LicenseClass.Fees).ToString();
            llShowLicensesHistory.Enabled = true;

            if(clsDriver.IsDriverHasNonExpLicense(_License.DriverID, _License.LicenseClass))
            {
                MessageBox.Show("This driver has already a non expired license from this class",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(DateTime.Compare(_License.ExpirationDate, DateTime.Now) > 0 )
            {
                MessageBox.Show($"This License is not expired, You can not renew it before {_License.ExpirationDate}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnRenew.Enabled = true;
            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Are you sure you want to renew this license?", "Confirmation",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _RenewApplication.Date = DateTime.Now;
                _RenewApplication.PersonID = _OldApplication.PersonID;
                _RenewApplication.TypeID = clsApplication.enAppType.RenewLicense;
                _RenewApplication.Status = clsApplication.enStatus.New;
                _RenewApplication.LastStatusDate = DateTime.Now;
                _RenewApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enAppType.RenewLicense).Price;
                _RenewApplication.UserID = clsGlobleSettings.CurrentUser.UserID;
                _License.IsActive = false; // Deactivate the old license

                if (_RenewApplication.SaveApplication())
                {
                    if (!_License.EditLicenseActivation())
                    {
                        return;
                    }
                    _License.ApplicationID = _RenewApplication.ApplicationID;
                    _License.IssueDate = DateTime.Now;
                    _License.ExpirationDate = DateTime.Now.AddYears(_LicenseClass.ValidityLength);
                    _License.Notes = tbNotes.Text.Trim();
                    _License.PaidFees = _LicenseClass.Fees;
                    _License.IsActive = true;
                    _License.IssueReason = clsLicense.enIssueReason.Renew;
                    _License.UserID = clsGlobleSettings.CurrentUser.UserID;

                    if (_License.IssueLicense())
                    {
                        lblRenewAppID.Text = _License.ApplicationID.ToString();
                        lblRenewedLicenseID.Text = _License.LicenseID.ToString();
                        MessageBox.Show("Data Saved Successfully", "License Renewed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRenew.Enabled = false;
                        llShowLicenseInfo.Enabled = true;
                        _RenewApplication.Status = clsApplication.enStatus.Completed;
                        _RenewApplication.SaveApplication();
                    }
                    else
                    {
                        MessageBox.Show("Fail to renew license", "Fail",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fail to renew license", "Fail",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_OldApplication.PersonID);
            frm1.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm1 = new frmShowLicense(_OldApplication.PersonID, _RenewApplication.ApplicationID);
            frm1.ShowDialog();
        }
    }
}
