using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmScheduletest : Form
    {
        clsLocalDrivingLicenseApp _LocalApp;
        clsTestAppointment _TestAppointment;
        int _TestTypeID;
        int _Trial;
        public frmScheduletest(int AppointmentID, int DLAppID, int TestTypeID, int Trial)
        {
            InitializeComponent();
            _LocalApp = clsLocalDrivingLicenseApp.GetLocalAppByID(DLAppID);
            _TestTypeID = TestTypeID;
            _Trial = Trial;
            if(AppointmentID == -1)
                _TestAppointment = new clsTestAppointment();
            else
                _TestAppointment = clsTestAppointment.Find(AppointmentID);
        }

        private void frmScheduletest_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Schedule Test";
            lblDLAppID.Text = _LocalApp.LocalDrivingLicenseAppID.ToString();
            lblClass.Text = clsLicenseClass.Find(_LocalApp.LicenseClassID).ClassName;
            lblName.Text = _LocalApp.PersonInfo.FullName;
            dtpDate.Value = _TestAppointment.AppointmentDate;
            dtpDate.MinDate = DateTime.Now;
            lblFees.Text = clsTestType.Find(_TestTypeID).Price.ToString();
            lblTrial.Text = _Trial.ToString();
            gbRetakeTest.Enabled = false;
            btnSave.Enabled = true;

            if(_Trial > 0)
            {
                gbRetakeTest.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestFees.Text = clsApplicationType.Find((int)clsApplication.enAppType.RetakeTest).Price.ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();
                lblRetakeTestAppID.Text = "N/A";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gbRetakeTest.Enabled)
            {
                clsApplication RetakeTestApp = new clsApplication();
                RetakeTestApp.PersonID = _LocalApp.PersonID;
                RetakeTestApp.Date = DateTime.Now;
                RetakeTestApp.TypeID = clsApplication.enAppType.RetakeTest;
                RetakeTestApp.Status = clsApplication.enStatus.New;
                RetakeTestApp.LastStatusDate = DateTime.Now;
                RetakeTestApp.PaidFees = clsApplicationType.Find((int)clsApplication.enAppType.RetakeTest).Price;
                RetakeTestApp.UserID = clsGlobleSettings.CurrentUser.UserID;
                if(RetakeTestApp.SaveApplication())
                {
                    _TestAppointment.TestTypeID = (clsTestAppointment.enTestType)_TestTypeID;
                    _TestAppointment.LocalDrivingLicenseAppID = _LocalApp.LocalDrivingLicenseAppID;
                    _TestAppointment.AppointmentDate = dtpDate.Value;
                    _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
                    _TestAppointment.UserID = clsGlobleSettings.CurrentUser.UserID;
                    _TestAppointment.IsLocked = false;
                    _TestAppointment.RetakeTestAppID = RetakeTestApp.ApplicationID;
                    if (_TestAppointment.Save())
                    {
                        MessageBox.Show("Data saved successfully", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblRetakeTestAppID.Text = _TestAppointment.RetakeTestAppID.ToString();
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Fail to add appointment", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                _TestAppointment.TestTypeID = (clsTestAppointment.enTestType)_TestTypeID;
                _TestAppointment.LocalDrivingLicenseAppID = _LocalApp.LocalDrivingLicenseAppID;
                _TestAppointment.AppointmentDate = dtpDate.Value;
                _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
                _TestAppointment.UserID = clsGlobleSettings.CurrentUser.UserID;
                _TestAppointment.IsLocked = false;
                _TestAppointment.RetakeTestAppID = 0; // it will save null in database
                if (_TestAppointment.Save())
                {
                    MessageBox.Show("Data saved successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Fail to add appointment", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }          
        }
    }
}
