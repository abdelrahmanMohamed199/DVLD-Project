using DVLD.Properties;
using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlApplicationInfoWithFilter : UserControl
    {
        clsLicense _License;
        public int LicenseID
        {
            set
            {
                tbFilterByID.Text = value.ToString();               
            }
        }
        public int DriverID
        {
            get { return _License.DriverID; }
        }

        bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set 
            { 
                _EnableFilter = value;
                gbFilter.Enabled = _EnableFilter;
            }
        }

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
                handler(LicenseID);
        }
        public ctrlApplicationInfoWithFilter()
        {
            InitializeComponent();
        }

        public void SearchForLicense()
        {
            if (tbFilterByID.Text != "")
            {
                _License = clsLicense.FindByID(Convert.ToInt32(tbFilterByID.Text));
                if (_License != null)
                {
                    clsDriver Driver = clsDriver.FindByDriverID(_License.DriverID);
                    lblClass.Text = clsLicenseClass.Find(_License.LicenseClass).ClassName;
                    lblName.Text = Driver.PersonInfo.FullName;
                    lblLicenseID.Text = _License.LicenseID.ToString();
                    lblNationalNo.Text = Driver.PersonInfo.NationalNo;
                    lblIssueDate.Text = _License.IssueDate.ToShortDateString();
                    lblExpDate.Text = _License.ExpirationDate.ToShortDateString();
                    lblDateOfBirth.Text = Driver.PersonInfo.DateOfBirth.ToShortDateString();
                    lblIssueReason.Text = _License.IssueReason.ToString();
                    lblDriverID.Text = Driver.DriverID.ToString();
                    if (Driver.PersonInfo.ImagePath != "")
                        pbPersonPic.ImageLocation = Driver.PersonInfo.ImagePath;
                    else
                    {
                        if (Driver.PersonInfo.Gendor == 0)
                        {
                            pbPersonPic.Image = Resources.person_boy;
                        }
                        else
                        {
                            pbPersonPic.Image = Resources.person_girl;
                        }
                    }

                    if(Driver.PersonInfo.Gendor == 0)
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

                    if (clsDetainedLicense.IsLicenseDetained(_License.LicenseID))
                        lblisDetained.Text = "Yes";
                    else
                        lblisDetained.Text = "No";

                    if (OnLicenseSelected != null)
                        LicenseSelected(_License.LicenseID);
                }
                else
                {
                    MessageBox.Show("License does not exist", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbFilterByID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForLicense();
        }
    }
}
