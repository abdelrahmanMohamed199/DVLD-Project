using DVLD.Licenses;
using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmReplaceLicense : Form
    {
        clsLicense _License;
        clsApplication _Application;
        public frmReplaceLicense()
        {
            InitializeComponent();
        }

        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            rbDamagedLicense.Checked = true;
            lblTitle.Text = "Replacement For Damaged License";
        }

        private void ctrlApplicationInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _License = clsLicense.FindByID(LicenseID);
            _Application = clsApplication.GetApplication(_License.ApplicationID);
            llShowLicenseHistory.Enabled = true;
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblOldLicenseID.Text = LicenseID.ToString();
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;

            if(!_License.IsActive)
            {
                MessageBox.Show("This license is not active, You can only replace an active license",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Are you want to replace license?", "Confirmation",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _Application.Mode = clsApplication.enMode.AddNew;
                _Application.Date = DateTime.Now;
                _Application.Status = clsApplication.enStatus.New;
                _Application.LastStatusDate = DateTime.Now;
                _Application.UserID = clsGlobleSettings.CurrentUser.UserID;
                if (rbDamagedLicense.Checked)
                {
                    _Application.TypeID = clsApplication.enAppType.ReplacementForDamagedLicense;
                    _Application.PaidFees = clsApplicationType.Find((int)clsApplication.enAppType.ReplacementForDamagedLicense).Price;
                }
                else
                {
                    _Application.TypeID = clsApplication.enAppType.ReplacementForLostLicense;
                    _Application.PaidFees = clsApplicationType.Find((int)clsApplication.enAppType.ReplacementForLostLicense).Price;
                }

                _License.IsActive = false; // make the old license deactivated
                if (_Application.SaveApplication())
                {
                    if(!_License.EditLicenseActivation())
                    {
                        return;
                    }
                    _License.ApplicationID = _Application.ApplicationID;
                    _License.IsActive = true; // Active the new license
                    _License.PaidFees = 0; // Driver just paid for replacement
                    _License.Notes = "";
                    _License.UserID = clsGlobleSettings.CurrentUser.UserID;
                    if (rbDamagedLicense.Checked)
                        _License.IssueReason = clsLicense.enIssueReason.ReplaceForDamage;
                    else
                        _License.IssueReason = clsLicense.enIssueReason.ReplaceForLost;

                    if (_License.IssueLicense())
                    {
                        lblReplacedLicenseID.Text = _License.LicenseID.ToString();
                        lblReplacementAppID.Text = _License.ApplicationID.ToString();
                        llShowLicenseInfo.Enabled = true;
                        btnSave.Enabled = false;
                        _Application.Status = clsApplication.enStatus.Completed;
                        _Application.SaveApplication();
                        MessageBox.Show("Data Saved Successfully", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail to save data", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fail to save data, there is something wrong with application", "Error",
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
            frmShowLicense frm1 = new frmShowLicense(_Application.PersonID, _Application.ApplicationID);
            frm1.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_Application.PersonID);
            frm1.ShowDialog();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDamagedLicense.Checked)
            {
                lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.ReplacementForDamagedLicense).Price.ToString();
                lblTitle.Text = "Replacement For Damaged License";
            }
            else
            {
                lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.ReplacementForLostLicense).Price.ToString();
                lblTitle.Text = "Replacement For Lost License";
            }
        }
    }
}
