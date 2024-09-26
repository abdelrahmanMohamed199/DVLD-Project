using DVLD.Properties;
using DVLD_BusinessTier;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrlInterLicenseInfo : UserControl
    {
        public ctrlInterLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadData(int InternationalLicenseID)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.GetInternationalLicense(InternationalLicenseID);
            if (InternationalLicense != null)
            {
                lblName.Text = InternationalLicense.PersonInfo.FullName;
                lblintLicenseID.Text = InternationalLicense.InterLicenseID.ToString();
                lblLicenseID.Text = InternationalLicense.LocalLicenseID.ToString();
                lblNationalNo.Text = InternationalLicense.PersonInfo.NationalNo;
                lblissueDate.Text = InternationalLicense.IssueDate.ToShortDateString();
                lblAppID.Text = InternationalLicense.ApplicationID.ToString();
                lblDateOfBirth.Text = InternationalLicense.PersonInfo.DateOfBirth.ToShortDateString();
                lblExpDate.Text = InternationalLicense.ExpirationDate.ToShortDateString();
                lblDriverID.Text = InternationalLicense.DriverID.ToString();

                if (InternationalLicense.PersonInfo.ImagePath != "")
                { 
                    pbPersonPic.ImageLocation = InternationalLicense.PersonInfo.ImagePath; 
                }
                else
                {
                    if(InternationalLicense.PersonInfo.Gendor == 0)
                    {
                        lblGendor.Text = "Male";
                        pbPersonPic.Image = Resources.person_boy;
                    }
                    else
                    {
                        lblGendor.Text = "Female";
                        pbPersonPic.Image = Resources.person_girl;
                    }
                }

                if (InternationalLicense.IsActive)
                    lblisActive.Text = "Yes";
                else
                    lblisActive.Text = "No";
            }
        }
    }
}
