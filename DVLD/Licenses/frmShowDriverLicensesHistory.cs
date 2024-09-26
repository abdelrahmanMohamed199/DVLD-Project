using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowDriverLicensesHistory : Form
    {
        public frmShowDriverLicensesHistory(int PersonID)
        {
            InitializeComponent();
            ctrlPersonInfoWithFilter1.LoadPersonInfo(PersonID);
            ctrlPersonLicenseHistory1.LoadData(PersonID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowDriverLicensesHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonInfoWithFilter1.FilterEnable = false;
        }

    }
}
