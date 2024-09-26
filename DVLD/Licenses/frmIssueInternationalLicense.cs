using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmIssueInternationalLicense : Form
    {
        clsDriver _Driver;
        clsInternationalLicense _InternationalLicense;
        int _UsedLocalLicenseID;
        public frmIssueInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmIssueInternationalLicense_Load(object sender, EventArgs e)
        {
            btnIssue.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblExpDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.NewInternationalLicense).Price.ToString();
            lblissueDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlApplicationInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _UsedLocalLicenseID = LicenseID;
            _Driver = clsDriver.FindByDriverID(ctrlApplicationInfoWithFilter1.DriverID);
            llShowLicenseHistory.Enabled = true;
            lblLocalLicenseID.Text = LicenseID.ToString();
            if(clsInternationalLicense.IsDriverHasInterLicense(ctrlApplicationInfoWithFilter1.DriverID))
            {
                _InternationalLicense = clsInternationalLicense.GetInterlLicenseByDriverID(_Driver.DriverID);
                lblinterLicenseID.Text = _InternationalLicense.InterLicenseID.ToString();
                lblinterAppID.Text = _InternationalLicense.ApplicationID.ToString();
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("This driver has already an active international license", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                llShowLicenseInfo.Enabled = false;
                btnIssue.Enabled = true;
                lblinterAppID.Text = "???";
                lblinterLicenseID.Text = "???";
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_Driver.PersonID);
            frm1.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _InternationalLicense = new clsInternationalLicense();

            if(!clsDriver.IsDriverHasNonExpLicense(_Driver.DriverID, 3))
            {
                MessageBox.Show("You must have non expired license from class 3", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(MessageBox.Show("Are you sure you want to issue an international license for this driver?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _InternationalLicense.PersonID = _Driver.PersonID;
                _InternationalLicense.Date = DateTime.Now;
                _InternationalLicense.TypeID = clsApplication.enAppType.NewInternationalLicense;
                _InternationalLicense.Status = clsApplication.enStatus.New;
                _InternationalLicense.LastStatusDate = DateTime.Now;
                _InternationalLicense.PaidFees = (clsApplicationType.Find((int)clsApplication.enAppType.NewInternationalLicense)).Price;
                _InternationalLicense.UserID = clsGlobleSettings.CurrentUser.UserID;
                _InternationalLicense.DriverID = _Driver.DriverID;
                _InternationalLicense.LocalLicenseID = _UsedLocalLicenseID;
                _InternationalLicense.IssueDate = DateTime.Now;
                _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
                _InternationalLicense.IsActive = true;
                    
                if(_InternationalLicense.IssueLicense())
                {
                    MessageBox.Show("License issued successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llShowLicenseInfo.Enabled = true;
                    _InternationalLicense.Status = clsApplication.enStatus.Completed;
                    _InternationalLicense.Mode = clsApplication.enMode.Update;
                    _InternationalLicense.SaveApplication();
                    lblinterLicenseID.Text = _InternationalLicense.InterLicenseID.ToString();
                    lblinterAppID.Text = _InternationalLicense.ApplicationID.ToString();
                }
                else
                {
                    MessageBox.Show("Fail to issue License", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frm1 = new frmInternationalLicenseInfo(_InternationalLicense.InterLicenseID);
            frm1.ShowDialog();
        }
    }
}
