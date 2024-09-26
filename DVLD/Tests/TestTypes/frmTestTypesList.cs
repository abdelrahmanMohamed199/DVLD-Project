using DVLD_BusinessTier;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmTestTypesList : Form
    {
        DataTable dtTestTypesList;
        public frmTestTypesList()
        {
            InitializeComponent();
        }

        private void frmTestTypesList_Load(object sender, EventArgs e)
        {
            dtTestTypesList = clsTestType.GetTestTypesList();
            dgvTestTypesList.DataSource = dtTestTypesList;
            lblNumOfRecords.Text = dgvTestTypesList.Rows.Count.ToString();

            if (dgvTestTypesList.Rows.Count > 0)
            {
                dgvTestTypesList.Columns[0].HeaderText = "ID";
                dgvTestTypesList.Columns[0].Width = 100;

                dgvTestTypesList.Columns[1].HeaderText = "Title";
                dgvTestTypesList.Columns[1].Width = 180;

                dgvTestTypesList.Columns[2].HeaderText = "Description";
                dgvTestTypesList.Columns[2].Width = 350;

                dgvTestTypesList.Columns[3].HeaderText = "Fees";
                dgvTestTypesList.Columns[3].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm1 = new frmEditTestType((int)dgvTestTypesList.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
            frmTestTypesList_Load(null, null);
        }
    }
}
