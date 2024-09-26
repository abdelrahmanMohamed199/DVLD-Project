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

namespace DVLD.Tests
{
    public partial class frmTestAppointment : Form
    {
        clsLocalDrivingLicenseApp _LocalApp;
        int _LocalAppID;
        int _TestTypeID;
        int _Trials = 0;
        public frmTestAppointment(int LocalAppID, int TestTypeID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            _TestTypeID = TestTypeID;
            _LocalApp = clsLocalDrivingLicenseApp.GetLocalAppByID(LocalAppID);
        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.LoadData(_LocalApp.ApplicationID);
            DataTable dtPersonAppointmentsList = clsTestAppointment.GetAppointmentsList(_LocalAppID, _TestTypeID);
            dgvAppointmentsList.DataSource = dtPersonAppointmentsList;
            lblNumOfRecords.Text = dgvAppointmentsList.Rows.Count.ToString();
            _Trials = dgvAppointmentsList.Rows.Count;
            switch (_TestTypeID)
            {
                case 1:
                    lblTitle.Text = "Vision Test Appointment";
                    break;
                case 2:
                    lblTitle.Text = "Written Test Appointment";
                    break;
                case 3:
                    lblTitle.Text = "Practical Test Appointment";
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(clsTestAppointment.IsTestAppointmentExist(_LocalApp.PersonID, _TestTypeID, _LocalApp.LicenseClassID))
            {
                MessageBox.Show("There is already an active appointment", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(clsTestAppointment.IsPersonPassedTest(_LocalApp.PersonID, _TestTypeID))
            {
                MessageBox.Show("This person has passed this test", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduletest frm1 = new frmScheduletest(-1, _LocalAppID, _TestTypeID, _Trials);
            frm1.ShowDialog();
            frmTestAppointment_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.Find((int)dgvAppointmentsList.CurrentRow.Cells[0].Value).IsLocked)
            {
                MessageBox.Show("You can not edit this appointment", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduletest frm1 = new frmScheduletest((int)dgvAppointmentsList.CurrentRow.Cells[0].Value, 
                _LocalAppID, _TestTypeID, _Trials);
            frm1.ShowDialog();
            frmTestAppointment_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvAppointmentsList.CurrentRow.Cells[3].Value)
            {
                MessageBox.Show("This person already taken the test", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int testAppointmentID = (int)dgvAppointmentsList.CurrentRow.Cells[0].Value;
            frmTakeTest frm1 = new frmTakeTest(_LocalAppID, _Trials, testAppointmentID);
            frm1.ShowDialog();
            frmTestAppointment_Load(null, null);
        }
    }
}
