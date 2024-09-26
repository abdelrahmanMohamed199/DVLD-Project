using DVLD.Licenses;
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
    public partial class frmInternationalLicenseApps : Form
    {
        DataTable _dtInternationalAppsList;
        clsInternationalLicense _InternationalLicense;
        public frmInternationalLicenseApps()
        {
            InitializeComponent();
        }

        private void frmInternationalLicenseApps_Load(object sender, EventArgs e)
        {
            _dtInternationalAppsList = clsInternationalLicense.GetInterAppsList();
            dgvInterAppsList.DataSource = _dtInternationalAppsList;
            lblNumOfRecords.Text = dgvInterAppsList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Visible = false;

            if (dgvInterAppsList.Rows.Count > 0)
            {
                dgvInterAppsList.Columns[0].HeaderText = "Int. License ID";
                dgvInterAppsList.Columns[0].Width = 80;

                dgvInterAppsList.Columns[1].HeaderText = "Application ID";
                dgvInterAppsList.Columns[1].Width = 100;

                dgvInterAppsList.Columns[2].HeaderText = "Driver ID";
                dgvInterAppsList.Columns[2].Width = 80;

                dgvInterAppsList.Columns[3].HeaderText = "Used Local License";
                dgvInterAppsList.Columns[3].Width = 100;

                dgvInterAppsList.Columns[4].HeaderText = "Issue Date";
                dgvInterAppsList.Columns[4].Width = 100;

                dgvInterAppsList.Columns[5].HeaderText = "Expiration Date";
                dgvInterAppsList.Columns[5].Width = 100;

                dgvInterAppsList.Columns[6].HeaderText = "Is Active";
                dgvInterAppsList.Columns[6].Width = 80;

                dgvInterAppsList.Columns[7].HeaderText = "Created By User ID";
                dgvInterAppsList.Columns[7].Width = 80;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm1 = new frmIssueInternationalLicense();
            frm1.ShowDialog();
            frmInternationalLicenseApps_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm1 = new frmShowPersonInfo(_InternationalLicense.PersonID);
            frm1.ShowDialog();
            frmInternationalLicenseApps_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frm1 = new frmInternationalLicenseInfo(_InternationalLicense.InterLicenseID);
            frm1.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(_InternationalLicense.PersonID);
            frm1.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _InternationalLicense = clsInternationalLicense.GetInternationalLicense((int)dgvInterAppsList.CurrentRow.Cells[0].Value);
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
                _dtInternationalAppsList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvInterAppsList.Rows.Count.ToString();
            }
            else
            {
                tbFilterBy.Visible = true;
                tbFilterBy.Focus();
                tbFilterBy.Text = "";
            }
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Int. License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || tbFilterBy.Text == "")
            {
                _dtInternationalAppsList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvInterAppsList.Rows.Count.ToString();
                return;
            }

            _dtInternationalAppsList.DefaultView.RowFilter = string.Format("{0} = {1}",
                FilterColumn, tbFilterBy.Text);
            lblNumOfRecords.Text = dgvInterAppsList.Rows.Count.ToString();
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

    }
}
