using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmIssueLicense : Form
    {
        clsLicense _License = new clsLicense();
        clsApplication _App;
        byte _ClassID;
        public frmIssueLicense(int AppID, byte ClassID)
        {
            InitializeComponent();
            ctrlApplicationInfo1.LoadData(AppID);
            _App = clsApplication.GetApplication(AppID);
            _ClassID = ClassID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to issue license?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsDriver Driver = clsDriver.FindByPersonID(_App.PersonID);
                if (Driver == null)
                {
                    Driver = new clsDriver();
                    Driver.PersonID = _App.PersonID;
                    Driver.UserID = clsGlobleSettings.CurrentUser.UserID;
                    Driver.CreatedDate = DateTime.Now;
                    if (!Driver.AddDriver())
                    {
                        MessageBox.Show("Fail to issue license there is something wrong", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                clsLicenseClass LicenseClass = clsLicenseClass.Find(_ClassID);
                _License.ApplicationID = _App.ApplicationID;
                _License.DriverID = Driver.DriverID;
                _License.LicenseClass = _ClassID;
                _License.IssueDate = DateTime.Now;
                _License.ExpirationDate = DateTime.Now.AddYears(LicenseClass.ValidityLength);
                _License.Notes = tbNotes.Text.Trim();
                _License.PaidFees = LicenseClass.Fees;
                _License.IsActive = true;
                _License.IssueReason = clsLicense.enIssueReason.FirstTime;
                _License.UserID = clsGlobleSettings.CurrentUser.UserID;

                if (_License.IssueLicense())
                {
                    _App.Status = clsApplication.enStatus.Completed;
                    _App.LastStatusDate = DateTime.Now;
                    _App.SaveApplication();
                    MessageBox.Show("License issued successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    ctrlApplicationInfo1.EnableShowLicenseInfo = true;
                }
                else
                    MessageBox.Show("Fail to issue license", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
