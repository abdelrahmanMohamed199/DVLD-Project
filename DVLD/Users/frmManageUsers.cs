using DVLD_BusinessTier;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        DataTable dtUsersList;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _Refresh()
        {
            dtUsersList = clsUser.GetUsersList();
            dgvUsersList.DataSource = dtUsersList;
            lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString(); 
            cbFilterBy.SelectedIndex = 0;
            cbIsActive.Visible = false;
            tbFilterBy.Visible = false;

            if(dgvUsersList.Rows.Count > 0)
            {
                dgvUsersList.Columns["UserID"].HeaderText = "User ID";
                dgvUsersList.Columns["UserID"].Width = 110;

                dgvUsersList.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsersList.Columns["PersonID"].Width = 120;

                dgvUsersList.Columns["FullName"].HeaderText = "Name";
                dgvUsersList.Columns["FullName"].Width = 350;

                dgvUsersList.Columns["UserName"].HeaderText = "Username";
                dgvUsersList.Columns["UserName"].Width = 120;

                dgvUsersList.Columns["IsActive"].HeaderText = "Is Active";
                dgvUsersList.Columns["IsActive"].Width = 120;
            } 
                
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "None")
            {
                tbFilterBy.Visible = false;
                cbIsActive.Visible = false;
                dtUsersList.DefaultView.RowFilter = "";
            }

            if(cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active")
            {
                cbIsActive.Visible = false;
                tbFilterBy.Visible = true;
                tbFilterBy.Focus();
            }

            if(cbFilterBy.Text == "Is Active")
            {
                tbFilterBy.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;               
            }
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "User ID":
                    filterColumn = "UserID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "Name":
                    filterColumn = "FullName";
                    break;
                case "Username":
                    filterColumn = "UserName";
                    break;
            }

            if (tbFilterBy.Text.Trim() == "")
            {
                dtUsersList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
                return;
            }

            if(filterColumn == "UserID" || filterColumn == "PersonID")
            {
                dtUsersList.DefaultView.RowFilter = string.Format("{0} = {1}",
                    filterColumn, tbFilterBy.Text.Trim());
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
            }

            if(filterColumn == "FullName" || filterColumn == "UserName")
            {
                dtUsersList.DefaultView.RowFilter = string.Format("{0} like '{1}%'",
                    filterColumn, tbFilterBy.Text.Trim());
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "Yes")
            {
                dtUsersList.DefaultView.RowFilter = "IsActive = 1";
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
            }
            else if (cbIsActive.Text == "No")
            {
                dtUsersList.DefaultView.RowFilter = "IsActive = 0";
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
            }
            else
            {
                dtUsersList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvUsersList.Rows.Count.ToString();
            }
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser(-1);
            frm1.ShowDialog();
            _Refresh();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser(-1);
            frm1.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser(Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value));
            frm1.ShowDialog();
            _Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this user?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value)))
                {
                    MessageBox.Show("User Deleted Successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Refresh();
                }
                else
                {
                    MessageBox.Show("Fail to delete user due to connected data", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm1 = new frmChangePassword(Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value));
            frm1.ShowDialog();
        }

        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frm1 = new frmUserCard(Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value));
            frm1.ShowDialog();
            _Refresh();
        }
    }
}
