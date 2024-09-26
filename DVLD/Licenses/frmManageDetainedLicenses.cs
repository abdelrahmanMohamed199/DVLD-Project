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

namespace DVLD.Licenses
{
    public partial class frmManageDetainedLicenses : Form
    {
        DataTable dtDetainedLicensesList;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            dtDetainedLicensesList = clsDetainedLicense.GetDetainedLicensesList();
            dgvDetainedLicensesList.DataSource = dtDetainedLicensesList;
            lblNumOfRecords.Text = dgvDetainedLicensesList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Visible = false;

        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "License ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "None")
            {
                tbFilterBy.Visible = false;
                dtDetainedLicensesList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvDetainedLicensesList.Rows.Count.ToString();
            }
            else
            {
                tbFilterBy.Visible = true;
                tbFilterBy.Text = "";
                tbFilterBy.Focus();
            }
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "License ID":
                    FilterColumn = "LicenseID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(FilterColumn == "None" || tbFilterBy.Text == "")
            {
                dtDetainedLicensesList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvDetainedLicensesList.Rows.Count.ToString();
            }
            else if(FilterColumn == "DetainID" || FilterColumn == "LicenseID")
            {
                dtDetainedLicensesList.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvDetainedLicensesList.Rows.Count.ToString();
            }
            else
            {
                dtDetainedLicensesList.DefaultView.RowFilter = string.Format("{0} like '{1}%'", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvDetainedLicensesList.Rows.Count.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm1 = new frmDetainLicense();
            frm1.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm1 = new frmReleaseDetainedLicense();
            frm1.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm1 = new frmShowPersonInfo(clsPerson.Find(dgvDetainedLicensesList.CurrentRow.Cells[6].Value.ToString()).ID);
            frm1.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense License = clsLicense.FindByID((int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value);
            frmShowLicense frm1 = new frmShowLicense(clsPerson.Find(dgvDetainedLicensesList.CurrentRow.Cells[6].Value.ToString()).ID, License);
            frm1.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicensesHistory frm1 = new frmShowDriverLicensesHistory(clsPerson.Find(dgvDetainedLicensesList.CurrentRow.Cells[6].Value.ToString()).ID);
            frm1.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm1 = new frmReleaseDetainedLicense((int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value);
            frm1.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!clsDetainedLicense.IsLicenseDetained((int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value))
            {
                releaseLicenseToolStripMenuItem.Enabled = false;
            }
            else
            {
                releaseLicenseToolStripMenuItem.Enabled = true;
            }
        }
    }
}
