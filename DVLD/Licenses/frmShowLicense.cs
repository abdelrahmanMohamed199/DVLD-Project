using DVLD.Properties;
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
    public partial class frmShowLicense : Form
    {
        clsDriver _Driver;
        clsLicense _License;
        public frmShowLicense(int PersonID, int AppID)
        {
            InitializeComponent();
            _Driver = clsDriver.FindByPersonID(PersonID);
            _License = clsLicense.FindByAppID(AppID);
        }

        public frmShowLicense(int PersonID, clsLicense License)
        {
            InitializeComponent();
            _Driver = clsDriver.FindByPersonID(PersonID);
            _License = License;
        }

        private void frmShowLicense_Load(object sender, EventArgs e)
        {
            lblClass.Text = clsLicenseClass.Find(_License.LicenseClass).ClassName;
            lblDateOfBirth.Text = _Driver.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _Driver.DriverID.ToString();
            lblExpDate.Text = _License.ExpirationDate.ToShortDateString();
            lblName.Text = _Driver.PersonInfo.FullName;
            lblNationalNo.Text = _Driver.PersonInfo.NationalNo;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            if (_Driver.PersonInfo.ImagePath != "")
                pbPersonPic.ImageLocation = _Driver.PersonInfo.ImagePath;
            else
            {
                if (_Driver.PersonInfo.Gendor == 0)
                {
                    pbPersonPic.Image = Resources.person_boy;
                }
                else
                {
                    pbPersonPic.Image = Resources.person_girl;
                }
            }

            if(_Driver.PersonInfo.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            if (_License.Notes != "")
                lblNotes.Text = _License.Notes;
            else
                lblNotes.Text = "No Notes";

            if (_License.IsActive)
                lblisActive.Text = "Yes";
            else
                lblisActive.Text = "No";

            if(clsDetainedLicense.IsLicenseDetained(_License.LicenseID))
                lblisDetained.Text = "Yes";
            else
                lblisDetained.Text = "No";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
