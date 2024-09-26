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

namespace DVLD.Drivers
{
    public partial class frmManageDrivers : Form
    {
        DataTable dtDriversList;
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            dtDriversList = clsDriver.GetDriversList();
            dgvDriversList.DataSource = dtDriversList;
            lblNumOfRecords.Text = dtDriversList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Visible = false;

            if (dgvDriversList.Rows.Count > 0)
            {
                dgvDriversList.Columns[0].HeaderText = "Driver ID";
                dgvDriversList.Columns[0].Width = 80;

                dgvDriversList.Columns[1].HeaderText = "Person ID";
                dgvDriversList.Columns[1].Width = 80;

                dgvDriversList.Columns[2].HeaderText = "National No";
                dgvDriversList.Columns[2].Width = 80;

                dgvDriversList.Columns[3].HeaderText = "Full Name";
                dgvDriversList.Columns[3].Width = 250;

                dgvDriversList.Columns[4].HeaderText = "Date";
                dgvDriversList.Columns[4].Width = 120;

                dgvDriversList.Columns[5].HeaderText = "Active Licenses";
                dgvDriversList.Columns[5].Width = 80;
            }
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
                dtDriversList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvDriversList.Rows.Count.ToString();
            }
            else
            {
                tbFilterBy.Visible = true;
                tbFilterBy.Text = "";
                tbFilterBy.Focus();
            }
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Driver ID" ||  cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
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

            if(FilterColumn == "None" || tbFilterBy.Text.Trim() == "")
            {
                dtDriversList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvDriversList.Rows.Count.ToString();
            }
            else if(FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {
                dtDriversList.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvDriversList.Rows.Count.ToString();
            }
            else
            {
                dtDriversList.DefaultView.RowFilter = string.Format("{0} like '{1}%'", FilterColumn, tbFilterBy.Text);
                lblNumOfRecords.Text = dgvDriversList.Rows.Count.ToString();
            }
        }
    }
}
