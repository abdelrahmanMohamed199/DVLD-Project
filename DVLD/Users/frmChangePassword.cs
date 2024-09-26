using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _User = clsUser.Find(UserID);
        }

        private void LoadUserData()
        {
            if(_User == null)
            {
                MessageBox.Show("User doesn't exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserData(_User.UserID);
            tbCurrentPassword.Focus();
        }

        private void tbCurrentPassword_Leave(object sender, EventArgs e)
        {
            if(tbCurrentPassword.Text == "")
            {
                errorProvider1.SetError(tbCurrentPassword, "This field is required");
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, null);
            }
        }

        private void tbNewPassword_Leave(object sender, EventArgs e)
        {
            if (tbNewPassword.Text == "")
            {
                errorProvider1.SetError(tbNewPassword, "This field is required");
            }
            else
            {
                errorProvider1.SetError(tbNewPassword, null);
            }
        }

        private void tbConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (tbConfirmPassword.Text == "")
            {
                errorProvider1.SetError(tbConfirmPassword, "This field is required");
            }
            else
            {
                if (tbConfirmPassword.Text != tbNewPassword.Text)
                    errorProvider1.SetError(tbConfirmPassword, "Password doesn't match");
                else
                    errorProvider1.SetError(tbConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbCurrentPassword.Text == "" || tbNewPassword.Text == ""
                || tbConfirmPassword.Text == "")
            {
                MessageBox.Show("Please fill the empty fields", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string Password = clsUtil.EncryptPassword(tbCurrentPassword.Text.Trim());
            if(Password != _User.Password)
            {
                MessageBox.Show("Wrong current password", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(tbNewPassword.Text != tbConfirmPassword.Text)
            {
                MessageBox.Show("Password doesn't match", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Password = clsUtil.EncryptPassword(tbNewPassword.Text.Trim());
            _User.Password = Password;
            if(_User.ChangePassword(_User.Password))
            {
                MessageBox.Show("Password saved successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password was not saved", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbCurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void tbNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void tbConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }
    }
}
