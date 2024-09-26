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

namespace DVLD.Applications
{
    public partial class frmAddNewLocalApp : Form
    {
        DataTable dtlicenseClassesList = clsLicenseClass.GetClassesList();
        clsPerson _Person;
        //clsApplication _AppRecord = new clsApplication();
        clsLocalDrivingLicenseApp _LocalApp = new clsLocalDrivingLicenseApp();
        int _AppTypeID = 0;
        public frmAddNewLocalApp(int appTypeID)
        {
            InitializeComponent();
            _AppTypeID = appTypeID;
        }

        private void FillCBLicenseClasses()
        {
            foreach (DataRow row in dtlicenseClassesList.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void frmAddNewApp_Load(object sender, EventArgs e)
        {
            FillCBLicenseClasses();
            cbLicenseClass.SelectedIndex = 0;
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = clsApplicationType.Find(_AppTypeID).Price.ToString();
            lblCreatedBy.Text = clsGlobleSettings.CurrentUser.Username;
            tabAppInfo.Enabled = false;
            btnSave.Enabled = false;
        }

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(int PersonID)
        {
            if(ctrlPersonInfoWithFilter1.PersonID != -1)
            {
                _Person = clsPerson.Find(PersonID);
                tabAppInfo.Enabled =true;
                btnSave.Enabled = true;
                lblAppID.Text = "???";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow[] rows = dtlicenseClassesList.Select($"ClassName = '{cbLicenseClass.Text}'");
            byte LicenseClassID = Convert.ToByte(rows[0][0]);

            if (clsLocalDrivingLicenseApp.IsPersonHasLicenseOrActiveApp(_Person.ID, LicenseClassID))
            {
                MessageBox.Show("This person already has an active application or has license from the same class",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(ctrlPersonInfoWithFilter1.PersonID == -1)
            {
                MessageBox.Show("There is no person, choose person first",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte MinimumAllowedAge = clsLicenseClass.Find(LicenseClassID).MinAge;
            byte PersonAge = (byte)(DateTime.Now.Subtract(_Person.DateOfBirth).TotalDays / 365.25);
            if(PersonAge < MinimumAllowedAge)
            {
                MessageBox.Show($"Your age is under the minimum allowed age for this license class, You must be at least {MinimumAllowedAge} years old",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalApp.PersonID = _Person.ID;
            _LocalApp.Date = DateTime.Now;
            _LocalApp.LastStatusDate = DateTime.Now;
            _LocalApp.Status = clsApplication.enStatus.New;
            _LocalApp.UserID = clsGlobleSettings.CurrentUser.UserID;
            _LocalApp.PaidFees = Convert.ToDecimal(lblAppFees.Text);
            _LocalApp.TypeID = (clsApplication.enAppType)_AppTypeID;
            _LocalApp.LicenseClassID = LicenseClassID;

            if(_LocalApp.AddLocalDrivingLicenseApp())
            {
                MessageBox.Show("Application saved", "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                lblAppID.Text = _LocalApp.ApplicationID.ToString();
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Fail to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
