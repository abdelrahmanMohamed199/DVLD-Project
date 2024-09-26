using DVLD.Applications;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.Users;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMainScreen : Form
    {
        frmLogin _frmlogin;
        public frmMainScreen(frmLogin frm)
        {
            InitializeComponent();
            _frmlogin = frm;
        }

        
        
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frmMangePeople1 = new frmManagePeople();
            frmMangePeople1.MdiParent = this;
            frmMangePeople1.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm1 = new frmManageUsers();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frm1 = new frmUserCard(clsGlobleSettings.CurrentUser.UserID);
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm1 = new frmChangePassword(clsGlobleSettings.CurrentUser.UserID);
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobleSettings.CurrentUser = null;
            _frmlogin.Show();
            this.Close();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAppTypesList frm = new frmAppTypesList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypesList frm = new frmTestTypesList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalApp frm1 = new frmAddNewLocalApp(1);
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalApplications frm1 = new frmLocalApplications();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers frm1 = new frmManageDrivers();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm1 = new frmIssueInternationalLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApps frm1 = new frmInternationalLicenseApps();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frm1 = new frmRenewLocalLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void replacementForLostOrDamageLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicense frm1 = new frmReplaceLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalApplications frm1 = new frmLocalApplications();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm1 = new frmDetainLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm1 = new frmReleaseDetainedLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm1 = new frmReleaseDetainedLicense();
            frm1.MdiParent = this;
            frm1.Show();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frm1 = new frmManageDetainedLicenses();
            frm1.MdiParent = this;
            frm1.Show();
        }
    }
}
