using DVLD_BusinessTier;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAppTypesList : Form
    {
        DataTable dtAppTypesList;
        public frmAppTypesList()
        {
            InitializeComponent();
        }

        private void frmAppTypesList_Load(object sender, EventArgs e)
        {
            dtAppTypesList = clsApplicationType.GetAppTypesList();
            dgvAppTypesList.DataSource = dtAppTypesList;
            lblNumOfRecords.Text = dgvAppTypesList.Rows.Count.ToString();
            if (dgvAppTypesList.Rows.Count > 0)
            {
                dgvAppTypesList.Columns[0].HeaderText = "ID";
                dgvAppTypesList.Columns[0].Width = 120;

                dgvAppTypesList.Columns[1].HeaderText = "Title";
                dgvAppTypesList.Columns[1].Width = 400;

                dgvAppTypesList.Columns[2].HeaderText = "Fees";
                dgvAppTypesList.Columns[2].Width = 150;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditAppType frm1 = new frmEditAppType((int)dgvAppTypesList.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
            frmAppTypesList_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
