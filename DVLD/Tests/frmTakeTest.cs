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
    public partial class frmTakeTest : Form
    {
        clsLocalDrivingLicenseApp _LocalApp;
        int _Trials;
        clsTestAppointment _TestAppointment;
        public frmTakeTest(int LocalAppID, int Trials, int TestAppointmentID)
        {
            InitializeComponent();
            _LocalApp = clsLocalDrivingLicenseApp.GetLocalAppByID(LocalAppID);
            _Trials = Trials;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            lblLDLAppID.Text = _LocalApp.LocalDrivingLicenseAppID.ToString();
            lblClass.Text = clsApplicationType.Find((int)_LocalApp.TypeID).AppTitle;
            lblName.Text = _LocalApp.PersonInfo.FullName;
            lblTrial.Text = _Trials.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblTestID.Text = "New Taken Test";
            lblFees.Text = clsTestType.Find((int)_TestAppointment.TestTypeID).Price.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to save test result?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsTest Test = new clsTest();
                Test.TestAppointmentID = _TestAppointment.TestAppointmentID;
                Test.UserID = clsGlobleSettings.CurrentUser.UserID;
                Test.TestResult = rbPass.Checked;
                Test.Notes = tbNotes.Text;
                _TestAppointment.IsLocked = true;

                if (Test.AddTestRecord() && _TestAppointment.Save())
                {
                    
                    if (MessageBox.Show("Data saved successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        this.Close();
                }
                else
                    MessageBox.Show("Fail to save data", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
