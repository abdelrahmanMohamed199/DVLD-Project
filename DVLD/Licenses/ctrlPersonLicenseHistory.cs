using DVLD_BusinessTier;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrlPersonLicenseHistory : UserControl
    {
        public ctrlPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public void LoadData(int PersonID)
        {
            clsDriver _Driver = clsDriver.FindByPersonID(PersonID);
            DataTable dtLocalLicenses = _Driver.GetLocalLicenses();
            DataTable dtInternationalLicenses = _Driver.GetInternationalLicenses();
            dgvLocal.DataSource = dtLocalLicenses;
            dgvInternational.DataSource = dtInternationalLicenses;
            lblNumOfInternationalRecords.Text = dgvInternational.Rows.Count.ToString();
            lblNumOfLocalRecords.Text = dgvLocal.Rows.Count.ToString();
        }
    }
}
