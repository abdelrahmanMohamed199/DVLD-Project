using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowPersonInfo : Form
    {
        int _PersonID;
        public frmShowPersonInfo(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {
            if(!clsPerson.IsPersonExist(_PersonID))
            {
                MessageBox.Show("Person does not exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                this.ctrlShowPersonInfo1.LoadPersonInfo(_PersonID);
            }
        }
    }
}
