using DVLD.Licenses;
using DVLD_BusinessTier;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlApplicationInfo : UserControl
    {
        clsApplication _Application;

        bool _EnableShowLicenseInfo = false;
        public bool EnableShowLicenseInfo
        {
            get { return _EnableShowLicenseInfo; }
            set
            {
                llShowLicenseInfo.Enabled = value;
                _EnableShowLicenseInfo = llShowLicenseInfo.Enabled;
            }         
        }
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadData(int AppID)
        {
            _Application = clsApplication.GetApplication(AppID);

            if (_Application == null)
            {
                MessageBox.Show("There is no application to show info", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                FindForm().Close();
                return;
            }

            switch(_Application.TypeID)
            {
                case clsApplication.enAppType.NewLocalLicense:
                    clsLocalDrivingLicenseApp _LocalApp = clsLocalDrivingLicenseApp.GetLocalAppByAppID(AppID);
                    lblDLAppID.Text = _LocalApp.LocalDrivingLicenseAppID.ToString();
                    lblClassName.Text = clsLicenseClass.Find(_LocalApp.LicenseClassID).ClassName;
                    lblPassedTests.Text = _LocalApp.GetNumOfPassedTests().ToString() + "/3";
                    break;

                default:
                    break;
            }
            
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.Status.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblDate.Text = _Application.Date.ToShortDateString();
            lblStatusDate.Text = _Application.LastStatusDate.ToShortDateString();
            lblAppType.Text = clsApplicationType.Find((int)_Application.TypeID).AppTitle;
            lblApplicant.Text = _Application.PersonInfo.FullName;
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;
            llShowLicenseInfo.Enabled = false;
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm1 = new frmShowPersonInfo(_Application.PersonID);
            frm1.ShowDialog();
            LoadData(_Application.ApplicationID); // Refresh
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm1 = new frmShowLicense(_Application.PersonID, _Application.ApplicationID);
            frm1.ShowDialog();
        }
    }
}
