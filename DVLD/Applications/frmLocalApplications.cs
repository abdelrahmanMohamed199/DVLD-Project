using DVLD.Licenses;
using DVLD.Tests;
using DVLD_BusinessTier;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmLocalApplications : Form
    {
        DataTable dtLocalAppsList;
        clsLocalDrivingLicenseApp _LocalApp;
        public frmLocalApplications()
        {
            InitializeComponent();
        }

        private void frmLocalApplications_Load(object sender, EventArgs e)
        {
            dtLocalAppsList = clsLocalDrivingLicenseApp.GetLocalAppsList();
            dgvLocalAppsList.DataSource = dtLocalAppsList;
            lblNumOfRecords.Text = dgvLocalAppsList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Visible = false;

            if (dgvLocalAppsList.Rows.Count > 0)
            {
                dgvLocalAppsList.Columns[0].HeaderText = "L.D.L.App.ID";
                dgvLocalAppsList.Columns[0].Width = 80;

                dgvLocalAppsList.Columns[1].HeaderText = "Driving Class";
                dgvLocalAppsList.Columns[1].Width = 150;

                dgvLocalAppsList.Columns[2].HeaderText = "National No";
                dgvLocalAppsList.Columns[2].Width = 80;

                dgvLocalAppsList.Columns[3].HeaderText = "Full Name";
                dgvLocalAppsList.Columns[3].Width = 250;

                dgvLocalAppsList.Columns[4].HeaderText = "Application Date";
                dgvLocalAppsList.Columns[4].Width = 100;

                dgvLocalAppsList.Columns[4].HeaderText = "Passed Tests";
                dgvLocalAppsList.Columns[4].Width = 60;

                dgvLocalAppsList.Columns[4].HeaderText = "Status";
                dgvLocalAppsList.Columns[4].Width = 50;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "None")
            {
                tbFilterBy.Visible = false;
                dtLocalAppsList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvLocalAppsList.Rows.Count.ToString();
            }
            else
            {
                tbFilterBy.Visible = true;
                tbFilterBy.Focus();
            }
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "LDLAppID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "LDLAppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(FilterColumn == "None" || tbFilterBy.Text == "")
            {
                dtLocalAppsList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvLocalAppsList.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                dtLocalAppsList.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvLocalAppsList.Rows.Count.ToString();
            }
            else
            {
                dtLocalAppsList.DefaultView.RowFilter = string.Format("{0} like '{1}'", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvLocalAppsList.Rows.Count.ToString();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewLocalApp frm1 = new frmAddNewLocalApp((int)clsApplication.enAppType.NewLocalLicense);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _LocalApp = clsLocalDrivingLicenseApp.GetLocalAppByID((int)dgvLocalAppsList.CurrentRow.Cells[0].Value);
            int NumOfPassedTests = _LocalApp.GetNumOfPassedTests();
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            scheduleTestToolStripMenuItem.Enabled = true;

            if (_LocalApp.Status == clsApplication.enStatus.Completed)
            {
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                scheduleTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = true;
            }

            if (_LocalApp.Status == clsApplication.enStatus.Canceled)
            {
                cancelApplicationToolStripMenuItem.Enabled = false;
                scheduleTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
            }

            if (NumOfPassedTests < 3)
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
            }

            if(NumOfPassedTests == 0)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = true;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                schedulePracticalTestToolStripMenuItem.Enabled = false;
            }
            else if(NumOfPassedTests == 1)
            {
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                schedulePracticalTestToolStripMenuItem.Enabled= false;
            }
            else if (NumOfPassedTests == 2)
            {
                schedulePracticalTestToolStripMenuItem.Enabled = true;
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
            }
            else if(NumOfPassedTests == 3 && _LocalApp.Status == clsApplication.enStatus.New)
            {
                scheduleTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm1 = new frmTestAppointment(_LocalApp.LocalDrivingLicenseAppID, 1);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm1 = new frmTestAppointment(_LocalApp.LocalDrivingLicenseAppID, 2);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void schedulePracticalTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm1 = new frmTestAppointment(_LocalApp.LocalDrivingLicenseAppID, 3);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueLicense frm1 = new frmIssueLicense(_LocalApp.ApplicationID, _LocalApp.LicenseClassID);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense frm1 = new frmShowLicense(_LocalApp.PersonID, _LocalApp.ApplicationID);
            frm1.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_LocalApp.GetNumOfPassedTests() > 0)
            {
                MessageBox.Show("You can not delete this application", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(MessageBox.Show("Are you sure you want to delete this application?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)  == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApp.DeleteLocalApp(_LocalApp.LocalDrivingLicenseAppID)
                    && clsApplication.DeleteApplication(_LocalApp.ApplicationID))
                {
                    MessageBox.Show("Application deleted successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmLocalApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Fail to delete application", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_LocalApp.GetNumOfPassedTests() > 0)
            {
                MessageBox.Show("You can not cancel this application", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //clsApplication Application = _LocalApp.ApplicationInfo;
                //Application.Status = clsApplication.enStatus.Canceled;
                //Application.LastStatusDate = DateTime.Now;
                //Application.Mode = clsApplication.enMode.Update;

                _LocalApp.Status = clsApplication.enStatus.Canceled;
                _LocalApp.LastStatusDate = DateTime.Now;
                _LocalApp.Mode = clsApplication.enMode.Update;

                if (_LocalApp.SaveApplication())
                {
                    MessageBox.Show("Application was canceled", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmLocalApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Fail to cancel application", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationDetails frm1 = new frmApplicationDetails(_LocalApp.ApplicationID);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_LocalApp.PersonID);
            frm1.ShowDialog();
            frmLocalApplications_Load(null, null);
        }
    }
}
