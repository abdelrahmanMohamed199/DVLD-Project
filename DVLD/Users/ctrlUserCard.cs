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

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {
        clsUser _User;
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserData(int UserID)
        {
            _User = clsUser.Find(UserID);
            if(_User == null )
            {
                MessageBox.Show("User doesn't exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlShowPersonInfo1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.Username.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }
    }
}
