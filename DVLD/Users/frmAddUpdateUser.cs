using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        int _UserID;
        clsUser _User;

        public delegate void DataBackEventHandler(int UserID);
        
        public event DataBackEventHandler DataBack;
        public frmAddUpdateUser(int userID)
        {
            InitializeComponent();
            _UserID = userID;           
        }

        private void DefaultUserData()
        {
            _User = new clsUser();
            lblTitle.Text = "Add New User";
            lblUserID.Text = "???";
            btnNext.Enabled = false;
            tabLoginInfo.Enabled = false;
            btnSave.Enabled = false;
        }

        private void LoadUserData()
        {           
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("This user doesn't exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlPersonInfoWithFilter1.LoadPersonInfo(_User.PersonID);
            ctrlPersonInfoWithFilter1.FilterEnable = false;
            tabLoginInfo.Enabled = true;
            btnNext.Enabled = true;
            btnSave.Enabled = true;
            lblTitle.Text = "Update User";
            lblUserID.Text = _UserID.ToString();
            tbUsername.Text = _User.Username;
            chbIsActive.Checked = _User.IsActive;            
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            if(_UserID == -1)
                DefaultUserData();
            else
                LoadUserData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(clsUser.IsUserExist(ctrlPersonInfoWithFilter1.PersonID) && _User.UserID == -1)
            {
                MessageBox.Show("This person is already user", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabLoginInfo.Enabled = false;
                return;
            }
            tabLoginInfo.Enabled = true;
            tabsContainer.SelectedIndex = 1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabsContainer.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbConfirmPassword_Leave(object sender, EventArgs e)
        {
            if(tbPassword.Text != tbConfirmPassword.Text)
            {
                errorProvider1.SetError(tbConfirmPassword, "Password doesn't match");
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text.Trim() == ""|| tbPassword.Text.Trim() == "" 
                || tbConfirmPassword.Text.Trim() == "")   
            {
                MessageBox.Show("Please fill the empty fields", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(tbPassword.Text != tbConfirmPassword.Text)
            {
                MessageBox.Show("The password doesn't match", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(clsUser.IsUserExist(tbUsername.Text.Trim()) && tbUsername.Text.Trim() != _User.Username)
            {
                MessageBox.Show("This username is already taken, Choose another one", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Username = tbUsername.Text.Trim();
            _User.Password = clsUtil.EncryptPassword(tbPassword.Text.Trim());
            _User.IsActive = chbIsActive.Checked;
            _User.PersonID = ctrlPersonInfoWithFilter1.PersonID;
            if(_User.Save())
            {
                MessageBox.Show("User saved successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUserID.Text = _User.UserID.ToString();
                DataBack?.Invoke(_User.UserID);
            }
            else
            {
                MessageBox.Show("Fail to save user data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(int obj)
        {
            btnNext.Enabled = true;
            int PersonID = obj;
            if (!clsUser.IsUserExist(PersonID))
            {
                tabLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                tabLoginInfo.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void tbUsername_Leave(object sender, EventArgs e)
        {
            if (clsUser.IsUserExist(tbUsername.Text.Trim()))
                errorProvider1.SetError(tbUsername, "This username is already taken");
            else
                errorProvider1.SetError(tbUsername, null);
        }
    }
}
